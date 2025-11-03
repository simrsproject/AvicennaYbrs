/************************************************************      
 * Code formatted by SoftTree SQL Assistant © v11.3.277      
 * Time: 16/03/2025 18.16.52      
 ************************************************************/      
      
CREATE PROCEDURE spxml_RL3_5V2025(@RlTxReportNo VARCHAR(20))      
AS      
SET NOCOUNT ON           
       
BEGIN      
 --DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250314-0004';            
  IF 1 = 0        BEGIN            SET FMTONLY OFF        END              
 DECLARE @PeriodYear           INT,      
         @PeriodMonthStart     INT,      
         @PeriodMonthEnd       INT,      
         @city                 VARCHAR(20),      
         @HealthcareName       VARCHAR(90),      
         @HospitalCode         VARCHAR(20),      
         @ProvincesCode        VARCHAR(20);      
       
 SELECT @city = h.City,      
        @HealthcareName     = h.HealthcareName,      
        @HospitalCode       = h.HospitalCode,      
        @ProvincesCode      = h.ProvincesCode      
 FROM   Healthcare AS h WITH(NOLOCK);      
       
 --SELECT * FROM Healthcare AS h      
       
       
 SELECT @PeriodYear = rtrv.PeriodYear,      
        @PeriodMonthStart     = rtrv.PeriodMonthStart,      
        @PeriodMonthEnd       = rtrv.PeriodMonthEnd      
 FROM   RlTxReportV2025 AS rtrv WITH(NOLOCK)      
 WHERE  rtrv.RlTxReportNo = @RlTxReportNo;      
       
       
 DROP TABLE       
 IF EXISTS #ServiceUnits      
     ;      
       
 CREATE TABLE #ServiceUnits      
 (      
  ServiceUnitID VARCHAR(40)      
 );      
 INSERT INTO #ServiceUnits      
 SELECT su.ServiceUnitID      
 FROM   ServiceUnit su   WITH(NOLOCK)      
        JOIN AppParameter ap   WITH(NOLOCK)      
             ON  su.DepartmentID = ap.ParameterValue      
 WHERE  ap.ParameterID IN ('EmergencyDepartmentID', 'OutPatientDepartmentID');      
       
 DECLARE @jumlahharibukapolilakidalamkota INT,      
         @jumlahharibukapoliwanitadalamkota INT,      
         @jumlahharibukapolilakiluarkota INT,      
         @jumlahharibukapoliwanitaluarkota INT,      
         @JmlPoli INT;      
       
 -- Hitung jumlah hari buka poli berdasarkan gender dan lokasi      
 WITH HariBuka AS (      
     SELECT p.Sex,      
            p.City,      
            COUNT(DISTINCT psd.ScheduleDate) AS JumlahHari      
     FROM   ParamedicScheduleDate psd   WITH(NOLOCK)      
            JOIN Registration r   WITH(NOLOCK)      
                 ON  psd.ServiceUnitID = r.ServiceUnitID      
            JOIN Patient p   WITH(NOLOCK)      
                 ON  r.PatientID = p.PatientID      
     WHERE  psd.ServiceUnitID IN (SELECT ServiceUnitID      
                                  FROM   #ServiceUnits)      
            AND MONTH(psd.ScheduleDate) BETWEEN @PeriodMonthStart AND @PeriodMonthEnd      
            AND YEAR(psd.ScheduleDate) = @PeriodYear      
     GROUP BY      
            p.Sex,      
            p.City      
 )      
       
       
       
 SELECT @jumlahharibukapolilakidalamkota = SUM(      
            CASE       
                 WHEN Sex = 'M'      
                      AND City     = @city THEN JumlahHari      
                     ELSE 0      
                END      
        ),      
        @jumlahharibukapoliwanitadalamkota = SUM(      
            CASE       
                 WHEN Sex = 'F'      
                      AND City     = @city THEN JumlahHari      
                     ELSE 0      
                END      
        ),      
        @jumlahharibukapolilakiluarkota = SUM(      
            CASE       
                 WHEN Sex = 'M'      
                      AND City <> @city THEN JumlahHari      
                     ELSE 0      
                END      
        ),      
        @jumlahharibukapoliwanitaluarkota = SUM(      
            CASE       
                 WHEN Sex = 'F'      
                      AND City <> @city THEN JumlahHari      
                     ELSE 0      
                END      
        )      
 FROM   HariBuka;      
       
 -- Hitung jumlah poli aktif      
 SELECT @JmlPoli = COUNT(ServiceUnitID)      
 FROM   #ServiceUnits;      
       
       
 DECLARE @TotKunjunganPriaDalam       INT,      
         @TotKunjunganWanitaDalam     INT,      
         @TotKunjunganPriaLuar        INT,      
         @TotKunjunganWanitaLuar      INT;      
       
 SELECT @TotKunjunganPriaDalam = rtrv.JumlahLaki,      
        @TotKunjunganWanitaDalam     = rtrv.JumlahPerempuan,      
        @TotKunjunganPriaLuar        = rtrv.JumlahLaki2,      
        @TotKunjunganWanitaLuar      = rtrv.JumlahPerempuan2      
 FROM   RlTxReport3_5V2025 AS rtrv WITH(NOLOCK)      
 WHERE  rtrv.RlTxReportNo = @RlTxReportNo      
        AND rtrv.RlMasterReportItemID = '206';      
       
       
       
       
 SELECT 'RL 3.5.Rekapitulasi Kunjungan' ReportName,      
        'Periode : ' + asri.Note + ' s/d ' + asri2.Note + ' ' + b.PeriodYear AS Periode,      
        @city                      AS City,      
        @HealthcareName            AS HealthcareName,      
        @HospitalCode              AS HospitalCode,      
        @ProvincesCode             AS HospitalCode,      
        b.PeriodYear,      
        c.RlMasterReportItemCode   AS 'No',      
        c.RlMasterReportItemName   AS 'JenisKegiatan',      
        a.JumlahLaki,      
        a.JumlahPerempuan,      
        a.JumlahLaki2,      
        a.JumlahPerempuan2,      
        a.Jumlah,      
        @jumlahharibukapolilakidalamkota AS jumlahharibukapolilakidalamkota,      
        @jumlahharibukapoliwanitadalamkota AS jumlahharibukapoliwanitadalamkota,      
        @jumlahharibukapolilakiluarkota AS jumlahharibukapolilakiluarkota,      
        @jumlahharibukapoliwanitaluarkota AS jumlahharibukapoliwanitaluarkota,      
        @JmlPoli                   AS JmlPoli,      
        @jumlahharibukapolilakidalamkota / @JmlPoli AS RataRataHariPoliklinilBukaDalamKotaPria,      
        @jumlahharibukapoliwanitadalamkota / @JmlPoli AS RataRataHariPoliklinilBukaDalamKotaWanita,      
        @jumlahharibukapolilakiluarkota / @JmlPoli AS RataRataHariPoliklinilBukaluarKotaPria,      
        @jumlahharibukapoliwanitaluarkota / @JmlPoli AS RataRataHariPoliklinilBukaluarKotaWanita,      
        @TotKunjunganPriaDalam     AS TotKunjunganPriaDalam,      
        @TotKunjunganWanitaDalam   AS TotKunjunganWanitaDalam,      
        @TotKunjunganPriaLuar      AS TotKunjunganPriaLuar,      
        @TotKunjunganWanitaLuar    AS TotKunjunganWanitaLuar,      
        ISNULL(      
            @TotKunjunganPriaDalam / NULLIF((@jumlahharibukapolilakidalamkota / @JmlPoli), 0),      
            0      
        )                          AS satu,      
        ISNULL(      
            @TotKunjunganWanitaDalam / NULLIF((@jumlahharibukapoliwanitadalamkota / @JmlPoli), 0),      
            0      
        )                          AS dua,      
        ISNULL(      
            @TotKunjunganPriaLuar / NULLIF((@jumlahharibukapolilakiluarkota / @JmlPoli), 0),      
            0      
        )                          AS tiga,      
        ISNULL(      
            @TotKunjunganWanitaLuar / NULLIF((@jumlahharibukapoliwanitaluarkota / @JmlPoli), 0),      
            0      
        )                          AS empat      
        ,      
              
              
              
        ISNULL(      
            @TotKunjunganPriaDalam / NULLIF((@jumlahharibukapolilakidalamkota / @JmlPoli), 0),      
            0      
        ) +      
        ISNULL(      
            @TotKunjunganWanitaDalam / NULLIF((@jumlahharibukapoliwanitadalamkota / @JmlPoli), 0),      
            0      
        )                          +      
        ISNULL(      
            @TotKunjunganPriaLuar / NULLIF((@jumlahharibukapolilakiluarkota / @JmlPoli), 0),      
            0      
        )                        +      
        ISNULL(      
            @TotKunjunganWanitaLuar / NULLIF((@jumlahharibukapoliwanitaluarkota / @JmlPoli), 0),      
            0      
        )                        AS lima      
              
              
              
 FROM   RlTxReport3_5V2025         AS a WITH(NOLOCK)      
        LEFT JOIN RlTxReportV2025  AS b WITH(NOLOCK)      
             ON  b.RlTxReportNo = a.RlTxReportNo      
        LEFT JOIN RlMasterReportItemV2025 AS c WITH(NOLOCK)      
             ON  c.RlMasterReportItemID = a.RlMasterReportItemID      
        LEFT JOIN AppStandardReferenceItem asri WITH(NOLOCK)      
             ON  asri.StandardReferenceID = 'MonthID'      
             AND asri.ReferenceID = b.PeriodMonthStart      
        LEFT JOIN AppStandardReferenceItem asri2 WITH(NOLOCK)      
             ON  asri2.StandardReferenceID = 'MonthID'      
             AND asri2.ReferenceID = b.PeriodMonthEnd      
 WHERE  a.RlTxReportNo = @RlTxReportNo      
 ORDER BY      
        c.RlMasterReportItemNo       
       
 DROP TABLE #ServiceUnits;      
END