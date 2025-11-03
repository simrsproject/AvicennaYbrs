/************************************************************
 * Code formatted by SoftTree SQL Assistant © v12.0.191
 * Time: 16/01/2025 10:49:07
 ************************************************************/

CREATE PROCEDURE spxml_RL3_2V2025
	@RlTxReportNo VARCHAR(20)
AS
	SET NOCOUNT ON
	
	--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250116-0002'
	
	SELECT 'RL 3.1 RAWAT INAP'               ReportName,
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
	       c.RlMasterReportItemName       AS 'JenisPelayanan',
	       a.PasienAwal,
	       a.PasienMasuk,
	       a.PasienKeluarHidup,
	       a.PasienLKeluarMatiK48,
	       a.PasienPKeluarMatiK48,
	       a.PasienLKeluarMatiL48,
	       a.PasienPKeluarMatiL48,
	       a.LamaRawat,
	       a.PasienAkhir,
	       a.HariRawat,
	       a.Vvip,
	       a.Vip,
	       a.I,
	       a.Ii,
	       a.Iii,
	       a.KelasKhusus,
	       a.AlokasiTT,
	       a.PasienPindahan,
	       a.PasienDipindahkan
	FROM   RlTxReport3_2V2025             AS a
	       INNER JOIN RlTxReportV2025     AS b
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
