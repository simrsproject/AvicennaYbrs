/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.0.35
 * Time: 10/13/2021 7:59:55 AM
 ************************************************************/

/* SP INI JANGAN  DIUPDATE DI RSIAMTP karena di RSIAMTP menggunakan profitloss bulanan */ 

--DECLARE @a MONEY,
--		@b MONEY
--EXEC [spRpt_CashManagement_CashFlow_IndirectGeneralV2] '09','2023', @a OUTPUT, @b OUTPUT
--SELECT @a, @b
 
ALTER PROCEDURE [dbo].[spRpt_CashManagement_CashFlow_IndirectGeneralV2] --'07','2012','2961555462','2256820859'      
(
    @pSinglePostingPeriode_Month     CHAR(2),
    @pSinglePostingPeriode_Year      CHAR(4),
    @pBeginingBalance                MONEY OUTPUT,
    @pEndingBalance                  MONEY OUTPUT
)
AS
--BEGIN
	/*       
	Revision History:       
	4/18/2012: Initial Version (AN). @IncomeSummaryAccount harus dirubah sesuai dengan noperkiraan jurnal balik ikhtisar rugi/laba       
	
	
	-- Usage:        
	DECLARE @RC int       
	DECLARE @pSinglePostingPeriode_Month char(2)       
	DECLARE @pSinglePostingPeriode_Year char(4)       
	DECLARE @pBeginingBalance money       
	DECLARE @pEndingBalance money       
	
	set @pSinglePostingPeriode_Month = '12'       
	set @pSinglePostingPeriode_Year = '2012'       
	
	EXECUTE @RC = [spRpt_CashManagement_CashFlow_IndirectGeneral] @pSinglePostingPeriode_Month,@pSinglePostingPeriode_Year,@pBeginingBalance OUTPUT,@pEndingBalance OUTPUT       
	
	select @pBeginingBalance, @pEndingBalance         
	*/       
	
	--DECLARE     
	--	@pSinglePostingPeriode_Month     CHAR(2)	= '08',
	--	@pSinglePostingPeriode_Year      CHAR(4)	= '2023',
	--	@pBeginingBalance                MONEY = 0,
	--	@pEndingBalance                  MONEY = 0
	
	SET NOCOUNT ON       
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED       
	BEGIN
		DECLARE @TempTable TABLE (
		            [RowID] [int] IDENTITY,
		            [ChartOfAccountCode] [char] (15),
		            [GeneralAccount] [char] (15),
		            [ChartOfAccountName] [nvarchar] (100),
		            [NormalBalance] [nvarchar] (25),
		            [Month] [char](2),
		            [Year] [char](4),
		            [Balance] [money],
		            [ActivityId] [int],
		            [Activities] [nvarchar] (50),
		            [CategoryId] [int],
		            [Category] [nvarchar] (50)
		        )       
		
		
		DECLARE @netIncome [money]       
		SET @netIncome = 0       
		SET @pBeginingBalance = 0       
		SET @pEndingBalance = 0 
		
		-- Net Income
		-- TODO: Read NetIncome from FinalBalance or current period mutation only? sudah diimplementasikan dengan parameter Acc_IsMonthlyNetIncomeForReport [20231019 BT]
		--       
		
		IF((SELECT COUNT(ap.ParameterID)
		FROM AppParameter AS ap WHERE ap.ParameterID = 'Acc_IsMonthlyNetIncomeForReport') = 0)
		BEGIN
			INSERT INTO AppParameter
			(
				ParameterID,
				ParameterName,
				ParameterValue,
				ParameterType,
				LastUpdateDateTime,
				LastUpdateByUserID,
				IsUsedBySystem
			)
			VALUES
			(
				'Acc_IsMonthlyNetIncomeForReport'/*{ ParameterID }*/,
				'Acc_IsMonthlyNetIncomeForReport'/*{ ParameterName }*/,
				'Yes'/*{ ParameterValue }*/,
				''/*{ ParameterType }*/,
				GETDATE()/*{ LastUpdateDateTime }*/,
				'sci'/*{ LastUpdateByUserID }*/,
				0/*{ IsUsedBySystem }*/
			)
		END
		
		DECLARE @IsMonthlyNetIncome BIT = 0
		SELECT @IsMonthlyNetIncome = CASE LOWER(ap.ParameterValue) WHEN 'yes' THEN 1 ELSE 0 END
		FROM AppParameter AS ap WHERE ap.ParameterID = 'Acc_IsMonthlyNetIncomeForReport'
		
		--SET @IsMonthlyNetIncome = 0
		
		SELECT @netIncome = SUM(
		           CASE 
		                WHEN c.NormalBalance = 'K' THEN (CASE @IsMonthlyNetIncome WHEN 1 THEN (a.CreditAmount - a.DebitAmount) ELSE a.[FinalBalance] END)
		                ELSE (CASE @IsMonthlyNetIncome WHEN 1 THEN (a.CreditAmount - a.DebitAmount) ELSE (a.[FinalBalance] * -1) END)
		                --WHEN c.NormalBalance = 'K' THEN a.[FinalBalance]
		                --ELSE a.[FinalBalance] * -1
		           END
		       )
		FROM   ChartOfAccountBalances a
		       INNER JOIN ChartOfAccounts c
		            ON  a.ChartOfAccountId = c.ChartOfAccountId
		WHERE  a.Year = @pSinglePostingPeriode_Year
		       AND a.Month = @pSinglePostingPeriode_Month
		       AND c.AccountGroup IN (22, 24, 26, 28, 30, 32)         
		
		INSERT INTO @TempTable
		VALUES
		  (
		    '',
		    'Net Income',
		    'Net Income - From Profit Loss Report',
		    'Profit/Loss',
		    @pSinglePostingPeriode_Month,
		    @pSinglePostingPeriode_Year,
		    @netIncome,
		    1,
		    'Operating Activities',
		    1,
		    'Net Income'
		  ) 
		
		-- Accumulated Depreciation
		-- Accounts on AccountGroup: 4 with NormalBalance: K supposed to be depreciation accounts       
		INSERT INTO @TempTable
		SELECT c.ChartOfAccountCode,
		       c.GeneralAccount,
		       c.ChartOfAccountName,
		       'Added',
		       @pSinglePostingPeriode_Month,
		       @pSinglePostingPeriode_Year,
		       a.CreditAmount -a.DebitAmount,
		       1,
		       'Operating Activities',
		       2,
		       'Accumulated Depreciation'
		FROM   ChartOfAccountBalances a
		       INNER JOIN ChartOfAccounts c
		            ON  a.ChartOfAccountId = c.ChartOfAccountId
		WHERE  a.Year = @pSinglePostingPeriode_Year
		       AND a.Month = @pSinglePostingPeriode_Month
		       AND c.AccountGroup IN (4)
		       AND c.NormalBalance = 'K' 
		
		-- Asset. All accounts other then fix asset/depreciation. Also need to exclude Bank/Cash account.
		--       
		INSERT INTO @TempTable
		SELECT c.ChartOfAccountCode,
		       c.GeneralAccount,
		       c.ChartOfAccountName,
		       CASE 
		            WHEN (a.DebitAmount -a.CreditAmount) * -1 < 0 THEN 'Deduct'
		            ELSE 'Added'
		       END,
		       @pSinglePostingPeriode_Month,
		       @pSinglePostingPeriode_Year,
		       (a.DebitAmount -a.CreditAmount) * -1,
		       1,
		       'Operating Activities',
		       3,
		       'Except Depreciation'
		FROM   ChartOfAccountBalances a
		       INNER JOIN (
		                SELECT *
		                FROM   ChartOfAccounts
		                WHERE  ChartOfAccountId NOT IN --(SELECT ChartOfAccountId FROM Bank)
		                (
		                	/* untuk meng-cover konsolidasi */
							SELECT DISTINCT coa.ChartOfAccountId
							FROM Bank AS b
								INNER JOIN ChartOfAccounts AS coa ON coa.ChartOfAccountId = b.ChartOfAccountId
								INNER JOIN ChartOfAccounts AS coa2 ON coa.GeneralAccount = coa2.ChartOfAccountCode
								INNER JOIN ChartOfAccounts AS coa3 ON coa2.GeneralAccount = coa3.ChartOfAccountCode
							WHERE b.BankName NOT LIKE '%ayat silang%'
		                )
		            ) c
		            ON  a.ChartOfAccountId = c.ChartOfAccountId
		WHERE  a.Year = @pSinglePostingPeriode_Year
		       AND a.Month = @pSinglePostingPeriode_Month
		       AND c.AccountGroup IN (2, 6) 
		
		-- Liabilities.
		--       
		INSERT INTO @TempTable
		SELECT c.ChartOfAccountCode,
		       c.GeneralAccount,
		       c.ChartOfAccountName,
		       CASE 
		            WHEN (a.CreditAmount -a.DebitAmount) < 0 THEN 'Deduct'
		            ELSE 'Added'
		       END,
		       @pSinglePostingPeriode_Month,
		       @pSinglePostingPeriode_Year,
		       (a.CreditAmount -a.DebitAmount),
		       1,
		       'Operating Activities',
		       3,
		       'Except Depreciation'
		FROM   ChartOfAccountBalances a
		       INNER JOIN ChartOfAccounts c
		            ON  a.ChartOfAccountId = c.ChartOfAccountId
		WHERE  a.Year = @pSinglePostingPeriode_Year
		       AND a.Month = @pSinglePostingPeriode_Month
		       AND c.AccountGroup IN (12, 14) 
		
		-- Investing from FixAsset
		-- Find fixAsset accounts only, not depreciation account.       
		INSERT INTO @TempTable
		SELECT c.ChartOfAccountCode,
		       c.GeneralAccount,
		       c.ChartOfAccountName,
		       CASE 
		            WHEN (a.DebitAmount -a.CreditAmount) * -1 < 0 THEN 'Deduct'
		            ELSE 'Added'
		       END,
		       @pSinglePostingPeriode_Month,
		       @pSinglePostingPeriode_Year,
		       (a.DebitAmount -a.CreditAmount) * -1,
		       2,
		       'Investing',
		       4,
		       'Investing'
		FROM   ChartOfAccountBalances a
		       INNER JOIN ChartOfAccounts c
		            ON  a.ChartOfAccountId = c.ChartOfAccountId
		WHERE  a.Year = @pSinglePostingPeriode_Year
		       AND a.Month = @pSinglePostingPeriode_Month
		       AND c.AccountGroup IN (4)
		       AND c.NormalBalance = 'D' 
		
		-- Financing. Must Exclude Income Summary Account
		-- TODO: Read Income Summary Account from table
		--        
		DECLARE @IncomeSummaryAccount CHAR(15)             
		SET @IncomeSummaryAccount = (
		        SELECT ParameterValue
		        FROM   AppParameter WITH (NOLOCK)
		        WHERE  ParameterID = 'coa_RetainedEarning'
		    )    
		
		INSERT INTO @TempTable
		SELECT c.ChartOfAccountCode,
		       c.GeneralAccount,
		       c.ChartOfAccountName,
		       CASE 
		            WHEN (a.CreditAmount -a.DebitAmount) < 0 THEN 'Deduct'
		            ELSE 'Added'
		       END,
		       @pSinglePostingPeriode_Month,
		       @pSinglePostingPeriode_Year,
		       (a.CreditAmount -a.DebitAmount) -(
		           CASE 
		                WHEN c.ChartOfAccountCode = @IncomeSummaryAccount THEN @netIncome
		                ELSE 0
		           END
		       ),
		       3,
		       'Financing',
		       5,
		       'Financing'
		FROM   ChartOfAccountBalances a
		       INNER JOIN ChartOfAccounts c
		            ON  a.ChartOfAccountId = c.ChartOfAccountId
		WHERE  a.Year = @pSinglePostingPeriode_Year
		       AND a.Month = @pSinglePostingPeriode_Month
		       AND c.AccountGroup IN (16)        
		
		-- remove added / deduct
		UPDATE @TempTable set [NormalBalance] = ''
		
		SELECT a.*,
		       CASE 
		            WHEN a.RowID = 1 THEN 'From Profit Loss Report'
		            ELSE b.ChartOfAccountName
		       END AS GeneralAccountName
		FROM   @TempTable a
		       LEFT JOIN (
		                SELECT *
		                FROM   ChartOfAccounts
		                WHERE  AccountLevel = 3
		            ) b
		            ON  a.GeneralAccount = b.ChartOfAccountCode 
		                --where a.Balance != 0 -- Not to sure to exclude Zero Balance here on in the report
		ORDER BY
		       a.CategoryId,
		       a.ActivityId,
		       a.NormalBalance,
		       a.ChartOfAccountCode       
		
		
		SELECT @pBeginingBalance = ISNULL(SUM(b.InitialBalance), 0),
		       @pEndingBalance = ISNULL(SUM(b.FinalBalance), 0)
		FROM   ChartOfAccountBalances b
		       INNER JOIN (
		                SELECT *
		                FROM   ChartOfAccounts
		                WHERE  ChartOfAccountId IN (
													SELECT DISTINCT coa.ChartOfAccountId
													FROM Bank AS b
														INNER JOIN ChartOfAccounts AS coa ON coa.ChartOfAccountId = b.ChartOfAccountId
														INNER JOIN ChartOfAccounts AS coa2 ON coa.GeneralAccount = coa2.ChartOfAccountCode
														INNER JOIN ChartOfAccounts AS coa3 ON coa2.GeneralAccount = coa3.ChartOfAccountCode
													WHERE b.BankName NOT LIKE '%ayat silang%'
		                )
		            ) a
		            ON  b.ChartOfAccountId = a.ChartOfAccountId
		WHERE  b.Month = @pSinglePostingPeriode_Month
		       AND b.Year = @pSinglePostingPeriode_Year
		       
		--SELECT @pBeginingBalance, @pEndingBalance
	END
--END       
      
