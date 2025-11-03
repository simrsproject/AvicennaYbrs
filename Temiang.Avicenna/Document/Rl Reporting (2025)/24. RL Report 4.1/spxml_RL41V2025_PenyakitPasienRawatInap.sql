/********************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 26/05/2025 14:25:14
 ********************/
 ALTER PROCEDURE [dbo].[spxml_RL41V2025_PenyakitPasienRawatInap]                
 @RlTxReportNo VARCHAR(20)                
AS  

SET NOCOUNT ON                

--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250506-0001'

;WITH DiagnosaInap AS (
         SELECT  
                c.AgeInDay,
                c.AgeInMonth,
                c.AgeInYear,
                d.Sex,
                c.SRDischargeCondition,
                b.DtdNo,
                a.DiagnoseID,
                b.DiagnoseName,
                c.DischargeDate,
                DATEDIFF(MINUTE, e.TimeOfBirth, GETDATE()) AS Selisih,                       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear = 0
                     AND c.AgeInDay = 0
                     AND DATEDIFF(MINUTE, e.TimeOfBirth, GETDATE()) < 60 THEN 1
                ELSE 0
           END AS L1j,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear = 0
                     AND c.AgeInDay = 0
                     AND DATEDIFF(MINUTE, e.TimeOfBirth, GETDATE()) < 60 THEN 1
                ELSE 0
           END AS P1j,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear = 0
                     AND c.AgeInDay = 0
                     AND DATEDIFF(MINUTE, e.TimeOfBirth, GETDATE()) >= 60
                     AND DATEDIFF(MINUTE, e.TimeOfBirth, GETDATE()) <= 1380 THEN 1
                ELSE 0
           END AS L123j,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear = 0
                     AND c.AgeInDay = 0
                     AND DATEDIFF(MINUTE, e.TimeOfBirth, GETDATE()) >= 60
                     AND DATEDIFF(MINUTE, e.TimeOfBirth, GETDATE()) <= 1380 THEN 1
                ELSE 0
           END AS P123j,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear = 0
                     AND c.AgeInMonth = 0
                     AND c.AgeInDay >= 1
                     AND c.AgeInDay <= 7 THEN 1
                ELSE 0
           END AS L0107h,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear = 0
                     AND c.AgeInMonth = 0
                     AND c.AgeInDay >= 1
                     AND c.AgeInDay <= 7 THEN 1
                ELSE 0
           END AS P0107h,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear = 0
                     AND c.AgeInDay >= 8
                     AND c.AgeInDay <= 28 THEN 1
                ELSE 0
           END AS L0828h,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear = 0
                     AND c.AgeInDay >= 8
                     AND c.AgeInDay <= 28 THEN 1
                ELSE 0
           END AS P0828h,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear = 0
                     AND c.AgeInDay >= 29
                     AND c.AgeInMonth < 3 THEN 1
                ELSE 0
           END AS L29h03b,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear = 0
                     AND c.AgeInDay >= 29
                     AND c.AgeInMonth < 3 THEN 1
                ELSE 0
           END AS P29h03b,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear = 0
                     AND c.AgeInMonth >= 3
                     AND c.AgeInMonth < 6 THEN 1
                ELSE 0
           END AS L0306b,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear = 0
                     AND c.AgeInMonth >= 3
                     AND c.AgeInMonth < 6 THEN 1
                ELSE 0
           END AS P0306b,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear = 0
                     AND c.AgeInMonth >= 6
                     AND c.AgeInMonth < 11 THEN 1
                ELSE 0
           END AS L0611b,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear = 0
                     AND c.AgeInMonth >= 6
                     AND c.AgeInMonth < 11 THEN 1
                ELSE 0
           END AS P0611b,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 1
                     AND c.AgeInYear <= 4 THEN 1
                ELSE 0
           END AS L0104t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 1
                     AND c.AgeInYear <= 4 THEN 1
                ELSE 0
           END AS P0104t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 5
                     AND c.AgeInYear <= 9 THEN 1
                ELSE 0
           END AS L0509t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 5
                     AND c.AgeInYear <= 9 THEN 1
                ELSE 0
           END AS P0509t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 10
                     AND c.AgeInYear <= 14 THEN 1
                ELSE 0
           END AS L1014t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 10
                     AND c.AgeInYear <= 14 THEN 1
                ELSE 0
           END AS P1014t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 15
                     AND c.AgeInYear <= 19 THEN 1
                ELSE 0
           END AS L1519t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 15
                     AND c.AgeInYear <= 19 THEN 1
                ELSE 0
           END AS P1519t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 20
                     AND c.AgeInYear <= 24 THEN 1
                ELSE 0
           END AS L2024t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 20
                     AND c.AgeInYear <= 24 THEN 1
                ELSE 0
           END AS P2024t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 25
                     AND c.AgeInYear <= 29 THEN 1
                ELSE 0
           END AS L2529t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 25
                     AND c.AgeInYear <= 29 THEN 1
                ELSE 0
           END AS P2529t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 30
                     AND c.AgeInYear <= 34 THEN 1
                ELSE 0
           END AS L3034t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 30
                     AND c.AgeInYear <= 34 THEN 1
                ELSE 0
           END AS P3034t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 35
                     AND c.AgeInYear <= 39 THEN 1
                ELSE 0
           END AS L3539t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 35
                     AND c.AgeInYear <= 39 THEN 1
                ELSE 0
           END AS P3539t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 40
                     AND c.AgeInYear <= 44 THEN 1
                ELSE 0
           END AS L4044t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 40
                     AND c.AgeInYear <= 44 THEN 1
                ELSE 0
           END AS P4044t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 45
                     AND c.AgeInYear <= 49 THEN 1
                ELSE 0
           END AS L4549t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 45
                     AND c.AgeInYear <= 49 THEN 1
                ELSE 0
           END AS P4549t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 50
                     AND c.AgeInYear <= 54 THEN 1
                ELSE 0
           END AS L5054t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 50
                     AND c.AgeInYear <= 54 THEN 1
                ELSE 0
           END AS P5054t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 55
                     AND c.AgeInYear <= 59 THEN 1
                ELSE 0
           END AS L5559t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 55
                     AND c.AgeInYear <= 59 THEN 1
                ELSE 0
           END AS P5559t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 60
                     AND c.AgeInYear <= 64 THEN 1
                ELSE 0
           END AS L6064t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 60
                     AND c.AgeInYear <= 64 THEN 1
                ELSE 0
           END AS P6064t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 65
                     AND c.AgeInYear <= 69 THEN 1
                ELSE 0
           END AS L6569t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 65
                     AND c.AgeInYear <= 69 THEN 1
                ELSE 0
           END AS P6569t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 70
                     AND c.AgeInYear <= 74 THEN 1
                ELSE 0
           END AS L7074t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 70
                     AND c.AgeInYear <= 74 THEN 1
                ELSE 0
           END AS P7074t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 75
                     AND c.AgeInYear <= 79 THEN 1
                ELSE 0
           END AS L7579t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 75
                     AND c.AgeInYear <= 79 THEN 1
                ELSE 0
           END AS P7579t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 80
                     AND c.AgeInYear <= 84 THEN 1
                ELSE 0
           END AS L8084t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 80
                     AND c.AgeInYear <= 84 THEN 1
                ELSE 0
           END AS P8084t,
       
           CASE 
                WHEN d.Sex = 'M'
                     AND c.AgeInYear >= 85 THEN 1
                ELSE 0
           END AS L85t,
       
           CASE 
                WHEN d.Sex = 'F'
                     AND c.AgeInYear >= 85 THEN 1
                ELSE 0
           END AS P85t,
           CASE WHEN d.Sex = 'M' THEN 1 ELSE 0 END AS TotalPasienHidupL,
           CASE WHEN d.Sex = 'F' THEN 1 ELSE 0 END AS TotalPasienHidupP,
		   CASE WHEN d.Sex = 'M' THEN 1 ELSE 0 END + CASE WHEN d.Sex = 'F' THEN 1 ELSE 0 END AS TotalPasienHidup,             
           --CASE WHEN d.Sex = 'M' AND c.SRDischargeCondition NOT IN ('I04', 'I05') THEN 1 ELSE 0 END AS TotalPasienHidupL,
           --CASE WHEN d.Sex = 'F' AND c.SRDischargeCondition NOT IN ('I04', 'I05') THEN 1 ELSE 0 END AS TotalPasienHidupP,
           --CASE WHEN c.SRDischargeCondition NOT IN ('I04', 'I05') THEN 1 ELSE 0 END AS TotalPasienHidup,      
           CASE 
                WHEN c.SRDischargeCondition IN ('I04', 'I05')
                     AND d.Sex = 'M' THEN 1
                ELSE 0
           END AS TotalPasienMatiL,
       
           CASE 
                WHEN c.SRDischargeCondition IN ('I04', 'I05')
                     AND d.Sex = 'F' THEN 1
                ELSE 0
           END AS TotalPasienMatiP,
       
           CASE 
                WHEN c.SRDischargeCondition IN ('I04', 'I05') THEN 1
                ELSE 0
           END AS TotalPasienMati

         FROM   EpisodeDiagnose a
                INNER JOIN Diagnose b
                     ON  a.DiagnoseID = b.DiagnoseID
                         --AND b.DtdNo IN (SELECT rmriv.RlMasterReportItemCode
                         --                FROM RlMasterReportItemV2025 AS rmriv
                         --                WHERE rmriv.RlMasterReportID = '19')                         
                INNER JOIN Registration c
                     ON  a.RegistrationNo = c.RegistrationNo
                INNER JOIN Patient d
                     ON  c.PatientID = d.PatientID
                LEFT JOIN BirthRecord e
                     ON  c.RegistrationNo = e.RegistrationNo
         WHERE  c.IsVoid = 0
                AND c.SRRegistrationType = 'IPR'
                AND a.IsVoid = 0
                --AND MONTH(c.DischargeDate) = (SELECT a.PeriodMonthStart FROM RlTxReportV2025 AS a WHERE a.RlTxReportNo = @RlTxReportNo)
                --AND MONTH(c.DischargeDate) = (SELECT a.PeriodMonthEnd FROM RlTxReportV2025 AS a WHERE a.RlTxReportNo = @RlTxReportNo)
                --AND YEAR(c.DischargeDate) = (SELECT a.PeriodYear FROM RlTxReportV2025 AS a WHERE a.RlTxReportNo = @RlTxReportNo)
     )

