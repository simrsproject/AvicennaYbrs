
CREATE PROCEDURE spxml_RL4_3V2025  
 @RlTxReportNo VARCHAR(20)  
AS  
  
--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250127-0001'  
  
IF 1 = 0  
BEGIN  
    SET FMTONLY OFF  
END  
  
SET NOCOUNT ON    
   
         
SELECT 'RL 4.3 10 BESAR KEMATIAN PENYAKIT RAWAT INAP' ReportName,  
'Periode : ' + d.Note + ' s/d ' + e.Note + ' ' + c.PeriodYear Periode,  
a.*,
(SELECT d.DiagnoseName FROM Diagnose AS d WITH(NOLOCK) WHERE d.DiagnoseID = a.DiagnosaID) DiagnoseName
FROM   RlTxReport4_3V2025            AS a WITH(NOLOCK)  
       JOIN RlTxReportV2025 AS c WITH (NOLOCK)  
       ON c.RlTxReportNo = a.RlTxReportNo  
       JOIN AppStandardReferenceItem AS d WITH (NOLOCK)  
       ON d.ReferenceID = c.PeriodMonthStart AND d.StandardReferenceID = 'MONTHID'  
       JOIN AppStandardReferenceItem AS e WITH (NOLOCK)  
       ON e.ReferenceID = c.PeriodMonthEnd AND e.StandardReferenceID = 'MONTHID'  
WHERE  a.RlTxReportNo = @RlTxReportNo
ORDER BY a.TotalKeluarMati DESC 


  
  