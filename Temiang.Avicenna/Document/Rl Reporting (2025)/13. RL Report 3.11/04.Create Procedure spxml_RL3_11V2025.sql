/************************************************************
 * Code formatted by SoftTree SQL Assistant © v9.2.349
 * Time: 2/4/2020 9:54:30 PM
 ************************************************************/

CREATE PROCEDURE spxml_RL3_11V2025
	@RlTxReportNo VARCHAR(20)
AS
	SET NOCOUNT ON

	--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250131-0001'
	
	SELECT 'RL 3.11 GIGI DAN MULUT' ReportName,
	       CASE 
	            WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' +
	                 asri.ItemName + ' ' + b.PeriodYear
	            ELSE 'Periode : ' + asri.ItemName + ' s/d ' + asri2.ItemName + ' ' +
	                 b.PeriodYear
	       END                            AS Periode,
	       h.HospitalCode,
	       h.HealthcareName,
	       h.ProvincesCode,
	       h.City,
	       b.PeriodYear,
	       c.RlMasterReportItemCode       AS 'No',
	       c.RlMasterReportItemName       AS 'JenisKegiatan',
	       a.Jumlah
	FROM RlTxReport3_11V2025                   AS a
	       INNER JOIN RlTxReportV2025          AS b
	            ON  b.RlTxReportNo = a.RlTxReportNo
	       INNER JOIN RlMasterReportItemV2025  AS c
	            ON  c.RlMasterReportItemID = a.RlMasterReportItemID
	       INNER JOIN AppStandardReferenceItem asri
	            ON  asri.StandardReferenceID = 'MonthID'
	            AND asri.ReferenceID = b.PeriodMonthStart
	       INNER JOIN AppStandardReferenceItem asri2
	            ON  asri2.StandardReferenceID = 'MonthID'
	            AND asri2.ReferenceID = b.PeriodMonthEnd
	       CROSS JOIN (
	                SELECT *
	                FROM   Healthcare AS h
	                WHERE  h.HealthcareID = (
	                           SELECT ap.ParameterValue
	                           FROM   AppParameter AS ap
	                           WHERE  ap.ParameterID = 'HealthcareID'
	                       )
	            )                            h
	WHERE  a.RlTxReportNo = @RlTxReportNo
	ORDER BY
	       c.RlMasterReportItemNo
