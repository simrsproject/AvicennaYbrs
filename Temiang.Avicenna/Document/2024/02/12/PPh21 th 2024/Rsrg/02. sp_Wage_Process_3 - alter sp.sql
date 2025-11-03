/************************************************************    
 * Code formatted by SoftTree SQL Assistant © v11.0.35    
 * Time: 9/23/2021 1:33:05 PM    
 ************************************************************/    
    
ALTER PROCEDURE [dbo].[sp_Wage_Process_3]     
	 @p_PayrollPeriodID INT,     
	 @p_WageProcessTypeID INT,     
	 @p_PersonID INT    
AS    
 SET NOCOUNT ON           
     
 DECLARE @date DATETIME, @dateThr DATETIME           
 IF (@p_WageProcessTypeID = 1)
 BEGIN
 	SET @date = ISNULL(    
             (    
                 SELECT pp.PayDate    
                 FROM   PayrollPeriod AS pp WITH (NOLOCK)   
                 WHERE  pp.PayrollPeriodID = @p_PayrollPeriodID    
             ),    
             GETDATE()    
         ) 
 END    
 ELSE
 BEGIN
 	SET @date = ISNULL(    
             (    
                 SELECT ts.PayDate    
                 FROM   ThrSchedule AS ts WITH (NOLOCK)    
                 WHERE  ts.PayrollPeriodID = @p_PayrollPeriodID    
             ),    
             GETDATE()    
         )   
 END    
 
 SET @dateThr = EOMONTH(@date)
 
 -----------------------------------------------------------------------------------------------------------    
 -- Proses 3  3.    
 -- Update table ##Temp_WageTransactionItem. Nilai diambil dari perhitungan    
 -----------------------------------------------------------------------------------------------------------              
     
 -----------------------------------------------------------------------------------    
 -- LANGKAH 1: BUAT VARIABLE    
 -- LANGKAH 2: ISI VARIABLE    
 -- Variable ini bersifat Fleksible, bisa ditambah2..    
 -----------------------------------------------------------------------------------            
 BEGIN    
  DECLARE @Gapok                     INT,    
          @SalaryTypeLoan            VARCHAR(20),    
          @SalaryTypeJaminanPensiun  VARCHAR(20),    
          @Ptkp                      INT,    
          @Pph21thn                  INT,    
          @Pph21thnThr               INT,    
          @Pph21bln                  INT,    
          @Pkp                       INT,    
          @PkpThr                    INT,    
          @Pph21Ditanggung           INT,    
          @Pph21Pribadi              INT,    
          @Pph21DTP                  INT = -11,    
          @Pph21ThrDTP               INT = -22,    
          @Thr                       INT,    
          @ThrX                      INT = 43, -- faktor pengali thr
          @Overtime                  INT,    
          @GajiNettoSetahun          INT = 17, --Penghasilan Netto Setaun          
          @GajiNettoSetahunThr       INT = 18, --Penghasilan Netto Setaun + THR          
          @GajiBruttoSetahunDtp      INT = -33, --Penghasilan Brutto Setaun DTP    
          @FaktorRuleDisplay         VARCHAR(20),
          
          @GapokBlnLalu INT = -69


  DECLARE 
  	@TER_BrutoSebulan INT,
	@TER_Tarif INT,
	@TER_BrutoSetahun INT,
	
	@TER_Pengurang INT,
	@TER_AkuPengurang INT,
	@TER_BiayaJabatan INT,
	
	@TER_NettoSetahun INT,
	@TER_PKP INT,
	
	@TER_Pph21_1 INT,
	@TER_Pph21_Thr INT,
	@TER_AkuPph21_1 INT,
	
	@TER_Pph21Setahun INT,
	@TER_Pph21_2 INT
	
          
  SET @TER_BrutoSebulan = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '51'), 111111)
  SET @TER_Tarif = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '52'), 111111)
  SET @TER_BrutoSetahun = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '53'), 111111)
  
  SET @TER_Pengurang = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '54'), 111111)
  SET @TER_AkuPengurang = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '55'), 111111)
  SET @TER_BiayaJabatan = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '56'), 111111)
  
  SET @TER_NettoSetahun = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '57'), 111111)
  SET @TER_PKP = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '58'), 111111)
  
  SET @TER_Pph21_1 = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '59'), 111111)
  SET @TER_Pph21_Thr = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '60'), 111111)
  SET @TER_AkuPph21_1 = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '61'), 111111)
  
  SET @TER_Pph21Setahun = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '62'), 111111)
  SET @TER_Pph21_2 = ISNULL((SELECT SalaryComponentID FROM SalaryComponent WITH (NOLOCK) WHERE  SRSalaryType = '63'), 111111)
  
  
  
  DECLARE @SPTYear INT, @SPTMonth INT
  SELECT @SPTYear = pp.SPTYear, @SPTMonth = pp.SPTMonth FROM PayrollPeriod AS pp WHERE pp.PayrollPeriodID = @p_PayrollPeriodID
  
  DECLARE @IsUsingStandardSalary     VARCHAR(10) = ISNULL(    
              (    
                  SELECT ap.ParameterValue    
                  FROM   AppParameter AS ap WITH (NOLOCK)    
                  WHERE  ap.ParameterID = 'IsPayrollUsingStandardSalary'    
              ),    
              'No'    
          )      
      
  DECLARE @InsentifPPh21DTP          VARCHAR(50) = ISNULL(    
              (    
                  SELECT ap.ParameterValue    
                  FROM   AppParameter AS ap WITH (NOLOCK)    
                  WHERE  ap.ParameterID = 'InsentifPPh21DTP'    
              ),    
              '0'    
          )      
      
  DECLARE @SQLScrip VARCHAR(MAX)     
  -----------------------------------------------------------------------------------               
  
  SET @Gapok = (    
          SELECT SalaryComponentID    
          FROM   SalaryComponent WITH (NOLOCK)    
          WHERE  SRSalaryType = '01'    
      )          
      
  SET @SalaryTypeLoan = '08'          
      
  SET @SalaryTypeJaminanPensiun = '11'          
      
  SET @Ptkp = (    
          SELECT SalaryComponentID    
          FROM   SalaryComponent WITH (NOLOCK)    
          WHERE  SRSalaryType = '17'    
      )          
      
  SET @Pph21thn = (    
          SELECT SalaryComponentID    
          FROM   SalaryComponent WITH (NOLOCK)    
          WHERE  SRSalaryType = '21'    
      )          
      
  SET @Pph21thnThr = (    
          SELECT SalaryComponentID    
          FROM   SalaryComponent WITH (NOLOCK)    
          WHERE  SRSalaryType = '22'    
      )          
      
  SET @Pph21bln = (    
         SELECT SalaryComponentID    
          FROM   SalaryComponent WITH (NOLOCK)    
          WHERE  SRSalaryType = '23'    
      )          
      
  SET @Pkp = (    
          SELECT SalaryComponentID    
          FROM   SalaryComponent WITH (NOLOCK)    
          WHERE  SRSalaryType = '19'    
      )          
      
  SET @PkpThr = (    
          SELECT SalaryComponentID    
          FROM   SalaryComponent WITH (NOLOCK)    
          WHERE  SRSalaryType = '20'    
      )          
      
  SET @Pph21Ditanggung = (    
          SELECT SalaryComponentID    
          FROM   SalaryComponent WITH (NOLOCK)    
          WHERE  SRSalaryType = '24'    
      )            
      
  SET @Pph21Pribadi = (    
          SELECT SalaryComponentID    
          FROM   SalaryComponent WITH (NOLOCK)    
          WHERE  SRSalaryType = '25'    
      )           
      
  SET @Thr = (    
          SELECT SalaryComponentID    
          FROM   SalaryComponent WITH (NOLOCK)    
          WHERE  SRSalaryType = '06'    
      )            
      
  SET @Overtime = ISNULL(    
          (    
              SELECT SalaryComponentID    
              FROM   SalaryComponent WITH (NOLOCK)   
              WHERE  SRSalaryType = 'XXX'    
          ),    
          111111    
      )            
      
      
  SELECT @Gapok = CASE     
                       WHEN @Gapok IS NULL THEN 111111    
                       ELSE @Gapok    
                  END,    
         @Ptkp                = CASE     
                      WHEN @Ptkp IS NULL THEN 111111    
                      ELSE @Ptkp    
                 END,    
         @Pph21thn            = CASE     
                          WHEN @Pph21thn IS NULL THEN 111111    
                          ELSE @Pph21thn    
                     END,    
         @Pph21thnThr         = CASE     
                             WHEN @Pph21thnThr IS NULL THEN 111111    
                             ELSE @Pph21thnThr    
                        END,    
         @Pph21bln            = CASE     
                          WHEN @Pph21bln IS NULL THEN 111111    
                          ELSE @Pph21bln    
                     END,    
         @Pkp                 = CASE     
                     WHEN @Pkp IS NULL THEN 111111    
                     ELSE @Pkp    
                END,    
         @PkpThr              = CASE     
                        WHEN @PkpThr IS NULL THEN 111111    
                        ELSE @PkpThr    
                   END,    
         @Pph21Ditanggung     = CASE     
                                 WHEN @Pph21Ditanggung IS NULL THEN 111111    
                                 ELSE @Pph21Ditanggung    
                            END,    
         @Pph21Pribadi        = CASE     
                              WHEN @Pph21Pribadi IS NULL THEN 111111    
                              ELSE @Pph21Pribadi    
                         END,    
         @Thr                 = CASE     
                     WHEN @Thr IS NULL THEN 111111    
                     ELSE @Thr    
                END    
 END     
     
 -----------------------------------------------------------------------------------    
 -- Update ##Temp_WageTransactionItem    
 -----------------------------------------------------------------------------------              
 BEGIN    
  DECLARE SalaryComponent_Cursor CURSOR     
  FOR    
      SELECT sc.SalaryComponentID,    
             sc.SRSalaryType,    
             sc.IsOrganizationUnit,    
             sc.IsEmployeeStatus,    
             sc.IsPosition,    
             sc.IsReligion,    
             sc.IsEmployee,    
             sc.IsEmploymentType,    
             sc.IsPositionGrade,    
             sc.IsMaritalStatus,    
             sc.IsServiceYear,    
             sc.IsSalaryTableNumber,    
             sc.IsEmployeeGrade,    
             sc.IsNoOfDependent,    
             sc.IsAttedanceMatrixID,    
             sc.IsKWI,    
             sc.IsEducationLevel,    
             sc.IsEmployeeType,    
             sc.IsServiceUnitID,    
             sc.FaktorRuleDisplay,    
             ISNULL(scr.Formula, 0) AS RoundingValue ,    
             ISNULL(scr.Formula_2, 0) AS RoundingValue2    
      FROM   SalaryComponent sc WITH (NOLOCK)    
             LEFT JOIN SalaryComponentRounding AS scr WITH (NOLOCK)    
                  ON  scr.SalaryComponentRoundingID = sc.SalaryComponentRoundingID    
      ORDER BY    
             sc.SalaryComponentCode     
      
  OPEN SalaryComponent_Cursor              
  DECLARE @SalaryComponentID VARCHAR(15),    
          @SRSalaryType VARCHAR(20),    
          @IsOrganizationUnit BIT,    
          @IsEmployeeStatus BIT,    
          @IsPosition BIT,    
          @IsReligion BIT,    
          @IsEmployee BIT,    
          @IsEmploymentType BIT,    
          @IsPositionGrade BIT,    
          @IsMaritalStatus BIT,    
          @IsServiceYear BIT,    
          @IsSalaryTableNumber BIT,    
          @IsEmployeeGrade BIT,    
          @IsNoOfDependent INT,    
          @IsAttedanceMatrixID BIT,    
          @IsKWI BIT,    
          @IsEducationLevel BIT,    
          @IsEmployeeType BIT,    
          @IsServiceUnitID BIT,    
          @RoundingValue INT,   
          @RoundingValue2 INT
               
      
  FETCH NEXT FROM SalaryComponent_Cursor INTO     
  @SalaryComponentID,     
  @SRSalaryType,     
  @IsOrganizationUnit,     
  @IsEmployeeStatus,     
  @IsPosition,     
  @IsReligion,     
  @IsEmployee,     
  @IsEmploymentType,     
  @IsPositionGrade,     
  @IsMaritalStatus,     
  @IsServiceYear,     
  @IsSalaryTableNumber,     
  @IsEmployeeGrade,     
  @IsNoOfDependent,     
  @IsAttedanceMatrixID,     
  @IsKWI,     
  @IsEducationLevel,     
  @IsEmployeeType,     
  @IsServiceUnitID,     
  @FaktorRuleDisplay,     
  @RoundingValue,
  @RoundingValue2             
  WHILE (@@FETCH_STATUS <> -1)    
  BEGIN    
      -----------------------------------------------------------------------------------    
      -----------------------------------------------------------------------------------    
      --0 > Jika menggunakan standard salary, gapok ambil dari standard salary, sesuai dg EmploymentType    
      -----------------------------------------------------------------------------------    
      -----------------------------------------------------------------------------------             
      IF (@SalaryComponentID = @Gapok AND @IsUsingStandardSalary = 'Yes') 
      BEGIN
      	
      		DECLARE @wageBase NUMERIC(18,2) = ISNULL((SELECT TOP 1 wb.Nominal FROM WageBase AS wb WHERE wb.ValidFrom <= @date ORDER BY wb.ValidFrom DESC), 0)
      	
      		UPDATE ##Temp_WageTransactionItem
      		SET Qty = 1, 
      			NominalAmount = (C.Points * @wageBase),
      			CurrencyAmount = (C.Points * @wageBase)
			FROM ##Temp_WageTransactionItem A 
			INNER JOIN ##Temp_WageTransaction B
				ON A.WageTransactionID = B.WageTransactionID 
				AND A.SalaryComponentID = @SalaryComponentID 
			INNER JOIN (SELECT p.PersonID, 
							ISNULL((SELECT TOP 1 wp.Points FROM EmployeeWageStructureAndScale AS wp WITH (NOLOCK) WHERE wp.PersonID = p.PersonID AND wp.ValidFrom <= @date ORDER BY wp.ValidFrom DESC), 0) AS Points
						 FROM PersonalInfo AS p WITH (NOLOCK)) C    
							  ON  C.PersonID = B.PersonID 
      END     
          
      -----------------------------------------------------------------------------------    
      -----------------------------------------------------------------------------------    
      --1 > Komponen Piutang    
      -----------------------------------------------------------------------------------    
      -----------------------------------------------------------------------------------              
      IF @SRSalaryType = @SalaryTypeLoan    
      BEGIN    
          ----------------------------------------------------------------    
          -- Update Detail    
          ----------------------------------------------------------------    
          --cek apakah dari proses rekalkulasi, kalo tidak proses biasa, tp kalo iya ambil nilai dari periodic salary          
          IF @p_PersonID = -1    
          BEGIN    
              UPDATE EmployeeLoanItem    
              SET    ActualDate = GETDATE(),    
                     ActualAmount = PlanAmount,    
                     IsPaid = 1    
              WHERE  PayrollPeriodID = @p_PayrollPeriodID    
                     AND EmployeeLoanID IN (SELECT el.EmployeeLoanID    
                                            FROM   EmployeeLoan AS el    
                                            WHERE  el.IsApproved = 1)          
                  
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = elix.PlanAmount,    
                     CurrencyAmount = elix.PlanAmount    
              FROM   ##Temp_WageTransactionItem A    
                     INNER JOIN SalaryComponent B WITH (NOLOCK)    
                          ON  A.SalaryComponentID = B.SalaryComponentID    
                          AND A.SalaryComponentID = @SalaryComponentID    
                     INNER JOIN ##Temp_WageTransaction C    
                          ON  A.WageTransactionID = C.WageTransactionID    
                     INNER JOIN (    
                              SELECT el.PersonID,    
                                     eli.PayrollPeriodID,    
                                     el.SalaryCodeInstallment,    
                                     SUM(eli.PlanAmount) AS PlanAmount    
                              FROM   EmployeeLoan AS el WITH (NOLOCK)    
                                     INNER JOIN EmployeeLoanItem AS eli WITH (NOLOCK)    
                                          ON  eli.EmployeeLoanID = el.EmployeeLoanID    
                              WHERE  el.IsApproved = 1    
                              GROUP BY    
                                     el.PersonID,    
                                     eli.PayrollPeriodID,    
                                     el.SalaryCodeInstallment    
                          ) elix    
                          ON  elix.PersonID = C.PersonID    
                          AND elix.PayrollPeriodID = @p_PayrollPeriodID    
          END    
          ELSE    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = eps.Amount,    
                     CurrencyAmount = eps.Amount    
              FROM   ##Temp_WageTransactionItem A    
                     INNER JOIN SalaryComponent B WITH (NOLOCK)    
                          ON  A.SalaryComponentID = B.SalaryComponentID    
                          AND A.SalaryComponentID = @SalaryComponentID    
                     INNER JOIN ##Temp_WageTransaction C    
                          ON  A.WageTransactionID = C.WageTransactionID    
                     INNER JOIN EmployeePeriodicSalary AS eps WITH (NOLOCK)    
                          ON  eps.PayrollPeriodID = @p_PayrollPeriodID    
                          AND eps.PersonID = @p_PersonID    
                          AND eps.SalaryComponentID = A.SalaryComponentID    
          END     
      END     
          
      -----------------------------------------------------------------------------------    
      -----------------------------------------------------------------------------------    
      -- 2 > Komponen PTKP    
      -----------------------------------------------------------------------------------    
      -----------------------------------------------------------------------------------              
      IF @SalaryComponentID = @ptkp    
      BEGIN    
          UPDATE ##Temp_WageTransactionItem    
          SET    Qty               = 1,    
                 NominalAmount     = A.NominalAmount,
				 CurrencyAmount = A.NominalAmount 
          FROM   (    
                     SELECT x.SRTaxStatus,    
                            x.Amount AS NominalAmount    
                     FROM   Ptkp x WITH (NOLOCK)    
                     WHERE  @date BETWEEN x.ValidFrom AND x.ValidTo    
                 ) A    
                 INNER JOIN ##Temp_WageTransaction B    
                      ON  A.SRTaxStatus = B.SRTaxStatus    
                 INNER JOIN ##Temp_WageTransactionItem C    
                      ON  B.WageTransactionID = C.WageTransactionID    
                      AND C.SalaryComponentID = @Ptkp    
      END     
          
          
      -----------------------------------------------------------------------------------    
      -----------------------------------------------------------------------------------    
      -- 3 > KOMPONEN LAINNYA    
      -----------------------------------------------------------------------------------    
      --> 3.1 PILIH SEMUA KOMPONEN SELAIN KOMPONEN2 DIATAS.    
      --      HITUNG SEMUA KOMPONEN - KOMPONEN ANAKNYA    
      -----------------------------------------------------------------------------------          
      IF (    
             @SRSalaryType <> @SalaryTypeLoan    
             AND @SalaryComponentID <> @Ptkp    
         )    
      BEGIN    
          DECLARE SalaryComponentRuleMatrix_Cursor CURSOR     
          FOR    
              SELECT x.SalaryRuleComponentID,    
                     x.SROperandType    
              FROM   SalaryComponentRuleMatrix x WITH (NOLOCK)
              INNER JOIN SalaryComponent AS sc WITH (NOLOCK) ON sc.SalaryComponentID = x.SalaryRuleComponentID
              WHERE  x.SalaryComponentID = @SalaryComponentID    
              ORDER BY    
                     x.SROperandType, sc.SalaryComponentCode   
              
          OPEN SalaryComponentRuleMatrix_Cursor              
          DECLARE @SalaryComponentIDDt1 INT            
          DECLARE @SROperandType1 VARCHAR(20)     
              
          FETCH NEXT FROM SalaryComponentRuleMatrix_Cursor INTO @SalaryComponentIDDt1, @SROperandType1              
              
          WHILE (@@FETCH_STATUS <> -1)    
          BEGIN    
              EXEC sp_Wage_Process_4 @SalaryComponentID,    
                   @SalaryComponentIDDt1,    
                   @SROperandType1     
                  
              FETCH NEXT FROM SalaryComponentRuleMatrix_Cursor INTO @SalaryComponentIDDt1,     
              @SROperandType1    
          END     
          CLOSE SalaryComponentRuleMatrix_Cursor     
          DEALLOCATE SalaryComponentRuleMatrix_Cursor     
              
          -----------------------------------------------------------------------------------    
          -- 3.2 Hasilnya dikali dengan faktor pengali    
          -----------------------------------------------------------------------------------             
          IF (@SalaryComponentID <> @Overtime)    
          BEGIN    
              SET @SQLScrip = ' UPDATE ##Temp_WageTransactionItem SET '            
                  
              IF @IsAttedanceMatrixID = 0    
              BEGIN    
                  IF @IsServiceYear = 1    
                  BEGIN    
                      SET @SQLScrip = @SQLScrip + ' Qty = 1, ' +    
                          ' NominalAmount = CASE ' +    
                '  WHEN ISNULL(CASE WHEN D.NominalAmount = 0 THEN (D.PercentageAmount / 100) * ISNULL(D.NominalAmount2, 0) ELSE D.NominalAmount END, 0) > 0 '     
                          +    
                          '   THEN (B.FaktorRule * CASE WHEN D.NominalAmount = 0 THEN (D.PercentageAmount / 100) * ISNULL(D.NominalAmount2, 0) ELSE D.NominalAmount END) '     
                          +    
                          '  WHEN A.NominalAmount > 0 THEN (B.FaktorRule * A.NominalAmount) '     
                          +    
                          ' ELSE 0 ' +    ---gw tambahin disini untuk settting minamountnya
                          ' END '    
                  END    
                  ELSE           
                  IF @IsKWI = 1    
                  BEGIN    
                      SET @SQLScrip = @SQLScrip + ' Qty = 1, ' +    
                          ' NominalAmount  = CASE WHEN c.IsKWI = 1 THEN CASE WHEN ISNULL(D.NominalAmount, 0) > 0 '     
                          +    
     ' THEN ( B.FaktorRule * D.NominalAmount )' +    
                          ' WHEN A.NominalAmount > 0 ' +    
                          ' THEN ( B.FaktorRule * A.NominalAmount )' +    
                          ' ELSE 0 END ELSE 0 END '    
                  END    
                  ELSE           
                  IF @IsMaritalStatus = 1    
                  BEGIN    
                      SET @SQLScrip = @SQLScrip + ' Qty = 1, ' +    
                          ' NominalAmount  = CASE WHEN ISNULL(D.NominalAmount, 0) > 0 THEN B.FaktorRule * (CASE WHEN ISNULL(D.PercentageAmount, 0) > 0 THEN ((ISNULL(D.PercentageAmount, 0) / 100) * D.NominalAmount) ELSE ISNULL(D.NominalAmount, 0) END)'    
 
                          +    
                          ' WHEN A.NominalAmount > 0 THEN B.FaktorRule * (CASE WHEN ISNULL(D.PercentageAmount, 0) > 0 THEN ((ISNULL(D.PercentageAmount, 0) / 100) * A.NominalAmount) ELSE ISNULL(D.NominalAmount, 0) END)'     
                          +    
                          ' ELSE 0 END '    
                  END    
                  ELSE    
                  BEGIN    
                   --   SET @SQLScrip = @SQLScrip + ' Qty = 1, ' +
		    	            --' NominalAmount  = CASE WHEN ISNULL(D.NominalAmount, 0) > 0 ' -- imel
		    	            --+
		    	            --' THEN ( CASE WHEN A.NominalAmount > 0 OR ISNULL(B.MinAmount,0) <0   THEN (CASE WHEN B.FaktorRule = 0 THEN (D.NominalAmount * A.NominalAmount) ELSE (B.FaktorRule * D.NominalAmount * A.NominalAmount) END) ELSE (B.FaktorRule * D.NominalAmount) END )' +
		    	            --' WHEN A.NominalAmount > 0  OR ISNULL(B.MinAmount,0) <0  ' + -- imel
		    	            ----' THEN ( B.FaktorRule * (CASE WHEN ISNULL(D.PercentageAmount, 0) > 0 THEN ((ISNULL(D.PercentageAmount, 0) / 100) * A.NominalAmount ) ELSE A.NominalAmount END ) ) ' +
		    	            --' THEN ( CASE WHEN B.FaktorRule = 0 AND ISNULL(D.PercentageAmount, 0) = 0 THEN 0 ELSE (CASE WHEN B.FaktorRule = 0 AND ISNULL(D.PercentageAmount, 0) > 0 THEN ((ISNULL(D.PercentageAmount, 0) / 100) * A.NominalAmount ) ELSE ' +
		    	            --' (B.FaktorRule * (CASE WHEN ISNULL(D.PercentageAmount, 0) > 0 THEN ((ISNULL(D.PercentageAmount, 0) / 100) * A.NominalAmount ) ELSE A.NominalAmount END )) END) END ) ' +
		    	            --' ELSE 0 END '
		    	        SET @SQLScrip = @SQLScrip + ' Qty = 1, ' +
		    	            ' NominalAmount  = CASE WHEN ISNULL(D.NominalAmount, -1) > 0 ' -- imel
		    	            +
		    	            ' THEN ( CASE WHEN A.NominalAmount > 0 OR ISNULL(B.MinAmount,0) <0   THEN (CASE WHEN B.FaktorRule = 0 THEN (D.NominalAmount * A.NominalAmount) ELSE (B.FaktorRule * D.NominalAmount * A.NominalAmount) END) ELSE (B.FaktorRule * D.NominalAmount) END )' +
		    	            ' WHEN A.NominalAmount > 0  OR ISNULL(B.MinAmount,0) <0  ' + -- imel
		    	            ' THEN ( CASE WHEN B.FaktorRule = 0 AND ISNULL(D.PercentageAmount, -1) = 0 THEN 0 ELSE (CASE WHEN B.FaktorRule = 0 AND ISNULL(D.PercentageAmount, -1) > 0 THEN ((ISNULL(D.PercentageAmount, 0) / 100) * A.NominalAmount ) ELSE ' +
		    	            ' (B.FaktorRule * (CASE WHEN ISNULL(D.PercentageAmount, -1) > 0 THEN ((ISNULL(D.PercentageAmount, 0) / 100) * A.NominalAmount ) ELSE A.NominalAmount END )) END) END ) ' +
		    	            ' ELSE 0 END '
		    	        
                  END    
              END          
                  
              IF @IsAttedanceMatrixID = 1    
              BEGIN    
                  IF @IsServiceYear = 1    
                  BEGIN    
                      SET @SQLScrip = @SQLScrip + ' Qty = 1, ' +    
                          ' NominalAmount = CASE ' +    
                          '  WHEN ISNULL(CASE WHEN D.NominalAmount = 0 THEN (D.PercentageAmount / 100) * ISNULL(D.NominalAmount2, 0) ELSE D.NominalAmount END, 0) > 0 '     
                          +    
                          '   THEN (B.FaktorRule * CASE WHEN D.NominalAmount = 0 THEN (D.PercentageAmount / 100) * ISNULL(D.NominalAmount2, 0) ELSE D.NominalAmount END) '     
                          +    
                          '  WHEN A.NominalAmount > 0 THEN (B.FaktorRule * A.NominalAmount) '     
                          +    
                          ' ELSE B.FaktorRule ' +    
                          ' END '    
                  END    
                  ELSE           
                  IF @IsKWI = 1    
                  BEGIN    
                      SET @SQLScrip = @SQLScrip +    
                          ' Qty = ISNULL(E.Qty,0), ' +    
                          ' NominalAmount  = CASE WHEN c.IsKWI = 1 THEN ISNULL(CASE WHEN ISNULL(D.NominalAmount, 0) > 0 '     
                          +    
                          ' THEN ( B.FaktorRule * D.NominalAmount )' +    
                          ' WHEN A.NominalAmount > 0 ' +    
                          ' THEN ( B.FaktorRule * A.NominalAmount )' +    
                          ' ELSE 0 END,0) ELSE 0 END '    
                  END    
                  ELSE    
                  BEGIN    
                      SET @SQLScrip = @SQLScrip +    
                          ' Qty = ISNULL(E.Qty,0), ' +    
                          ' NominalAmount  = ISNULL(CASE WHEN ISNULL(D.NominalAmount, 0) > 0 '     
                          +    
                          ' THEN ( B.FaktorRule * D.NominalAmount )' +    
                          ' WHEN A.NominalAmount > 0 ' +    
                          ' THEN ( B.FaktorRule * A.NominalAmount )' +    
                          ' ELSE 0 END,0) '    
                  END    
              END           
                  
              IF @IsServiceYear = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' FROM ##Temp_WageTransactionItem A ' +    
                      ' INNER JOIN SalaryComponent B ON A.SalaryComponentID = B. SalaryComponentID AND A.SalaryComponentID = '     
                      + @SalaryComponentID + ' ' +    
                      ' INNER JOIN ##Temp_WageTransaction C ON A.WageTransactionID = C.WageTransactionID '     
                      +    
                      ' INNER JOIN (SELECT d.SalaryComponentID, D.ValidFrom, D.ValidTo, d.ServiceYear, D.PercentageAmount, D.NominalAmount, E.NominalAmount AS NominalAmount2, f.PersonID, D.SREmploymentType '     
                      +    
                      '    FROM SalaryComponentRuleDefinition D ' +    
                      '    INNER JOIN ##Temp_WageTransactionItem E '     
                      +    
                      '     ON  D.PercentageComponentID = E.SalaryComponentID '     
                      +    
                      '   INNER JOIN ##Temp_WageTransaction AS F '     
                      +    
                      '    ON  e.WageTransactionID = f.WageTransactionID '     
                      +    
                      '   WHERE D.SalaryComponentID = ' + @SalaryComponentID     
                      + ' ' +    
                      '  AND CONVERT(VARCHAR(10), D.ValidFrom, 112) <= ''' + CONVERT(VARCHAR(10), @date, 112) +    
                      ''' ' +    
                      '  AND CONVERT(VARCHAR(10), D.ValidTo, 112) >= ''' + CONVERT(VARCHAR(10), @date, 112) +    
                      ''' ' +    
                      '  AND f.ServiceYear BETWEEN CASE WHEN CHARINDEX(''-'', D.ServiceYear) = 0 THEN D.ServiceYear '     
                      +    
                      '         ELSE CAST(SUBSTRING(D.ServiceYear, 0, CHARINDEX(''-'', D.ServiceYear)) AS INT) '     
                      +    
                      '        END AND CASE WHEN CHARINDEX(''-'', D.ServiceYear) = 0 THEN D.ServiceYear '     
                      +    
                      '           ELSE CAST(SUBSTRING(D.ServiceYear, CHARINDEX(''-'', D.ServiceYear) + 1, 3) AS INT) '     
                      +    
                      '          END) D ' +    
                      '  ON  c.PersonID = d.PersonID '    
              END    
              ELSE    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' FROM ##Temp_WageTransactionItem A ' +    
                      ' INNER JOIN SalaryComponent B ON A.SalaryComponentID = B. SalaryComponentID AND A.SalaryComponentID = '     
                      + @SalaryComponentID + ' ' +    
                      ' INNER JOIN ##Temp_WageTransaction C ON A.WageTransactionID = C.WageTransactionID '     
                      +    
                      ' LEFT JOIN SalaryComponentRuleDefinition D ON A.SalaryComponentID = D.SalaryComponentID '     
                      +    
                      ' AND CONVERT(VARCHAR(10), D.ValidFrom, 112) <= ''' + CONVERT(VARCHAR(10), @date, 112) +    
                      ''' '     
                      +    
                      ' AND CONVERT(VARCHAR(10), D.ValidTo, 112) >= ''' + CONVERT(VARCHAR(10), @date, 112) + ''' '    
              END           
                  
              IF @IsOrganizationUnit = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.OrganizationUnitID = D.OrganizationUnitID '    
              END          
                  
              IF @IsEmployeeStatus = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.SREmployeeStatus = D.SREmployeeStatus '    
              END          
                  
              IF @IsPosition = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.PositionID = D.PositionID '    
              END          
                  
              IF @IsReligion = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.SRReligion = D.SRReligion '    
              END          
                  
              IF @IsEmployee = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.PersonID = D.PersonID '    
              END          
                  
              IF @IsEmploymentType = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.SREmploymentType = D.SREmploymentType '    
              END          
                  
              IF @IsPositionGrade = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.PositionGradeID = D.PositionGradeID '    
              END          
                  
              IF @IsMaritalStatus = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.SRMaritalStatus = D.SRMaritalStatus '    
              END           
                  
              IF @IsSalaryTableNumber = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.SalaryTableNumber = D.SalaryTableNumber '    
              END          
                  
              IF @IsEmployeeGrade = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.EmployeeGradeID = D.EmployeeGradeID '    
              END          
                  
              IF @IsNoOfDependent = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.NoOfDependent = D.NoOfDependent '    
              END          
                  
              IF @IsAttedanceMatrixID = 1    
              BEGIN    
                  DECLARE @S1     VARCHAR(20),    
                          @S2     VARCHAR(20)            
                      
                  SET @S1 = (    
                          SELECT AttedanceMatrixID    
                          FROM   SalaryComponentRuleDefinition WITH (NOLOCK)    
                          WHERE  SalaryComponentID = @SalaryComponentID    
                      )          
                      
                  SET @S2 = (    
                          SELECT AttedanceMatrixFieldt    
                          FROM   AttedanceMatrix WITH (NOLOCK)    
                          WHERE  AttedanceMatrixID = @S1    
                      )          
                      
                  SET @SQLScrip = @SQLScrip +    
                      'LEFT JOIN (SELECT PersonID, PayrollPeriodID, ' + @S2     
                      +    
                      ' AS Qty FROM MontlyAttedance WHERE PayrollPeriodID= '     
                      + CAST(@p_PayrollPeriodID AS VARCHAR(MAX)) + ' AND '     
                      + @S2 + ' > 0) E ON C.PersonID = E.PersonID '    
              END           
                  
              IF @IsKWI = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip + ' AND C.IsKWI = 1 '    
              END          
                  
              IF @IsEducationLevel = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                ' AND C.SREducationLevel = D.SREducationLevelID '    
              END          
                  
              IF @IsEmployeeType = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.SREmployeeType = D.SREmployeeType '    
              END          
                  
              IF @IsServiceUnitID = 1    
              BEGIN    
                  SET @SQLScrip = @SQLScrip +    
                      ' AND C.ServiceUnitID = D.ServiceUnitID '    
              END     
                  
              --PRINT (@SQLScrip)           
              EXEC (@SQLScrip)    
          END     
              
          -------------------------------------------------------------------------------------    
          ---- 3.3 Hitung nilai min & max    
          -------------------------------------------------------------------------------------           
              
          --BEGIN    
          -- UPDATE ##Temp_WageTransactionItem    
          -- SET    NominalAmount = CASE     
          --                             WHEN (sc.MinAmount = 0 AND sc.MaxAmount = 0) OR sc.MinAmount = -1 THEN B.NominalAmount    
          --                             WHEN sc.MinAmount > 0 AND B.NominalAmount < sc.MinAmount THEN sc.MinAmount    
          --                             WHEN sc.MaxAmount > 0 AND B.NominalAmount > sc.MaxAmount THEN sc.MaxAmount    
          --                             ELSE B.NominalAmount    
          --                        END    
          -- FROM   ##Temp_WageTransaction A    
          --        INNER JOIN ##Temp_WageTransactionItem B    
          --             ON  A.WageTransactionID = B.WageTransactionID    
          --        INNER JOIN SalaryComponent AS sc    
          --             ON  sc.SalaryComponentID = B.SalaryComponentID    
          --END     
              
          -----------------------------------------------------------------------------------    
          -- 3.4 Periodic Salary - selain THR    
          -----------------------------------------------------------------------------------           
          IF (    
                 @SalaryComponentID <> @Overtime    
                 AND @SalaryComponentID <> @Thr    
             )    
          BEGIN    
              -----------------------------------------------------------------------------------    
              -- 3.4.1 Hitung nilai amount    
              -----------------------------------------------------------------------------------           
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty = 1,    
                     NominalAmount = CASE     
                                          WHEN A.NominalAmount = 0 THEN C.Amount * (    
                                                   SELECT CAST(    
                                                              (CASE WHEN sc.Amount = 0 THEN 1 ELSE sc.Amount END)     
                                                              AS NUMERIC(18, 2)    
                                                          )    
                                                   FROM   SalaryComponent AS sc WITH (NOLOCK)    
                                                   WHERE  sc.SalaryComponentID = @SalaryComponentID    
                                               )    
                                          ELSE A.NominalAmount * C.Amount    
                                     END    
              FROM   ##Temp_WageTransactionItem A    
                     INNER JOIN ##Temp_WageTransaction B    
                          ON  A.WageTransactionID = B.WageTransactionID    
                          AND A.SalaryComponentID = @SalaryComponentID    
                     INNER JOIN (    
                              SELECT eps.PayrollPeriodID,    
                                     eps.PersonID,    
                                     eps.SalaryComponentID,    
                                     SUM(eps.Amount) AS Amount    
                              FROM   EmployeePeriodicSalary eps WITH (NOLOCK)    
                              GROUP BY    
                                     eps.PayrollPeriodID,    
                                     eps.PersonID,    
                                     eps.SalaryComponentID    
                          ) C    
                          ON  B.PersonID = C.PersonID    
                          AND C.PayrollPeriodID = @p_PayrollPeriodID    
                          AND A.SalaryComponentID = C.SalaryComponentID     
                  
              -----------------------------------------------------------------------------------    
              -- 3.4.2 NOL-kan amount yg bukan salary component yg termasuk dalam proses periodic salary    
              -----------------------------------------------------------------------------------           
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = 0    
              FROM   ##Temp_WageTransactionItem A    
                     INNER JOIN ##Temp_WageTransaction B    
                          ON  A.WageTransactionID = B.WageTransactionID    
                          AND A.SalaryComponentID = @SalaryComponentID    
                     INNER JOIN SalaryComponent AS sc WITH (NOLOCK)    
                          ON  sc.SalaryComponentID = A.SalaryComponentID    
                          AND sc.IsPeriodicSalary = 1    
              WHERE  B.PersonID NOT IN (SELECT C.PersonID    
           FROM   EmployeePeriodicSalary C WITH (NOLOCK)    
                                        WHERE  C.PayrollPeriodID = @p_PayrollPeriodID    
                                               AND C.SalaryComponentID = A.SalaryComponentID)    
          END     
              
           -----------------------------------------------------------------------------------    
          -- 3.5 Periodic Salary - THR    
          --     NOL-kan amount yg tidak termasuk dalam proses periodic salary 
          --     Faktor Pengali THR   
          -----------------------------------------------------------------------------------  
          -- faktor pengali THR       
          IF (@SalaryComponentID = @ThrX)    
          BEGIN    
              UPDATE A    
              SET    A.NominalAmount = CASE     
                                          WHEN DATEDIFF(MONTH, vet.JoinDate, @date) >= 12 THEN 1    
                                          ELSE (    
                                                   DATEDIFF(MONTH, vet.JoinDate, @date) / 12.00    
                                               )  
                                     END,    
                     A.CurrencyAmount = CASE     
                                           WHEN DATEDIFF(MONTH, vet.JoinDate, @date) >= 12 THEN 1    
                                           ELSE (    
                                                    DATEDIFF(MONTH, vet.JoinDate, @date) / 12.00    
                                                )    
                                      END
              FROM   ##Temp_WageTransactionItem A    
                     INNER JOIN ##Temp_WageTransaction B    
                          ON  A.WageTransactionID = B.WageTransactionID    
                          AND A.SalaryComponentID = @ThrX  
                     INNER JOIN  ##Temp_WageTransactionItem C ON C.WageTransactionID = B.WageTransactionID AND C.SalaryComponentID = @Thr
                     INNER JOIN Vw_EmployeeTable AS vet WITH (NOLOCK)    
                          ON  vet.PersonID = B.PersonID    
			
          END   
          
          --THR NOL-kan amount yg tidak termasuk dalam proses periodic salary atau     
          IF (@SalaryComponentID = @Thr OR @p_WageProcessTypeID = 1)    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = 0    
              FROM   ##Temp_WageTransactionItem A    
                     INNER JOIN ##Temp_WageTransaction B    
                          ON  A.WageTransactionID = B.WageTransactionID    
                          AND A.SalaryComponentID = @Thr    
              WHERE  B.PersonID NOT IN (SELECT C.PersonID    
                                        FROM   EmployeePeriodicSalary C WITH (NOLOCK)    
                                        WHERE  C.PayrollPeriodID = @p_PayrollPeriodID    
                                               AND C.SalaryComponentID = A.SalaryComponentID)                 
          END   
          
            
              
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------    
          -- 4 > Komponen JP    
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------            
              
          IF (@SRSalaryType = @SalaryTypeJaminanPensiun)    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = 0    
              FROM   ##Temp_WageTransactionItem A    
                     INNER JOIN ##Temp_WageTransaction B    
                          ON  A.WageTransactionID = B.WageTransactionID    
                     INNER JOIN SalaryComponent AS sc WITH (NOLOCK)    
                          ON  sc.SalaryComponentID = A.SalaryComponentID    
              WHERE  sc.SRSalaryType = @SalaryTypeJaminanPensiun    
                     AND B.PersonID IN (SELECT C.PersonID    
                                        FROM   Vw_EmployeeTable C WITH (NOLOCK)    
                                        WHERE  C.ResignDate <= @date)    
          END     
              
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------    
          -- 5 > Komponen PKP    
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------           
              
          IF @SalaryComponentID = @Pkp    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = CASE     
                                          WHEN (Y.NominalAmount > X.NominalAmount) THEN (Y.NominalAmount - X.NominalAmount)    
                                          ELSE 0    
                                     END    
              FROM   ##Temp_WageTransaction A    
                     INNER JOIN ##Temp_WageTransactionItem B    
                          ON  A.WageTransactionID = B.WageTransactionID    
                     INNER JOIN (    
                              SELECT C.PersonID,    
                                     D.qty,    
                                     D.NominalAmount,    
                                     D.CurrencyAmount    
                              FROM   ##Temp_WageTransaction C    
                                     INNER JOIN ##Temp_WageTransactionItem D    
                                          ON  C.WageTransactionID = D.WageTransactionID    
                              WHERE  D.SalaryComponentID = @Ptkp    
                          ) X    
                          ON  A.PersonID = X.PersonID    
                     INNER JOIN (    
                              SELECT C.PersonID,    
                                     D.qty,    
                                     D.NominalAmount,    
                                     D.CurrencyAmount    
                              FROM   ##Temp_WageTransaction C    
                                     INNER JOIN ##Temp_WageTransactionItem D    
                                          ON  C.WageTransactionID = D.WageTransactionID    
                              WHERE  D.SalaryComponentID = @GajiNettoSetahun    
                          ) Y    
                          ON  A.PersonID = Y.PersonID    
              WHERE  B.SalaryComponentID = @Pkp    
          END          
              
          IF @SalaryComponentID = @PkpThr    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = CASE     
                                          WHEN (Y.NominalAmount > X.NominalAmount) THEN (Y.NominalAmount - X.NominalAmount)    
                                          ELSE 0    
                                     END    
              FROM   ##Temp_WageTransaction A    
                     INNER JOIN ##Temp_WageTransactionItem B    
                          ON  A.WageTransactionID = B.WageTransactionID    
                     INNER JOIN (    
                              SELECT C.PersonID,    
                                     D.qty,    
                                     D.NominalAmount,    
                                     D.CurrencyAmount    
                              FROM   ##Temp_WageTransaction C    
							  INNER JOIN ##Temp_WageTransactionItem D    
                                          ON  C.WageTransactionID = D.WageTransactionID    
                              WHERE  D.SalaryComponentID = @Ptkp    
                          ) X    
                          ON  A.PersonID = X.PersonID    
                     INNER JOIN (    
                              SELECT C.PersonID,    
                                     D.qty,    
                                     D.NominalAmount,    
                                     D.CurrencyAmount    
                              FROM   ##Temp_WageTransaction C    
                                     INNER JOIN ##Temp_WageTransactionItem D    
                                          ON  C.WageTransactionID = D.WageTransactionID    
                              WHERE  D.SalaryComponentID = @GajiNettoSetahunThr    
                          ) Y    
                          ON  A.PersonID = Y.PersonID    
              WHERE  B.SalaryComponentID = @PkpThr    
          END     
              
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------    
          -- 6 > Komponen pph21 SETAHUN    
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------              
          IF @SalaryComponentID = @pph21thn    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    NominalAmount = X.NominalAmount    
              FROM   (    
                         SELECT A.PersonID,    
                                B.NominalAmount,    
                                B.SalaryComponentID    
                         FROM   ##Temp_WageTransaction A    
                                INNER JOIN ##Temp_WageTransactionItem B    
                                     ON  A.WageTransactionID = B.WageTransactionID    
                                     AND B.SalaryComponentID = @Pkp    
                     ) X    
                     INNER JOIN ##Temp_WageTransaction D    
                          ON  X.PersonID = D.PersonID    
                     INNER JOIN ##Temp_WageTransactionItem E    
                          ON  D.WageTransactionID = E.WageTransactionID    
                          AND E.SalaryComponentID = @pph21thn     
              -----------------------------------------------------------------------------------                  
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = (    
                         (    
                             X.NominalAmount -(    
                                 CASE     
                                      WHEN B.LowerLimit = 0 THEN B.LowerLimit    
                                      ELSE (B.LowerLimit - 1)    
                                 END    
                             )    
                         ) * (B.TaxRate / 100)    
                     )     
                     + (    
         CASE     
                              WHEN B.AmountOfDeduction = 0 THEN B.TaxAmount    
                              ELSE (B.AmountOfDeduction - B.TaxAmount)    
                         END    
                     )    
              FROM   (    
                         SELECT A.PersonID,    
                                B.NominalAmount    
                         FROM   ##Temp_WageTransaction A    
                                INNER JOIN ##Temp_WageTransactionItem B    
                                     ON  A.WageTransactionID = B.WageTransactionID    
                                     AND B.SalaryComponentID = @pph21thn    
                     ) X    
                     INNER JOIN Pkp B WITH (NOLOCK)    
                          ON  X.NominalAmount BETWEEN LowerLimit AND UpperLimit    
                     INNER JOIN ##Temp_WageTransaction D    
                          ON  X.PersonID = D.PersonID AND B.IsNPWP = D.IsNPWP
                     INNER JOIN ##Temp_WageTransactionItem E    
                          ON  D.WageTransactionID = E.WageTransactionID    
                          AND E.SalaryComponentID = @pph21thn    
          END           
              
          IF @SalaryComponentID = @Pph21thnThr    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    NominalAmount = X.NominalAmount    
              FROM   (    
                         SELECT A.PersonID,    
                                B.NominalAmount,    
                                B.SalaryComponentID    
                         FROM   ##Temp_WageTransaction A    
                                INNER JOIN ##Temp_WageTransactionItem B    
                                     ON  A.WageTransactionID = B.WageTransactionID    
                                     AND B.SalaryComponentID = @PkpThr    
                     ) X    
                     INNER JOIN ##Temp_WageTransaction D    
                          ON  X.PersonID = D.PersonID    
                     INNER JOIN ##Temp_WageTransactionItem E    
                          ON  D.WageTransactionID = E.WageTransactionID    
                          AND E.SalaryComponentID = @Pph21thnThr     
              -----------------------------------------------------------------------------------                  
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = (    
                         (    
                             X.NominalAmount -(    
                                 CASE     
                                      WHEN B.LowerLimit = 0 THEN B.LowerLimit    
                                      ELSE (B.LowerLimit - 1)    
                                 END    
                             )    
                         ) * (B.TaxRate / 100)    
                     )     
                     + (    
                         CASE     
                              WHEN B.AmountOfDeduction = 0 THEN B.TaxAmount    
                              ELSE (B.AmountOfDeduction - B.TaxAmount)    
                         END    
                     )    
              FROM   (    
                         SELECT A.PersonID,    
                                B.NominalAmount    
                         FROM   ##Temp_WageTransaction A    
                                INNER JOIN ##Temp_WageTransactionItem B    
                                     ON  A.WageTransactionID = B.WageTransactionID    
                                     AND B.SalaryComponentID = @Pph21thnThr    
                     ) X    
                     INNER JOIN Pkp B WITH (NOLOCK)    
                          ON  X.NominalAmount BETWEEN LowerLimit AND     
                              UpperLimit    
                     INNER JOIN ##Temp_WageTransaction D    
                          ON  X.PersonID = D.PersonID    
                     INNER JOIN ##Temp_WageTransactionItem E    
                          ON  D.WageTransactionID = E.WageTransactionID    
                          AND E.SalaryComponentID = @Pph21thnThr    
          END     
              
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------    
          -- 7 > Komponen pph21 Sebulan    
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------              
          IF @SalaryComponentID = @Pph21bln    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = CASE     
                                          WHEN X.NominalAmount > 0 THEN X.NominalAmount    
                                          ELSE 0    
                                     END    
              FROM   (    
                         SELECT A.PersonID,    
                                B.Qty,    
                                B.CurrencyRate,    
                                (B.CurrencyAmount / 12) AS NominalAmount    
                         FROM   ##Temp_WageTransaction A    
                                INNER JOIN ##Temp_WageTransactionItem B    
                                     ON  A.WageTransactionID = B.WageTransactionID    
                                     AND B.SalaryComponentID = @pph21thn    
                     ) X    
                     INNER JOIN ##Temp_WageTransaction D    
                          ON  X.PersonID = D.PersonID    
                     INNER JOIN ##Temp_WageTransactionItem E    
                          ON  D.WageTransactionID = E.WageTransactionID    
                          AND E.SalaryComponentID = @pph21bln     
                                  
                              ---- update salary component pph21bulan u/ yg gak punya npwp dikalikan 120%    
                              --UPDATE ##Temp_WageTransactionItem    
                              --SET    NominalAmount = (B.NominalAmount * 1.2)    
                              --FROM   ##Temp_WageTransaction A    
                              --       INNER JOIN ##Temp_WageTransactionItem B    
                              --            ON  A.WageTransactionID = B.WageTransactionID    
                              --            AND B.SalaryComponentID = @pph21bln    
                              --WHERE A.IsNpwp = 0    
          END     
              
          --Pph21 Yayasan          
          IF @SalaryComponentID = @Pph21Ditanggung    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = CASE     
                                          WHEN X.NominalAmount >= ISNULL(rd.NominalAmount, 0) THEN ISNULL(rd.NominalAmount, 0)    
                                          ELSE X.NominalAmount    
                                     END    
              FROM   (    
                         SELECT A.PersonID,    
                                B.NominalAmount    
                         FROM   ##Temp_WageTransaction A    
                                INNER JOIN ##Temp_WageTransactionItem B    
                                     ON  A.WageTransactionID = B.WageTransactionID    
                                     AND B.SalaryComponentID = @Pph21bln    
                     ) X    
                     INNER JOIN ##Temp_WageTransaction D    
                          ON  X.PersonID = D.PersonID    
                     INNER JOIN ##Temp_WageTransactionItem E    
                          ON  D.WageTransactionID = E.WageTransactionID    
                          AND E.SalaryComponentID = @Pph21Ditanggung    
                     INNER JOIN SalaryComponent AS sc WITH (NOLOCK)    
                          ON  sc.SalaryComponentID = E.SalaryComponentID    
                     LEFT JOIN (    
                              SELECT scrl.*    
                              FROM   SalaryComponentRuleDefinition scrl WITH (NOLOCK)    
                              WHERE  scrl.SalaryComponentID = @Pph21Ditanggung    
                                     AND CONVERT(VARCHAR(10), scrl.ValidFrom, 112) <= CONVERT(VARCHAR(10), @date, 112)    
                                     AND CONVERT(VARCHAR(10), scrl.ValidTo, 112) >= CONVERT(VARCHAR(10), @date, 112)    
                          ) rd    
                          ON  D.OrganizationUnitID = rd.OrganizationUnitID    
          END     
              
          --Pph21 Karyawan          
          IF @SalaryComponentID = @Pph21Pribadi    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    Qty               = 1,    
                     NominalAmount     = CASE     
                                          WHEN X.NominalAmount >= ISNULL(rd.NominalAmount, 0) THEN X.NominalAmount     
                                               - ISNULL(Y.NominalAmount, 0)    
                                          ELSE 0    
                                     END    
              FROM   (    
                         SELECT A.PersonID,    
                                B.NominalAmount    
                         FROM   ##Temp_WageTransaction A    
                                INNER JOIN ##Temp_WageTransactionItem B    
                                     ON  A.WageTransactionID = B.WageTransactionID    
                                     AND B.SalaryComponentID = @Pph21bln    
                     ) X    
                     LEFT JOIN (    
                              SELECT A.PersonID,    
                                     B.NominalAmount    
                              FROM   ##Temp_WageTransaction A    
                                     INNER JOIN ##Temp_WageTransactionItem B    
                                          ON  A.WageTransactionID = B.WageTransactionID    
                                          AND B.SalaryComponentID = @Pph21Ditanggung    
                          ) Y    
                          ON  X.PersonID = Y.PersonID    
                     INNER JOIN ##Temp_WageTransaction D    
                          ON  X.PersonID = D.PersonID    
                     INNER JOIN ##Temp_WageTransactionItem E    
                          ON  D.WageTransactionID = E.WageTransactionID    
                          AND E.SalaryComponentID = @Pph21Pribadi    
                     INNER JOIN SalaryComponent AS sc WITH (NOLOCK)    
                          ON  sc.SalaryComponentID = E.SalaryComponentID    
                     LEFT JOIN (    
                              SELECT scrl.*    
                              FROM   SalaryComponentRuleDefinition scrl WITH (NOLOCK)    
                              WHERE  scrl.SalaryComponentID = @Pph21Ditanggung    
                                     AND CONVERT(VARCHAR(10), scrl.ValidFrom, 112) <= CONVERT(VARCHAR(10), @date, 112)    
                                     AND CONVERT(VARCHAR(10), scrl.ValidTo, 112) >= CONVERT(VARCHAR(10), @date, 112)    
                          ) rd    
                          ON  D.OrganizationUnitID = rd.OrganizationUnitID    
          END     
              
          --Pph21 DTP (bantuan pemerintah)          
          IF @SalaryComponentID = @Pph21DTP    
          BEGIN    
              IF (@InsentifPPh21DTP = '0')    
              BEGIN    
                  -- nolkan jika tidak ada bantuan sesuai settingan di appparameter          
                  UPDATE ##Temp_WageTransactionItem    
                  SET    NominalAmount = 0    
                  FROM   ##Temp_WageTransaction A    
                         INNER JOIN ##Temp_WageTransactionItem B    
                              ON  A.WageTransactionID = B.WageTransactionID    
                              AND B.SalaryComponentID = @Pph21DTP    
              END    
              ELSE    
              BEGIN    
                  -- nolkan jika u/ pendapatan per tahun lebih dari 200jt (settingan appparameter) atau karyawan tidak punya NPWP       
                  UPDATE ##Temp_WageTransactionItem    
                  SET    NominalAmount = 0    
                  FROM   ##Temp_WageTransaction A    
                         INNER JOIN ##Temp_WageTransactionItem B    
                              ON  A.WageTransactionID = B.WageTransactionID    
                              AND B.SalaryComponentID = @Pph21DTP    
                         INNER JOIN (    
                                  SELECT A.PersonID,    
                                         B.NominalAmount    
                                  FROM   ##Temp_WageTransaction A    
                                         INNER JOIN ##Temp_WageTransactionItem B    
                                              ON  A.WageTransactionID = B.WageTransactionID    
                                              AND B.SalaryComponentID = @GajiBruttoSetahunDtp    
                                  WHERE  (    
                                             B.NominalAmount > CAST(@InsentifPPh21DTP AS NUMERIC(18, 2))    
                                             OR A.IsNpwp = 0    
                                         )    
                              ) x    
                              ON  x.PersonID = A.PersonID    
              END    
          END          
              
          IF @SalaryComponentID = @Pph21ThrDTP    
          BEGIN    
              IF (@InsentifPPh21DTP = '0')    
              BEGIN    
                  -- nolkan jika tidak ada bantuan sesuai settingan di appparameter          
                  UPDATE ##Temp_WageTransactionItem    
                  SET    NominalAmount = 0    
                  FROM   ##Temp_WageTransaction A    
                         INNER JOIN ##Temp_WageTransactionItem B    
                              ON  A.WageTransactionID = B.WageTransactionID    
                              AND B.SalaryComponentID = @Pph21ThrDTP    
              END    
              ELSE    
              BEGIN    
                  -- nolkan jika u/ pendapatan per tahun lebih dari 200jt (settingan appparameter)          
                  UPDATE ##Temp_WageTransactionItem    
                  SET    NominalAmount = 0    
                  FROM   ##Temp_WageTransaction A    
                         INNER JOIN ##Temp_WageTransactionItem B    
                              ON  A.WageTransactionID = B.WageTransactionID    
                              AND B.SalaryComponentID = @Pph21ThrDTP    
                         INNER JOIN (    
                                  SELECT A.PersonID,    
                                         B.NominalAmount    
                                  FROM   ##Temp_WageTransaction A    
                                         INNER JOIN ##Temp_WageTransactionItem B    
                                              ON  A.WageTransactionID = B.WageTransactionID    
                                              AND B.SalaryComponentID = @GajiNettoSetahunThr    
                                  WHERE  B.NominalAmount > CAST(@InsentifPPh21DTP AS NUMERIC(18, 2))    
                              ) x    
                              ON  x.PersonID = A.PersonID    
              END    
          END  
          
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------    
          -- 7.b > Komponen pph21 - th 2024    
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------   
          --@TER_BrutoSebulan INT,
          --@TER_Pengurang INT,
          --@TER_NettoSetahun INT,
          
			IF (@SPTMonth = 12 AND @p_WageProcessTypeID = 1) 
			BEGIN
				-- masa pajak terakhir (des)
				
				--@TER_BiayaJabatan, cek masa kerja, jika < 12 maka proporsional
				IF (@SalaryComponentID = @TER_BiayaJabatan)
          		BEGIN
          			UPDATE a SET a.NominalAmount = CASE     
                                          WHEN DATEDIFF(MONTH, vet.JoinDate, @date) >= 12 THEN a.NominalAmount  
                                          ELSE CASE 
											WHEN a.NominalAmount < (sc.MaxAmount / 12) * (DATEDIFF(MONTH, vet.JoinDate, @date)) THEN a.NominalAmount 
											ELSE (sc.MaxAmount / 12) * (DATEDIFF(MONTH, vet.JoinDate, @date)) 
                                          END  
                                     END
          			FROM  ##Temp_WageTransactionItem a 
          			INNER JOIN ##Temp_WageTransaction b 
          					ON b.WageTransactionID = a.WageTransactionID 
          					AND a.SalaryComponentID = @TER_BiayaJabatan 
          			INNER JOIN Vw_EmployeeTable AS vet WITH (NOLOCK)    
                          ON  vet.PersonID = b.PersonID 
                    INNER JOIN SalaryComponent AS sc ON sc.SalaryComponentID = a.SalaryComponentID
          					
          		END
          		
          		--@TER_PKP
          		IF (@SalaryComponentID = @TER_PKP)
          		BEGIN 
          			UPDATE B 
          			SET B.Qty = 1, 
          				B.NominalAmount =	CASE 
          										WHEN (Y.NominalAmount > X.NominalAmount) THEN (Y.NominalAmount - X.NominalAmount)
          										ELSE 0 
          									END 
          			FROM ##Temp_WageTransaction A 
          			INNER JOIN ##Temp_WageTransactionItem B ON A.WageTransactionID = B.WageTransactionID 
          			INNER JOIN (    
								  SELECT C.PersonID,    
										 D.qty,    
										 D.NominalAmount,    
										 D.CurrencyAmount    
								  FROM   ##Temp_WageTransaction C    
										 INNER JOIN ##Temp_WageTransactionItem D    
											  ON  C.WageTransactionID = D.WageTransactionID    
								  WHERE  D.SalaryComponentID = @TER_PKP    
							  ) X    
							  ON  A.PersonID = X.PersonID
					INNER JOIN (    
								  SELECT C.PersonID,    
										 D.qty,    
										 D.NominalAmount,    
										 D.CurrencyAmount    
								  FROM   ##Temp_WageTransaction C    
										 INNER JOIN ##Temp_WageTransactionItem D    
											  ON  C.WageTransactionID = D.WageTransactionID    
								  WHERE  D.SalaryComponentID = @TER_NettoSetahun    
							  ) Y    
							  ON  A.PersonID = Y.PersonID    
					WHERE  B.SalaryComponentID = @TER_PKP    
				END
          		
          		--@TER_Pph21_1
          		IF (@SalaryComponentID = @TER_Pph21_1 OR @SalaryComponentID = @TER_Pph21_Thr)
          		BEGIN
          			UPDATE b SET b.NominalAmount = 0
          			FROM  ##Temp_WageTransaction a 
          			INNER JOIN ##Temp_WageTransactionItem b 
          					ON a.WageTransactionID = b.WageTransactionID 
          					AND b.SalaryComponentID = @TER_Pph21_1 
          		END
          		
          		--@TER_AkuPph21_1
          		IF (@SalaryComponentID = @TER_AkuPph21_1)
				BEGIN
          			UPDATE b SET b.NominalAmount = ISNULL(etc.TaxAmount, 0) + bb.NominalAmount
          			FROM  ##Temp_WageTransaction AS a 
          			INNER JOIN ##Temp_WageTransactionItem AS b 
          					ON a.WageTransactionID = b.WageTransactionID 
          					AND b.SalaryComponentID = @TER_AkuPph21_1 
          			LEFT JOIN (SELECT etc.PersonID, SUM(etc.TaxAmount) AS TaxAmount
          					   FROM EmployeeTaxCalculation AS etc WHERE etc.SPTYear = @SPTYear AND (etc.SPTMonth < @SPTMonth OR etc.WageProcessTypeID = 2)
          					   GROUP BY etc.PersonID) AS etc ON etc.PersonID = a.PersonID
          			INNER JOIN ##Temp_WageTransactionItem AS bb ON bb.WageTransactionID = a.WageTransactionID 
          				AND bb.SalaryComponentID = @TER_Pph21_1
				END
				
				--@TER_Pph21Setahun
				IF @SalaryComponentID = @TER_Pph21Setahun 
				BEGIN 
					UPDATE ##Temp_WageTransactionItem 
					SET    NominalAmount = X.NominalAmount 
					FROM   (    
                         SELECT A.PersonID,    
                                B.NominalAmount,    
                                B.SalaryComponentID    
                         FROM   ##Temp_WageTransaction A    
                                INNER JOIN ##Temp_WageTransactionItem B    
                                     ON  A.WageTransactionID = B.WageTransactionID    
                                     AND B.SalaryComponentID = @TER_PKP
                     ) X    
                     INNER JOIN ##Temp_WageTransaction D    
                          ON  X.PersonID = D.PersonID    
                     INNER JOIN ##Temp_WageTransactionItem E    
                          ON  D.WageTransactionID = E.WageTransactionID    
                          AND E.SalaryComponentID = @TER_Pph21Setahun     
					-----------------------------------------------------------------------------------  
					
					UPDATE ##Temp_WageTransactionItem 
					SET Qty               = 1, 
						NominalAmount     = (    
							 (    
								 X.NominalAmount -(    
									 CASE     
										  WHEN B.LowerLimit = 0 THEN B.LowerLimit    
										  ELSE (B.LowerLimit - 1)    
									 END    
								 )    
							 ) * (B.TaxRate / 100)    
						 )     
						 + ( 
						 CASE     
                              WHEN B.AmountOfDeduction = 0 THEN B.TaxAmount    
                              ELSE (B.AmountOfDeduction - B.TaxAmount)    
                         END) 
                     FROM   (    
                         SELECT A.PersonID,    
                                B.NominalAmount    
                         FROM   ##Temp_WageTransaction A    
                                INNER JOIN ##Temp_WageTransactionItem B    
                                     ON  A.WageTransactionID = B.WageTransactionID    
                                     AND B.SalaryComponentID = @TER_Pph21Setahun    
                     ) X    
                     INNER JOIN Pkp B WITH (NOLOCK) 
                          ON  X.NominalAmount BETWEEN LowerLimit AND UpperLimit 
                     INNER JOIN ##Temp_WageTransaction D 
                          ON  X.PersonID = D.PersonID AND B.IsNPWP = D.IsNPWP 
                     INNER JOIN ##Temp_WageTransactionItem E 
                          ON  D.WageTransactionID = E.WageTransactionID 
                          AND E.SalaryComponentID = @TER_Pph21Setahun 
				END
          		
			END
			ELSE
			BEGIN
				-- masa pajak jan - nov atau pajak THR
				
				-- @TER_Tarif
				IF (@SalaryComponentID = @TER_Tarif)
				BEGIN
					IF (@p_WageProcessTypeID = 1)
					BEGIN
						UPDATE b SET b.NominalAmount = ISNULL((SELECT ti.TaxRate FROM TERMonthlyItem AS ti 
											INNER JOIN (SELECT TOP 1 t.TERMonthlyID FROM TERMonthly AS t WHERE t.SRTaxStatus = esi.SRTaxStatus AND t.ValidFrom <= @date ORDER BY t.ValidFrom DESC) AS t 
												ON t.TERMonthlyID = ti.TERMonthlyID 
          									WHERE ti.LowerLimit <= bb.NominalAmount AND ti.UpperLimit >= bb.NominalAmount), 0)
          				FROM  ##Temp_WageTransaction a 
          				INNER JOIN ##Temp_WageTransactionItem b 
          						ON a.WageTransactionID = b.WageTransactionID 
          						AND b.SalaryComponentID = @TER_Tarif 
          				INNER JOIN EmployeeSalaryInfo AS esi ON esi.PersonID = a.PersonID
          				INNER JOIN ##Temp_WageTransactionItem bb ON bb.WageTransactionID = a.WageTransactionID 
          					AND bb.SalaryComponentID = @TER_BrutoSebulan
					END
					ELSE
					BEGIN
						UPDATE b SET b.NominalAmount = ISNULL((SELECT ti.TaxRate FROM TERMonthlyItem AS ti 
											INNER JOIN (SELECT TOP 1 t.TERMonthlyID FROM TERMonthly AS t WHERE t.SRTaxStatus = esi.SRTaxStatus AND t.ValidFrom <= @date ORDER BY t.ValidFrom DESC) AS t 
												ON t.TERMonthlyID = ti.TERMonthlyID 
          									WHERE ti.LowerLimit <= bb.NominalAmount AND ti.UpperLimit >= bb.NominalAmount), 0)
          				FROM  ##Temp_WageTransaction a 
          				INNER JOIN ##Temp_WageTransactionItem b 
          						ON a.WageTransactionID = b.WageTransactionID 
          						AND b.SalaryComponentID = @TER_Tarif 
          				INNER JOIN EmployeeSalaryInfo AS esi ON esi.PersonID = a.PersonID
          				INNER JOIN ##Temp_WageTransactionItem bb ON bb.WageTransactionID = a.WageTransactionID 
          					AND bb.SalaryComponentID = @Thr
					END
				END
				
				-- @TER_BiayaJabatan, @TER_PKP, @TER_AkuPph21_1, @TER_Pph21Setahun --> di-nol-kan
				IF (@SalaryComponentID = @TER_BiayaJabatan OR 
					@SalaryComponentID = @TER_PKP OR 
					@SalaryComponentID = @TER_AkuPph21_1 OR 
					@SalaryComponentID = @TER_Pph21Setahun OR 
					@SalaryComponentID = @TER_Pph21_2)
					
          		BEGIN
          			UPDATE b SET b.NominalAmount = 0
          			FROM  ##Temp_WageTransaction a 
          			INNER JOIN ##Temp_WageTransactionItem b 
          					ON a.WageTransactionID = b.WageTransactionID 
          					AND b.SalaryComponentID = @SalaryComponentID 
          		END
          		
          		IF (@p_WageProcessTypeID = 2 AND @SalaryComponentID = @TER_Pengurang)
          		BEGIN
          			UPDATE b SET b.NominalAmount = 0
          			FROM  ##Temp_WageTransaction a 
          			INNER JOIN ##Temp_WageTransactionItem b 
          					ON a.WageTransactionID = b.WageTransactionID 
          					AND b.SalaryComponentID = @SalaryComponentID 
          		END
			END
		  
          -- ALL MASA PAJAK  
          IF (@SalaryComponentID = @TER_BrutoSetahun)
		  BEGIN
				UPDATE b SET b.NominalAmount = ISNULL(etc.GrossIncome, 0) + bb.NominalAmount
          		FROM  ##Temp_WageTransaction AS a 
          		INNER JOIN ##Temp_WageTransactionItem AS b 
          				ON a.WageTransactionID = b.WageTransactionID 
          				AND b.SalaryComponentID = @TER_BrutoSetahun 
          		LEFT JOIN (SELECT etc.PersonID, SUM(etc.GrossIncome) AS GrossIncome
          		           FROM EmployeeTaxCalculation AS etc WHERE etc.SPTYear = @SPTYear AND (etc.SPTMonth < @SPTMonth OR etc.WageProcessTypeID = 2)
          		           GROUP BY etc.PersonID) AS etc ON etc.PersonID = a.PersonID
          		INNER JOIN ##Temp_WageTransactionItem AS bb ON bb.WageTransactionID = a.WageTransactionID 
          			AND bb.SalaryComponentID = @TER_BrutoSebulan
		  END
          
		  IF (@SalaryComponentID = @TER_AkuPengurang)
		  BEGIN
          		UPDATE b SET b.NominalAmount = ISNULL(etc.Deduction, 0) + bb.NominalAmount
          		FROM  ##Temp_WageTransaction AS a 
          		INNER JOIN ##Temp_WageTransactionItem AS b 
          				ON a.WageTransactionID = b.WageTransactionID 
          				AND b.SalaryComponentID = @TER_AkuPengurang 
          		LEFT JOIN (SELECT etc.PersonID, SUM(etc.Deduction) AS Deduction
          		           FROM EmployeeTaxCalculation AS etc WHERE etc.SPTYear = @SPTYear AND etc.SPTMonth < @SPTMonth
          		           GROUP BY etc.PersonID) AS etc ON etc.PersonID = a.PersonID
          		INNER JOIN ##Temp_WageTransactionItem AS bb ON bb.WageTransactionID = a.WageTransactionID 
          			AND bb.SalaryComponentID = @TER_Pengurang
		  END
          
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------    
          -- 8 > Komponen Overtime    
          -----------------------------------------------------------------------------------    
          -----------------------------------------------------------------------------------            
              
          IF @SalaryComponentID = @Overtime    
          BEGIN    
              UPDATE ##Temp_WageTransactionItem    
              SET    NominalAmount = 0    
              FROM   ##Temp_WageTransactionItem X    
                     INNER JOIN ##Temp_WageTransaction D    
                          ON  X.WageTransactionID = D.WageTransactionID    
                     INNER JOIN SalaryComponent AS sc WITH (NOLOCK)    
                          ON  x.SalaryComponentID = sc.SalaryComponentID    
                          AND sc.SalaryComponentID = @Overtime    
              WHERE  D.PersonID NOT IN (SELECT eps.PersonID    
                                        FROM   EmployeePeriodicSalary AS eps WITH (NOLOCK)    
                                        WHERE  eps.PayrollPeriodID = @p_PayrollPeriodID    
                                               AND eps.SalaryComponentID = @Overtime)           
                  
              DECLARE @sql NVARCHAR(MAX)              
              SET @sql = 'UPDATE ##Temp_WageTransactionItem ' +    
                  +    
                  'SET    NominalAmount = eps.Amount * (scrd.NominalAmount * (ISNULL((SELECT eps.FromBasicSalaryAmount FROM EmployeePeriodicSalary AS eps '     
                  +    
                  + 'WHERE eps.PayrollPeriodID = ' + CAST(@p_PayrollPeriodID AS VARCHAR(MAX))     
                  + ' ' +    
                  + ' AND eps.PersonID = d.PersonID ' +    
                  +    
                  ' AND eps.SalaryComponentID = (SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = ''SalaryComponentIdForBasicSalary'') '     
                  +    
                  +    
                  ' AND eps.SRProcessType = (SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = ''ProcessTypePositionGrade'')), X.NominalAmount)'     
                  + @FaktorRuleDisplay + ')) ' +    
                  + 'FROM   ##Temp_WageTransactionItem X ' +    
                  + 'INNER JOIN ##Temp_WageTransaction D ' +    
                  + '    ON  X.WageTransactionID = D.WageTransactionID ' +    
                  + 'INNER JOIN SalaryComponent AS sc ' +    
                  + '    ON  x.SalaryComponentID = sc.SalaryComponentID '     
                  +    
                  + '    AND sc.SalaryComponentID = ' + CAST(@Overtime AS VARCHAR(MAX))     
                  + ' ' +    
                  + 'INNER JOIN SalaryComponentRuleDefinition AS scrd  ' +    
                  + ' ON scrd.SalaryComponentID = sc.SalaryComponentID ' +    
                  + 'INNER JOIN EmployeePeriodicSalary AS eps ' +    
                  + ' ON eps.PersonID = d.PersonID ' +    
                  + ' AND eps.PayrollPeriodID = ' + CAST(@p_PayrollPeriodID AS VARCHAR(MAX))     
                  + ' AND eps.PayrollPeriodID > 0 ' +    
                  + ' AND eps.SalaryComponentID = sc.SalaryComponentID'     
                  
              --PRINT (@sql)          
              EXEC (@sql)    
          END 
          
          -----------------------------------------------------------------------------------    
          -- 3.3 Hitung nilai min & max    
          -----------------------------------------------------------------------------------           
              
          BEGIN    
           UPDATE ##Temp_WageTransactionItem    
           SET    NominalAmount = CASE     
                                       WHEN (sc.MinAmount = 0 AND sc.MaxAmount = 0) OR sc.MinAmount = -1 THEN A.NominalAmount    
                                       WHEN sc.MinAmount > 0 AND A.NominalAmount < sc.MinAmount THEN sc.MinAmount    
                                       WHEN sc.MaxAmount > 0 AND A.NominalAmount > sc.MaxAmount THEN sc.MaxAmount    
                                       ELSE A.NominalAmount    
                                  END    
           FROM   ##Temp_WageTransactionItem A    
                INNER JOIN ##Temp_WageTransaction B    
                    ON  B.WageTransactionID = A.WageTransactionID    
                    AND A.SalaryComponentID = @SalaryComponentID  
                  INNER JOIN SalaryComponent AS sc WITH (NOLOCK)    
                       ON  sc.SalaryComponentID = A.SalaryComponentID    
          END      
              
          -----------------------------------------------------------------------------------    
          -- 9> CurrencyAmount  
          -----------------------------------------------------------------------------------              
          BEGIN    
			UPDATE ##Temp_WageTransactionItem    
			SET CurrencyAmount = (A.Qty * A.NominalAmount * A.CurrencyRate)
			FROM   ##Temp_WageTransactionItem A    
                INNER JOIN ##Temp_WageTransaction B    
                    ON  B.WageTransactionID = A.WageTransactionID    
                    AND A.SalaryComponentID = @SalaryComponentID    
		  END 
		  
		  -----------------------------------------------------------------------------------    
          -- 10> Rounding
          -----------------------------------------------------------------------------------   
          IF (@RoundingValue < 1)
		  BEGIN    
			UPDATE ##Temp_WageTransactionItem    
			SET NominalAmount = ROUND(A.NominalAmount, @RoundingValue, @RoundingValue2),  
				CurrencyAmount = ROUND(A.CurrencyAmount, @RoundingValue ,@RoundingValue2)
			FROM   ##Temp_WageTransactionItem A    
                INNER JOIN ##Temp_WageTransaction B    
                    ON  B.WageTransactionID = A.WageTransactionID    
                    AND A.SalaryComponentID = @SalaryComponentID    
		  END 
		  
		  -----------------------------------------------------------------------------------    
          -- 11> Update Gapok Bulan Lalu = Gapok skr jika nol
          -----------------------------------------------------------------------------------  
		  IF (@SalaryComponentID = @GapokBlnLalu)
		  BEGIN    
			UPDATE A    
			SET A.NominalAmount = C.NominalAmount,  
				A.CurrencyAmount = C.CurrencyAmount
			FROM   ##Temp_WageTransactionItem A    
                INNER JOIN ##Temp_WageTransaction B    
                    ON  B.WageTransactionID = A.WageTransactionID    
                    AND A.SalaryComponentID = @SalaryComponentID 
                INNER JOIN ##Temp_WageTransactionItem C ON C.WageTransactionID = B.WageTransactionID AND C.SalaryComponentID = @Gapok  
			WHERE A.NominalAmount = 0
		  END 
		      
      END     
          
      -----------------------------------------------------------------------------------              
          
      FETCH NEXT FROM SalaryComponent_Cursor INTO     
      @SalaryComponentID,     
      @SRSalaryType,     
      @IsOrganizationUnit,     
      @IsEmployeeStatus,     
      @IsPosition,     
      @IsReligion,     
      @IsEmployee,     
      @IsEmploymentType,     
      @IsPositionGrade,     
      @IsMaritalStatus,     
      @IsServiceYear,     
      @IsSalaryTableNumber,     
      @IsEmployeeGrade,     
      @IsNoOfDependent,     
      @IsAttedanceMatrixID,     
      @IsKWI,     
      @IsEducationLevel,     
      @IsEmployeeType,     
      @IsServiceUnitID,     
      @FaktorRuleDisplay,     
      @RoundingValue,
      @RoundingValue2  
  END     
  CLOSE SalaryComponent_Cursor     
  DEALLOCATE SalaryComponent_Cursor    
 END 