SELECT DISTINCT 'RL 4.1 KOMPILASI PENYAKIT/MORBIDITAS PASIEN RAWAT INAP' AS ReportName,
       CASE 
            WHEN c.PeriodMonthStart = c.PeriodMonthEnd THEN 'Periode : ' +
                 asri.ItemName + ' ' + c.PeriodYear
            ELSE 'Periode : ' + asri.ItemName + ' s/d ' + asri2.ItemName + ' ' +
                 c.PeriodYear
       END AS Periode,
       h.HospitalCode,              
       h.HealthcareName,              
       h.ProvincesCode,              
       h.City,              
       c.PeriodYear, 
       RIGHT('000' + CAST(ROW_NUMBER() OVER (ORDER BY b.DtdNo, b.DiagnoseID) AS VARCHAR(3)), 3) AS NoUrut,      
       b.DtdNo AS 'NoDdt',
       b.DiagnoseID AS 'DtdLabel',
       b.DiagnoseName AS 'GolSebabPenyakit',
       SUM(b.L1j) AS L1j,
       SUM(b.P1j) AS P1j,
       SUM(b.L123j) AS L123j,
       SUM(b.P123j) AS P123j,
       SUM(b.L0107h) AS L0107h,
       SUM(b.P0107h) AS P0107h,
       SUM(b.L0828h) AS L0828h,
       SUM(b.P0828h) AS P0828h,
       SUM(b.L29h03b) AS L29h03b,
       SUM(b.P29h03b) AS P29h03b,
       SUM(b.L0306b) AS L0306b,
       SUM(b.P0306b) AS P0306b,
       SUM(b.L0611b) AS L0611b,
       SUM(b.P0611b) AS P0611b,
       SUM(b.L0104t) AS L0104t,
       SUM(b.P0104t) AS P0104t,
       SUM(b.L0509t) AS L0509t,
       SUM(b.P0509t) AS P0509t,
       SUM(b.L1014t) AS L1014t,
       SUM(b.P1014t) AS P1014t,
       SUM(b.L1519t) AS L1519t,
       SUM(b.P1519t) AS P1519t,
       SUM(b.L2024t) AS L2024t,
       SUM(b.P2024t) AS P2024t,
       SUM(b.L2529t) AS L2529t,
       SUM(b.P2529t) AS P2529t,
       SUM(b.L3034t) AS L3034t,
       SUM(b.P3034t) AS P3034t,
       SUM(b.L3539t) AS L3539t,
       SUM(b.P3539t) AS P3539t,
       SUM(b.L4044t) AS L4044t,
       SUM(b.P4044t) AS P4044t,
       SUM(b.L4549t) AS L4549t,
       SUM(b.P4549t) AS P4549t,
       SUM(b.L5054t) AS L5054t,
       SUM(b.P5054t) AS P5054t,
       SUM(b.L5559t) AS L5559t,
       SUM(b.P5559t) AS P5559t,
       SUM(b.L6064t) AS L6064t,
       SUM(b.P6064t) AS P6064t,
       SUM(b.L6569t) AS L6569t,
       SUM(b.P6569t) AS P6569t,
       SUM(b.L7074t) AS L7074t,
       SUM(b.P7074t) AS P7074t,
       SUM(b.L7579t) AS L7579t,
       SUM(b.P7579t) AS P7579t,
       SUM(b.L8084t) AS L8084t,
       SUM(b.P8084t) AS P8084t,
       SUM(b.L85t) AS L85t,
       SUM(b.P85t) AS P85t,
       SUM(b.TotalPasienHidupL) AS TotalPasienHidupL,
       SUM(b.TotalPasienHidupP) AS TotalPasienHidupP,
       SUM(b.TotalPasienHidup) AS TotalPasienHidup,
       SUM(b.TotalPasienMatiL) AS TotalPasienMatiL,
       SUM(b.TotalPasienMatiP) AS TotalPasienMatiP,
       SUM(b.TotalPasienMati) AS TotalPasienMati

