/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 09/02/2025 16:25:53
 ************************************************************/

CREATE PROCEDURE spxml_RL3_1V2025
	@RlTxReportNo VARCHAR(20)
AS
	SET NOCOUNT ON

--DECLARE @RlTxReportNo VARCHAR(20) = ''

SELECT 'RL 3.1 Indikator Pelayanan'     ReportName,
       CASE 
            WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' + asri.Note + ' ' + b.PeriodYear
            ELSE 'Periode : ' + asri.Note + ' s/d ' + asri2.ItemName + ' ' + b.PeriodYear
       END                           AS Periode,
       asri.Note                     AS Bulan,
       h.HealthcareName,
       h.HospitalCode,
       h.ProvincesCode,
       h.City,
       b.PeriodYear,
       a.BorNonIntensif,
       a.BorICU,
       a.BorNICU,
       a.BorPICU,
       a.BorIntensifLainnya,
       a.LosNonIntensif,
       a.LosICU,
       a.LosNICU,
       a.LosPICU,
       a.LosIntensifLainnya,
       a.BtoNonIntensif,
       a.BtoICU,
       a.BtoNICU,
       a.BtoPICU,
       a.BtoIntensifLainnya,
       a.ToiNonIntensif,
       a.ToiICU,
       a.ToiNICU,
       a.ToiPICU,
       a.ToiIntensifLainnya,
       a.NdrNonIntensif,
       a.NdrICU,
       a.NdrNICU,
       a.NdrPICU,
       a.NdrIntensifLainnya,
       a.GdrNonIntensif,
       a.GdrICU,
       a.GdrNICU,
       a.GdrPICU,
       a.GdrIntensifLainnya
FROM   RlTxReport3_1V2025            AS a
       INNER JOIN RlTxReportV2025    AS b
            ON  b.RlTxReportNo = a.RlTxReportNo
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
            )                           h
WHERE  a.RlTxReportNo = @RlTxReportNo