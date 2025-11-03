/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.0.35
 * Time: 7/12/2022 2:27:09 PM
 ************************************************************/

ALTER PROCEDURE [dbo].[sp_Wage_Process_Main] 
	@p_PayrollPeriodID INT, 
	@p_WageProcessTypeID INT, 
	@p_TransactionDate DATETIME, 
	@p_UserID VARCHAR(40), 
	@p_PersonID INT
AS
	SET NOCOUNT ON  
	
	BEGIN
		-----------------------------------------------------------------------------------------------------------
		-- Langkah  1.
		-- Set Value Proses Gaji = 'T', Langkah ini supaya jika user proses gaji, user lain tidak bisa proses gaji
		-- pada waktu yang sama, kecuali jika tidak ada lagi yang proses atau sudah selesai
		-----------------------------------------------------------------------------------------------------------  
		
		UPDATE AppParameter
		SET    ParameterValue = 'T'
		FROM   AppParameter WITH (NOLOCK)
		WHERE  ParameterID = 'WageProcessFlag' 
		
		-----------------------------------------------------------------------------------------------------------
		-- Langkah  2.
		-- Masukkan Data Karyawan yang aktif ke temp table Header ##Temp_WageTransaction.
		-----------------------------------------------------------------------------------------------------------  
		
		EXEC sp_Wage_Process_1 @p_PayrollPeriodID,
		     @p_WageProcessTypeID,
		     @p_TransactionDate,
		     @p_UserID,
		     @p_PersonID 
		
		-- Insert THR ke Salary Periodic  
		DECLARE @x VARCHAR(10) = ISNULL(
		            (
		                SELECT ap.ParameterValue
		                FROM   AppParameter AS ap WITH (NOLOCK)
		                WHERE  ap.ParameterID = 'IsThrIncludeInWageProcess'
		            ),
		            'No'
		        )  
		
		IF (@x = 'Yes' OR @p_WageProcessTypeID = 2)
		BEGIN
			--THR
		    EXEC sp_Wage_Process_1_1 @p_PayrollPeriodID, @p_UserID
		END 
		ELSE
		BEGIN
			-- Gapok, DasarBpjs, HutangInsentif Before
			EXEC sp_Wage_Process_1_2_GapokSebelum @p_PayrollPeriodID, @p_UserID
			-- Overtime, PayCut & PjShift
			EXEC sp_Wage_Process_1_2 @p_PayrollPeriodID, @p_UserID
		END
		
		-- faktor x pph21
		EXEC sp_Wage_Process_1_3 @p_PayrollPeriodID, @p_UserID
		
		-----------------------------------------------------------------------------------------------------------
		-- Langkah  3.
		-- Masukkan Data Komponen Gaji yang didapat oleh Karyawan temp table ##Temp_WageTransactionItem.
		-- Nanti akan di insert ke Table WageTransaction.
		-- Ambil nilainya dari Matrix atau Untuk sementara qty, jmlgaji, totalgaji = 0
		-----------------------------------------------------------------------------------------------------------  
		
		EXEC sp_Wage_Process_2 @p_PayrollPeriodID, @p_UserID 
		
		-----------------------------------------------------------------------------------------------------------
		-- Langkah  4.
		-- Update table ##Temp_WageTransactionItem. Nilai diambil dari perhitungan
		-----------------------------------------------------------------------------------------------------------    
		
		EXEC sp_Wage_Process_3 @p_PayrollPeriodID, @p_WageProcessTypeID, @p_PersonID 
		
		-----------------------------------------------------------------------------------------------------------
		-- Langkah  5.
		-- Hapus Data di table WageTransactionItem
		-- Hapus Data di table WageTransaction
		-----------------------------------------------------------------------------------------------------------  
		IF (@p_PersonID = -1)
		BEGIN
		    DELETE 
		    FROM   WageTransactionItem
		    WHERE  WageTransactionID IN (SELECT WageTransactionID
		                                 FROM   WageTransaction WITH (NOLOCK)
		                                 WHERE  PayrollPeriodID = @p_PayrollPeriodID
		                                        AND WageProcessTypeID = @p_WageProcessTypeID)  
		    
		    DELETE 
		    FROM   WageTransaction
		    WHERE  PayrollPeriodID = @p_PayrollPeriodID
		           AND WageProcessTypeID = @p_WageProcessTypeID
		END
		ELSE
		BEGIN
		    DELETE 
		    FROM   WageTransactionItem
		    WHERE  WageTransactionID IN (SELECT WageTransactionID
		                                 FROM   WageTransaction WITH (NOLOCK)
		                                 WHERE  PayrollPeriodID = @p_PayrollPeriodID
		                                        AND PersonID = @p_PersonID
		                                        AND WageProcessTypeID = @p_WageProcessTypeID)  
		    
		    DELETE 
		    FROM   WageTransaction
		    WHERE  PayrollPeriodID = @p_PayrollPeriodID
		           AND PersonID = @p_PersonID
		           AND WageProcessTypeID = @p_WageProcessTypeID
		END 
		
		-----------------------------------------------------------------------------------------------------------
		-- Langkah  6.
		-- Masukkan data dari table ##Temp_WageTransactionItem --> WageTransaction
		-- Masukkan data dari table ##Temp_WageTransactionItem --> WageTransactionItem
		-----------------------------------------------------------------------------------------------------------  
		
		INSERT INTO WageTransaction
		SELECT *
		FROM   ##Temp_WageTransaction WITH (NOLOCK)  
		
		INSERT INTO WageTransactionItem
		SELECT *, 0 IsModified
		FROM   ##Temp_WageTransactionItem  WITH (NOLOCK)
		
		
		-----------------------------------------------------------------------------------------------------------
		-- Langkah  7.
		-- Update table TAX
		-----------------------------------------------------------------------------------------------------------  
		
		DECLARE @SPTYear INT, @SPTMonth INT
		SELECT @SPTYear = pp.SPTYear, @SPTMonth = pp.SPTMonth FROM PayrollPeriod AS pp WHERE pp.PayrollPeriodID = @p_PayrollPeriodID
		
		DECLARE 
			@scGrossIncome INT = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE SRSalaryType = '51'), 111111),
			@scTaxRate INT = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '52'), 111111),
			@scTaxAmount INT = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '59'), 111111),
			@scTaxAmount_Thr INT = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '60'), 111111),
			@scDeduction INT = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '54'), 111111),
			@scTHR INT = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE SRSalaryType = '06'), 111111)
		
		-- delete
		DELETE etc FROM EmployeeTaxCalculation AS etc
		WHERE etc.WageProcessTypeID = @p_WageProcessTypeID AND etc.SPTYear = @SPTYear AND etc.SPTMonth = @SPTMonth
		
		CREATE TABLE ##temp_EmployeeTaxCalculation
		(
			WageTransactionID BIGINT,
			PersonID INT,
			GrossIncome NUMERIC(18,4),
			TaxRate NUMERIC(10,2),
			TaxAmount NUMERIC(18,4),
			Deduction NUMERIC(18,4)
		)
		-- insert to temp table
		INSERT INTO ##temp_EmployeeTaxCalculation
		(
			WageTransactionID,
			PersonID,
			GrossIncome,
			TaxRate,
			TaxAmount,
			Deduction
		)
		SELECT a.WageTransactionID, 
			a.PersonID,
			0 GrossIncome,
			0 TaxRate,
			0 TaxAmount,
			0 Deduction
		FROM ##Temp_WageTransaction AS a
		
		IF (@p_WageProcessTypeID = 1)
		BEGIN
			UPDATE a SET a.GrossIncome = ISNULL((SELECT x.CurrencyAmount FROM ##Temp_WageTransactionItem x WHERE x.WageTransactionID = a.WageTransactionID AND x.SalaryComponentID = @scGrossIncome), 0)
			FROM  ##temp_EmployeeTaxCalculation AS a
			
			UPDATE a SET a.TaxAmount = ISNULL((SELECT x.CurrencyAmount FROM ##Temp_WageTransactionItem x WHERE x.WageTransactionID = a.WageTransactionID AND x.SalaryComponentID = @scTaxAmount), 0)
			FROM  ##temp_EmployeeTaxCalculation AS a
		END
		ELSE
		BEGIN
			UPDATE a SET a.GrossIncome = ISNULL((SELECT x.CurrencyAmount FROM ##Temp_WageTransactionItem x WHERE x.WageTransactionID = a.WageTransactionID AND x.SalaryComponentID = @scTHR), 0)
			FROM  ##temp_EmployeeTaxCalculation AS a
			
			UPDATE a SET a.TaxAmount = ISNULL((SELECT x.CurrencyAmount FROM ##Temp_WageTransactionItem x WHERE x.WageTransactionID = a.WageTransactionID AND x.SalaryComponentID = @scTaxAmount_Thr), 0)
			FROM  ##temp_EmployeeTaxCalculation AS a
		END
		
		UPDATE a SET a.TaxRate = ISNULL((SELECT x.CurrencyAmount FROM ##Temp_WageTransactionItem x WHERE x.WageTransactionID = a.WageTransactionID AND x.SalaryComponentID = @scTaxRate), 0)
		FROM  ##temp_EmployeeTaxCalculation AS a
		
		UPDATE a SET a.Deduction = ISNULL((SELECT x.CurrencyAmount FROM ##Temp_WageTransactionItem x WHERE x.WageTransactionID = a.WageTransactionID AND x.SalaryComponentID = @scDeduction), 0)
		FROM  ##temp_EmployeeTaxCalculation AS a
		
		-- insert to table	
		INSERT INTO EmployeeTaxCalculation
		(
			PersonID,
			WageProcessTypeID,
			SPTYear,
			SPTMonth,
			GrossIncome,
			TaxRate,
			TaxAmount,
			Deduction,
			LastUpdateDateTime,
			LastUpdateByUserID
		)
		SELECT a.PersonID,
			@p_WageProcessTypeID,
			@SPTYear SPTYear,
			@SPTMonth SPTMonth,
			a.GrossIncome,
			a.TaxRate,
			a.TaxAmount,
			a.Deduction,
			GETDATE() LastUpdateDateTime,
			@p_UserID LastUpdateByUserID 
		FROM ##temp_EmployeeTaxCalculation AS a
		
		DROP TABLE ##temp_EmployeeTaxCalculation
		
		
		-----------------------------------------------------------------------------------------------------------
		-- Langkah  8.
		-- Update Nilai flagGaji = 'F' Supaya user lain atau next time user tadi bisa proses gaji
		-----------------------------------------------------------------------------------------------------------  
		
		UPDATE AppParameter
		SET    ParameterValue     = 'F'
		FROM   AppParameter WITH (NOLOCK)
		WHERE  ParameterID        = 'WageProcessFlag' 
		       
		       
		       
		-- Proses Gaji Selesai.
		-----------------------------------------------------------------------------------------------------------
	END  
  