FROM   DiagnosaInap b
       INNER JOIN RlTxReportV2025 c
            ON  MONTH(b.DischargeDate) >= c.PeriodMonthStart
            AND MONTH(b.DischargeDate) <= c.PeriodMonthEnd
            AND YEAR(b.DischargeDate) = c.PeriodYear
       INNER JOIN AppStandardReferenceItem AS asri
            ON  asri.StandardReferenceID = 'MonthID'
            AND asri.ReferenceID = c.PeriodMonthStart
       INNER JOIN AppStandardReferenceItem AS asri2
            ON  asri2.StandardReferenceID = 'MonthID'
            AND asri2.ReferenceID = c.PeriodMonthEnd
       CROSS JOIN (
                SELECT *
                FROM   Healthcare AS h
                WHERE  h.HealthcareID = (
                           SELECT ap.ParameterValue
                           FROM   AppParameter AS ap
                           WHERE  ap.ParameterID = 'HealthcareID'
                       )
            ) h
WHERE  c.RlTxReportNo = @RlTxReportNo
GROUP BY
       CASE 
            WHEN c.PeriodMonthStart = c.PeriodMonthEnd THEN 'Periode : ' +
                 asri.ItemName + ' ' + c.PeriodYear
            ELSE 'Periode : ' + asri.ItemName + ' s/d ' + asri2.ItemName + ' ' +
                 c.PeriodYear
       END,
       h.HospitalCode,              
       h.HealthcareName,              
       h.ProvincesCode,              
       h.City,              
       c.PeriodYear,                     
       b.DtdNo,
       b.DiagnoseID,
       b.DiagnoseName
ORDER BY
       b.DiagnoseID