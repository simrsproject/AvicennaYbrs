

CREATE PROCEDURE spxml_RL3_4_V2025
	@RlTxReportNo VARCHAR(20)
AS

SET NOCOUNT ON
			--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250120-0004'
SELECT 'RL 3.4 Pengunjung' ReportName,
	        CASE WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' +  asri.Note + ' ' + b.PeriodYear 
            ELSE 'Periode : ' + asri.Note + ' s/d ' + asri2.ItemName + ' ' +  b.PeriodYear END AS Periode,
            asri.Note AS Bulan,h.HealthcareName, 
            h.HospitalCode, h.ProvincesCode, h.City, b.PeriodYear, c.RlMasterReportItemCode AS 'No', c.RlMasterReportItemName AS 'JenisKegiatan',
	a.Jumlah
FROM RlTxReport3_4V2025 AS a
INNER JOIN RlTxReportV2025 AS b ON b.RlTxReportNo = a.RlTxReportNo
INNER JOIN RlMasterReportItemV2025 AS c ON c.RlMasterReportItemID = a.RlMasterReportItemID 
	        INNER JOIN AppStandardReferenceItem asri 
            ON  asri.StandardReferenceID = 'MonthID' 
            AND asri.ReferenceID = b.PeriodMonthStart 
            INNER JOIN AppStandardReferenceItem asri2 
            ON  asri2.StandardReferenceID = 'MonthID' 
            AND asri2.ReferenceID = b.PeriodMonthEnd 
CROSS JOIN (SELECT * FROM Healthcare AS h WHERE h.HealthcareID = (SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'HealthcareID')) h
WHERE a.RlTxReportNo = @RlTxReportNo
ORDER BY c.RlMasterReportItemNo

