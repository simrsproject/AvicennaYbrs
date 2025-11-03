

CREATE PROCEDURE spxml_RL3_3V2025
	@RlTxReportNo VARCHAR(20)
AS

--DECLARE @RlTxReportNo VARCHAR(20) = ''

IF 1 = 0
BEGIN
    SET FMTONLY OFF
END

SET NOCOUNT ON  
	
       
SELECT 'RL 3.3 REKAPITULASI KEGIATAN PELAYANAN RAWAT DARURAT' ReportName,
'Periode : ' + d.Note + ' s/d ' + e.Note + ' ' + c.PeriodYear Periode,
CASE 
    WHEN b.RlMasterReportItemCode LIKE '%.%'  THEN 'SCN'   
    ELSE 'PRM'
END Code,
b.RlMasterReportItemCode,
b.RlMasterReportItemName JenisPelayanan,
a.*
FROM   RlTxReport3_3V2025            AS a WITH(NOLOCK)
       JOIN RlMasterReportItemV2025  AS b WITH (NOLOCK)
            ON  b.RlMasterReportItemID = a.RlMasterReportItemID
            AND b.RlMasterReportID = 3
       JOIN RlTxReportV2025 AS c WITH (NOLOCK)
       ON c.RlTxReportNo = a.RlTxReportNo
       JOIN AppStandardReferenceItem AS d WITH (NOLOCK)
       ON d.ReferenceID = c.PeriodMonthStart AND d.StandardReferenceID = 'MONTHID'
       JOIN AppStandardReferenceItem AS e WITH (NOLOCK)
       ON e.ReferenceID = c.PeriodMonthEnd AND e.StandardReferenceID = 'MONTHID'
WHERE  a.RlTxReportNo = @RlTxReportNo
ORDER BY b.RlMasterReportItemNo ASC 


  