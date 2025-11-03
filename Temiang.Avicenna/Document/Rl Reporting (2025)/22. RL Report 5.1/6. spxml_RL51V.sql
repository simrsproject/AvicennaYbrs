/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 17/02/2025 11:35:17
 ************************************************************/

CREATE PROCEDURE spxml_RL51V
	@RlTxReportNo VARCHAR(20)
AS
	--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250515-0006'    
	
	SET NOCOUNT ON      
	
	SELECT 'RL 5.1 KOMPILASI MORBIDITAS PASIEN RAWAT JALAN' ReportName,
	       a.RlMasterReportItemID,
	       CASE 
	            WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' +
	                 asri.ItemName + ' ' + b.PeriodYear
	            ELSE 'Periode : ' + asri.ItemName + ' s/d ' + asri2.ItemName + ' ' +
	                 b.PeriodYear
	       END                         AS Periode,
	       h.HospitalCode,
	       h.HealthcareName,
	       h.ProvincesCode,
	       h.City,
	       b.PeriodYear,
	       CAST(c.RlMasterReportItemNo AS VARCHAR) AS 'NoUrut',
	       c.RlMasterReportItemCode    AS 'NoDdt',
	       --CAST(d.DtdLabel AS VARCHAR) AS DtdLabel,   
	       CAST(ed.DiagnoseID AS VARCHAR) AS DtdLabel,
	       c.RlMasterReportItemName    AS 'GolSebabPenyakit',
	       a.L0001j,
	       a.P0001j,
	       a.L0001h,
	       a.P0001h,
	       a.L0007h,
	       a.P0007h,
	       a.L0828h,
	       a.P0828h,
	       a.L29h03b,
	       a.P29h03b,
	       a.L3b6b,
	       a.P3b6b,
	       a.L6b11b,
	       a.P6b11b,
	       a.L0104t,
	       a.P0104t,
	       a.L0509t,
	       a.P0509t,
	       a.L1014t,
	       a.P1014t,
	       a.L1519t,
	       a.P1519t,
	       a.L2024t,
	       a.P2024t,
	       a.L2529t,
	       a.P2529t,
	       a.L3034t,
	       a.P3034t,
	       a.L3539t,
	       a.P3539t,
	       a.L4044t,
	       a.P4044t,
	       a.L4549t,
	       a.P4549t,
	       a.L5054t,
	       a.P5054t,
	       a.L5559t,
	       a.P5559t,
	       a.L6064t,
	       a.P6064t,
	       a.L6569t,
	       a.P6569t,
	       a.L7074t,
	       a.P7074t,
	       a.L7579t,
	       a.P7579t,
	       a.L8084t,
	       a.P8084t,
	       a.L85t,
	       a.P85t,
	       a.KasusBaruL,
	       a.KasusBaruP,
	       a.KunjunganL AS KasusLamaL,
	       a.KunjunganP AS KasusLamaP,
	       a.TotalKasusBaru,
	       a.TotalKunjungan
	FROM   RlTxReport51V2025           AS a WITH(NOLOCK)
	       INNER JOIN RlTxReportV2025  AS b WITH(NOLOCK)
	            ON  b.RlTxReportNo = a.RlTxReportNo
	       INNER JOIN RlMasterReportItemV2025 AS c WITH(NOLOCK)
	            ON  c.RlMasterReportItemID = a.RlMasterReportItemID
	       INNER JOIN Dtd d WITH(NOLOCK)
	            ON  d.DtdNo = c.RlMasterReportItemCode
	       INNER JOIN AppStandardReferenceItem asri WITH(NOLOCK)
	            ON  asri.StandardReferenceID = 'MonthID'
	            AND asri.ReferenceID = b.PeriodMonthStart
	       INNER JOIN AppStandardReferenceItem asri2 WITH(NOLOCK)
	            ON  asri2.StandardReferenceID = 'MonthID'
	            AND asri2.ReferenceID = b.PeriodMonthEnd
	       CROSS JOIN (
	                SELECT *
	                FROM   Healthcare AS h WITH(NOLOCK)
	                WHERE  h.HealthcareID = (
	                           SELECT ap.ParameterValue
	                           FROM   AppParameter AS ap
	                           WHERE  ap.ParameterID = 'HealthcareID'
	                       )
	            ) h
	       INNER JOIN EpisodeDiagnose  AS ed WITH(NOLOCK)
	            ON  ed.SRDiagnoseType = 'DiagnoseType-001'
	            AND ed.DiagnoseID = d.DtdLabel
	WHERE  a.RlTxReportNo = @RlTxReportNo
	GROUP BY
	       a.RlMasterReportItemID,
	       CASE 
	            WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' +
	                 asri.ItemName + ' ' + b.PeriodYear
	            ELSE 'Periode : ' + asri.ItemName + ' s/d ' + asri2.ItemName + ' ' +
	                 b.PeriodYear
	       END,
	       h.HospitalCode,
	       h.HealthcareName,
	       h.ProvincesCode,
	       h.City,
	       b.PeriodYear,
	       CAST(c.RlMasterReportItemNo AS VARCHAR),
	       c.RlMasterReportItemCode,
	       CAST(ed.DiagnoseID AS VARCHAR),
	       c.RlMasterReportItemName,
	       a.L0001j,
	       a.P0001j,
	       a.L0001h,
	       a.P0001h,
	       a.L0007h,
	       a.P0007h,
	       a.L0828h,
	       a.P0828h,
	       a.L29h03b,
	       a.P29h03b,
	       a.L3b6b,
	       a.P3b6b,
	       a.L6b11b,
	       a.P6b11b,
	       a.L0104t,
	       a.P0104t,
	       a.L0509t,
	       a.P0509t,
	       a.L1014t,
	       a.P1014t,
	       a.L1519t,
	       a.P1519t,
	       a.L2024t,
	       a.P2024t,
	       a.L2529t,
	       a.P2529t,
	       a.L3034t,
	       a.P3034t,
	       a.L3539t,
	       a.P3539t,
	       a.L4044t,
	       a.P4044t,
	       a.L4549t,
	       a.P4549t,
	       a.L5054t,
	       a.P5054t,
	       a.L5559t,
	       a.P5559t,
	       a.L6064t,
	       a.P6064t,
	       a.L6569t,
	       a.P6569t,
	       a.L7074t,
	       a.P7074t,
	       a.L7579t,
	       a.P7579t,
	       a.L8084t,
	       a.P8084t,
	       a.L85t,
	       a.P85t,
	       a.KasusBaruL,
	       a.KasusBaruP,
	       a.KunjunganL,
	       a.KunjunganP,
	       a.TotalKasusBaru,
	       a.TotalKunjungan,
	       c.RlMasterReportItemNo
	ORDER BY
	       c.RlMasterReportItemNo 
