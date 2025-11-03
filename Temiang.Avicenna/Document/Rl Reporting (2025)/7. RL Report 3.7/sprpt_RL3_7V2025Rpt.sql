/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 27/02/2025 14:32:55
 ************************************************************/

CREATE PROCEDURE [dbo].[sprpt_RL3_7V2025Rpt]
	@RlTxReportNo VARCHAR(20)
AS
	SET NOCOUNT ON
	
--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250227-0001'

SELECT 'RL 3.7 Rekapitulasi Kegiatan Pelayanan Neonatal, Bayi, dan Balita' AS ReportName,
       CASE 
            WHEN CHARINDEX('.', rmri.RlMasterReportItemCode) = 0 THEN rmri.RlMasterReportItemCode -- Jika tidak ada titik, tetap pakai nilai aslinya
            WHEN LEN(rmri.RlMasterReportItemCode) - LEN(REPLACE(rmri.RlMasterReportItemCode, '.', '')) = 1 THEN LEFT(
                     rmri.RlMasterReportItemCode,
                     CHARINDEX('.', rmri.RlMasterReportItemCode) - 1
                 )
            ELSE LEFT(
                     rmri.RlMasterReportItemCode,
                     CHARINDEX(
                         '.',
                         rmri.RlMasterReportItemCode,
                         CHARINDEX('.', rmri.RlMasterReportItemCode) + 1
                     ) - 1
                 )
       END                            AS GroupNo,
       rmri.RlMasterReportItemCode,
       rtr.*,
       rmri.RlMasterReportItemName,
       CASE 
            WHEN rtr3.PeriodMonthStart = rtr3.PeriodMonthEnd THEN 'Periode : ' + asri.ItemName + ' ' + rtr3.PeriodYear
            ELSE 'Periode : ' + asri.ItemName + ' s/d ' + asri2.ItemName + ' ' + rtr3.PeriodYear
       END                            AS Periode,
       h.HealthcareName,
       h.City,
       h.HospitalCode,
       ''                             AS KodePropinsi
FROM   Healthcare h
       JOIN RlTxReport3_7v2025 rtr
            ON  rtr.RlTxReportNo = @RlTxReportNo
       JOIN RlTxReportV2025 rtr3
            ON  rtr.RlTxReportNo = rtr3.RlTxReportNo
       JOIN RlMasterReportItemV2025 rmri
            ON  rmri.RlMasterReportItemID = rtr.RlMasterReportItemID
       JOIN RlMasterReportV2025 rmr
            ON  rmr.RlMasterReportID = rmri.RlMasterReportID
       JOIN AppStandardReferenceItem  asri
            ON  asri.StandardReferenceID = 'MonthID'
            AND CAST(asri.ItemID AS INT) = CAST(rtr3.PeriodMonthStart AS INT)
       JOIN AppStandardReferenceItem  asri2
            ON  asri2.StandardReferenceID = 'MonthID'
            AND CAST(asri2.ItemID AS INT) = CAST(rtr3.PeriodMonthEnd AS INT)
WHERE  rmri.RlMasterReportID = 7
       AND rmri.RlMasterReportItemNo NOT IN ('1', '2', '3', '4', '8', '9', '11', '12', '13', '14', '15', '16', '17')
ORDER BY
       rtr.RlMasterReportItemID ASC;