CREATE PROCEDURE spxml_RL5_2V2025
	@RlTxReportNo VARCHAR(20)
AS

SET NOCOUNT ON

SELECT TOP 10 
'RL5.2 10 Besar Penyakit Rawat Jalan' ReportName,
	        CASE WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' +  asri.Note + ' ' + b.PeriodYear 
            ELSE 'Periode : ' + asri.Note + ' s/d ' + asri2.ItemName + ' ' +  b.PeriodYear END AS Periode,
            asri.Note AS Bulan,  
            h.HospitalCode, h.HealthcareName, h.ProvincesCode, h.City, b.PeriodYear, a.DiagnosaID, d.DiagnoseName,
	a.KasusBaruL, a.KasusBaruP, a.JumlahKasusBaru, a.JumlahKunjungan, a.KunjunganL,a.KunjunganP
FROM RlTxReport5_2V2025 AS a
INNER JOIN RlTxReportV2025 AS b ON b.RlTxReportNo = a.RlTxReportNo
INNER JOIN Diagnose AS d ON d.DiagnoseID = a.DiagnosaID
	        INNER JOIN AppStandardReferenceItem asri 
            ON  asri.StandardReferenceID = 'MonthID' 
            AND asri.ReferenceID = b.PeriodMonthStart 
            INNER JOIN AppStandardReferenceItem asri2 
            ON  asri2.StandardReferenceID = 'MonthID' 
            AND asri2.ReferenceID = b.PeriodMonthEnd 
CROSS JOIN (SELECT * FROM Healthcare AS h WHERE h.HealthcareID = (SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'HealthcareID')) h
WHERE a.RlTxReportNo = @RlTxReportNo
ORDER BY a.JumlahKunjungan DESC

