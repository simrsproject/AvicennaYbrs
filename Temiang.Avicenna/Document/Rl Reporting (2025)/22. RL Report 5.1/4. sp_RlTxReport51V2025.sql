/************************************************************  
 * Code formatted by SoftTree SQL Assistant © v11.3.277  
 * Time: 12/02/2025 10:11:07  
 ************************************************************/
CREATE PROCEDURE sp_RlTxReport51V2025  
 @p_RlTxReportNo VARCHAR(20),  
 @p_FromMonth INT,  
 @p_ToMonth INT,  
 @p_Year INT,  
 @p_UserID VARCHAR(20)  
AS  

 CREATE TABLE #tmpRlTxReport51V2025  
 (  
  RlMasterReportItemID       INT,  
  RlMasterReportItemCode     VARCHAR(50),  
  L0001j                     INT,  
  P0001j                     INT,  
  L0001h                     INT,  
  P0001h                     INT,  
  L0007h                     INT,  
  P0007h                     INT,  
  L0828h                     INT,  
  P0828h                     INT,  
  L29h03b                    INT,  
  P29h03b                    INT,  
  L3b6b                      INT,  
  P3b6b                      INT,  
  L6b11b                     INT,  
  P6b11b                     INT,  
  L0104t                     INT,  
  P0104t                     INT,  
  L0509t                     INT,  
  P0509t                     INT,  
  L1014t                     INT,  
  P1014t                     INT,  
  L1519t                     INT,  
  P1519t                     INT,  
  L2024t                     INT,  
  P2024t                     INT,  
  L2529t                     INT,  
  P2529t                     INT,  
  L3034t                     INT,  
  P3034t                     INT,  
  L3539t                     INT,  
  P3539t                     INT,  
  L4044t                     INT,  
  P4044t                     INT,  
  L4549t                     INT,  
  P4549t                     INT,  
  L5054t                     INT,  
  P5054t                     INT,  
  L5559t                     INT,  
  P5559t                     INT,  
  L6064t                     INT,  
  P6064t                     INT,  
  L6569t                     INT,  
  P6569t                     INT,  
  L7074t                     INT,  
  P7074t                     INT,  
  L7579t                     INT,  
  P7579t                     INT,  
  L8084t                     INT,  
  P8084t                     INT,  
  L85t                       INT,  
  P85t                       INT,  
  KasusBaruL                 INT,  
  KasusBaruP                 INT,  
  TotalKasusBaru             INT,
  KunjunganL				 INT,  
  KunjunganP				 INT,  
  TotalKunjungan             INT   
  PRIMARY KEY(RlMasterReportItemID)  
 )      
   
 INSERT INTO #tmpRlTxReport51V2025  
   (  
     RlMasterReportItemID,  
     RlMasterReportItemCode,  
     L0001j,  
     P0001j,  
     L0001h,  
     P0001h,  
     L0007h,  
     P0007h,  
     L0828h,  
     P0828h,  
     L29h03b,  
     P29h03b,  
     L3b6b,  
     P3b6b,  
     L6b11b,  
     P6b11b,  
     L0104t,  
     P0104t,  
     L0509t,  
     P0509t,  
     L1014t,  
     P1014t,  
     L1519t,  
     P1519t,  
     L2024t,  
     P2024t,  
     L2529t,  
     P2529t,  
     L3034t,  
     P3034t,  
     L3539t,  
     P3539t,  
     L4044t,  
     P4044t,  
     L4549t,  
     P4549t,  
     L5054t,  
     P5054t,  
     L5559t,  
     P5559t,  
     L6064t,  
     P6064t,  
     L6569t,  
     P6569t,  
     L7074t,  
     P7074t,  
     L7579t,  
     P7579t,  
     L8084t,  
     P8084t,  
     L85t,  
     P85t,  
     KasusBaruL,  
     KasusBaruP,  
     TotalKasusBaru,
     KunjunganL,
     KunjunganP,  
     TotalKunjungan  
   )  
 SELECT x.RlMasterReportItemID,  
        x.RlMasterReportItemCode,  
        0                           L0001j,  
        0                           P0001j,  
        0                           L0001h,  
        0                           P0001h,  
        0                           L0007h,  
        0                           P0007h,  
        0                           L0828h,  
        0                           P0828h,  
        0                           L29h03b,  
        0                           P29h03b,  
        0                           L3b6b,  
        0                           P3b6b,  
        0                           L6b11b,  
        0                           P6b11b,  
        0                           L0104t,  
        0                           P0104t,  
        0                           L0509t,  
        0                           P0509t,  
        0                           L1014t,  
        0                           P1014t,  
        0                           L1519t,  
        0                           P1519t,  
        0                           L2024t,  
        0                           P2024t,  
        0                           L2529t,  
        0                           P2529t,  
        0                           L3034t,  
        0                           P3034t,  
        0                           L3539t,  
        0                           P3539t,  
        0                           L4044t,  
        0                           P4044t,  
        0                           L4549t,  
        0                           P4549t,  
        0                           L5054t,  
        0                           P5054t,  
        0                           L5559t,  
        0                           P5559t,  
        0                           L6064t,  
        0                           P6064t,  
        0                           L6569t,  
        0                           P6569t,  
        0                           L7074t,  
        0                           P7074t,  
        0                           L7579t,  
        0                           P7579t,  
        0                           L8084t,  
        0                           P8084t,  
        0                           L85t,  
        0                           P85t,  
        0                           KasusBaruL,  
        0                           KasusBaruP,  
        0                           TotalKasusBaru,
        0							KunjunganL,  
        0							KunjunganP,  
        0                           TotalKunjungan  
 FROM   RlMasterReportItemV2025     x  
 WHERE  x.RlMasterReportID = '22'      
   
 CREATE TABLE #tmpRlTxReport51V2025_Reff  
 (  
  RegistrationNo     VARCHAR(20),  
  DtdNo              VARCHAR(50),  
  AgeInDay           INT,  
  AgeInMonth         INT,  
  AgeInYear          INT,  
  Sex                VARCHAR(20),  
  IsOldCase          BIT  
 )      
 INSERT INTO #tmpRlTxReport51V2025_Reff  
   (  
     RegistrationNo,  
     DtdNo,  
     AgeInDay,  
     AgeInMonth,  
     AgeInYear,  
     Sex,  
     IsOldCase  
   )  
 SELECT a.RegistrationNo,  
        b.DtdNo,  
        c.AgeInDay,  
        c.AgeInMonth,  
        c.AgeInYear,  
        d.Sex,  
        a.IsOldCase  
 FROM   EpisodeDiagnose a WITH (NOLOCK)  
        INNER JOIN Diagnose b WITH (NOLOCK)  
             ON  b.DiagnoseID = a.DiagnoseID  
        INNER JOIN Registration  AS c WITH (NOLOCK)  
             ON  c.RegistrationNo = a.RegistrationNo  
        INNER JOIN Patient       AS d WITH (NOLOCK)  
             ON  d.PatientID = c.PatientID  
 WHERE  c.SRRegistrationType <> 'IPR'  
        AND MONTH(c.RegistrationDate) >= @p_FromMonth  
        AND MONTH(c.RegistrationDate) <= @p_ToMonth  
        AND YEAR(c.RegistrationDate) = @p_Year  
        AND c.IsVoid = 0  
        AND a.IsVoid = 0  
        AND b.DtdNo IN (SELECT x.RlMasterReportItemCode  
                        FROM   #tmpRlTxReport51V2025 AS x)      
   
 UPDATE x  
 SET    L0001j = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInDay <= 0  
                       AND a.AgeInMonth = 0  
                       AND a.AgeInYear = 0  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P0001j = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInDay <= 0  
                       AND a.AgeInMonth = 0  
                       AND a.AgeInYear = 0  
                       AND a.Sex = 'F' AND a.IsOldCase = 0   
            ),  
            0  
        ),  
        L0001h = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInDay <= 1  
                       AND a.AgeInMonth = 0  
                       AND a.AgeInYear = 0  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P0001h = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInDay <= 1  
                       AND a.AgeInMonth = 0  
                       AND a.AgeInYear = 0  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L0007h = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInDay > 1
                       AND a.AgeInDay <= 7  
                       AND a.AgeInMonth = 0  
                       AND a.AgeInYear = 0  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P0007h = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode
					   AND a.AgeInDay > 1  
                       AND a.AgeInDay <= 7  
                       AND a.AgeInMonth = 0  
                       AND a.AgeInYear = 0  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L0828h = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInDay > 7  
                       AND a.AgeInDay <= 28  
                       AND a.AgeInMonth = 0  
                       AND a.AgeInYear = 0  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P0828h = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInDay > 7  
                       AND a.AgeInDay <= 28  
                       AND a.AgeInMonth = 0  
                       AND a.AgeInYear = 0  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L29h03b = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND (  
                               (a.AgeInDay > 28 AND a.AgeInMonth = 0 AND a.AgeInYear = 0)  
                               OR (a.AgeInMonth >= 1 AND a.AgeInMonth < 3 AND a.AgeInYear = 0)
                           )  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P29h03b = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND (  
                               (a.AgeInDay > 28 AND a.AgeInMonth = 0 AND a.AgeInYear = 0)  
                               OR (a.AgeInMonth >= 1 AND a.AgeInMonth < 3 AND a.AgeInYear = 0) 
                           )  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L3b6b = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND (  
                               (a.AgeInMonth >= 3 AND a.AgeInMonth < 6 AND a.AgeInYear = 0)  
                           )  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P3b6b = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND (  
                               (a.AgeInMonth >= 3 AND a.AgeInMonth < 6 AND a.AgeInYear = 0) 
                           )  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L6b11b = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND (  
                               (a.AgeInMonth >= 6 AND a.AgeInMonth <= 11 AND a.AgeInYear = 0)  
                           )  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P6b11b = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND (  
                               (a.AgeInMonth >= 6 AND a.AgeInMonth <= 11 AND a.AgeInYear = 0)  
                           )  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L0104t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND (
                       		(a.AgeInYear >= 1  AND a.AgeInYear <= 4) OR (a.AgeInMonth >= 12 AND a.AgeInYear = 0)
						   )  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P0104t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                      AND (
                       		(a.AgeInYear >= 1  AND a.AgeInYear <= 4) OR (a.AgeInMonth >= 12 AND a.AgeInYear = 0)
						  )  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L0509t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 4  
                       AND a.AgeInYear <= 9  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P0509t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 4  
                       AND a.AgeInYear <= 9  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L1014t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 9  
                       AND a.AgeInYear <= 14  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P1014t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 9  
                       AND a.AgeInYear <= 14  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L1519t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 14  
                       AND a.AgeInYear <= 19  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P1519t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 14  
                       AND a.AgeInYear <= 19  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L2024t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 19  
                       AND a.AgeInYear <= 24  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P2024t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 19  
                       AND a.AgeInYear <= 24  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L2529t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 24  
                       AND a.AgeInYear <= 29  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P2529t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 24  
                       AND a.AgeInYear <= 29  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L3034t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 29  
                       AND a.AgeInYear <= 34  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P3034t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 29  
                       AND a.AgeInYear <= 34  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L3539t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 34  
                       AND a.AgeInYear <= 39  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P3539t = ISNULL(  
            (  
          SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 34  
                       AND a.AgeInYear <= 39  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L4044t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 39  
                       AND a.AgeInYear <= 44  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P4044t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 39  
                       AND a.AgeInYear <= 44  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L4549t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 44  
                       AND a.AgeInYear <= 49  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P4549t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 44  
                       AND a.AgeInYear <= 49  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L5054t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 49  
                       AND a.AgeInYear <= 54  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P5054t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 49  
                       AND a.AgeInYear <= 54  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L5559t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 54  
                       AND a.AgeInYear <= 59  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P5559t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 54  
                       AND a.AgeInYear <= 59  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L6064t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 59  
                       AND a.AgeInYear <= 64  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P6064t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 59  
					   AND a.AgeInYear <= 64  
					   AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L6569t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 64  
                       AND a.AgeInYear <= 69  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P6569t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 64  
                       AND a.AgeInYear <= 69  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L7074t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 69  
                       AND a.AgeInYear <= 74  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P7074t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 69  
                       AND a.AgeInYear <= 74  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L7579t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 74  
                       AND a.AgeInYear <= 79  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P7579t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 74  
                       AND a.AgeInYear <= 79  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L8084t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 79  
                       AND a.AgeInYear <= 84  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P8084t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 79  
                       AND a.AgeInYear <= 84  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        L85t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 84  
                       AND a.Sex = 'M' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        P85t = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.AgeInYear > 84  
                       AND a.Sex = 'F' AND a.IsOldCase = 0  
            ),  
            0  
        ),  
        KasusBaruL = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode
					   AND a.IsOldCase = 0  
                       AND a.Sex = 'M' 
            ),  
            0  
        ),  
        KasusBaruP = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.IsOldCase = 0  
                       AND a.Sex = 'F'
            ),  
            0  
        ),  
        TotalKasusBaru = 0,
        KunjunganL = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode
                       AND a.Sex = 'M' 
            ),  
            0  
        ),  
        KunjunganP = ISNULL(  
            (  
                SELECT COUNT(a.RegistrationNo)  
                FROM   #tmpRlTxReport51V2025_Reff AS a  
                WHERE  a.DtdNo = x.RlMasterReportItemCode  
                       AND a.Sex = 'F'
            ),  
            0  
        ),    
        TotalKunjungan = 0
 FROM   #tmpRlTxReport51V2025 x      
   
 INSERT INTO RlTxReport51V2025  
   (  
     RlTxReportNo,  
     RlMasterReportItemID,  
     L0001j,  
     P0001j,  
     L0001h,  
     P0001h,  
     L0007h,  
     P0007h,  
     L0828h,  
     P0828h,  
     L29h03b,  
     P29h03b,  
     L3b6b,  
     P3b6b,  
     L6b11b,  
     P6b11b,  
     L0104t,  
     P0104t,  
     L0509t,  
     P0509t,  
     L1014t,  
     P1014t,  
     L1519t,  
     P1519t,  
     L2024t,  
     P2024t,  
     L2529t,  
     P2529t,  
     L3034t,  
     P3034t,  
     L3539t,  
     P3539t,  
     L4044t,  
     P4044t,  
     L4549t,  
     P4549t,  
     L5054t,  
     P5054t,  
     L5559t,  
     P5559t,  
     L6064t,  
     P6064t,  
     L6569t,  
     P6569t,  
     L7074t,  
     P7074t,  
     L7579t,  
     P7579t,  
     L8084t,  
     P8084t,  
     L85t,  
     P85t,  
     KasusBaruL,  
     KasusBaruP,  
     TotalKasusBaru,
     KunjunganL,
     KunjunganP,  
     TotalKunjungan,  
     LastUpdateDateTime,  
     LastUpdateByUserID  
   )  
 SELECT @p_RlTxReportNo           RlTxReportNo,  
        x.RlMasterReportItemID,  
        x.L0001j,  
        x.P0001j,  
        x.L0001h,  
        x.P0001h,  
        x.L0007h,  
        x.P0007h,  
        x.L0828h,  
        x.P0828h,  
        x.L29h03b,  
        x.P29h03b,  
        x.L3b6b,  
        x.P3b6b,  
        x.L6b11b,  
        x.P6b11b,  
        x.L0104t,  
        x.P0104t,  
        x.L0509t,  
        x.P0509t,  
        x.L1014t,  
        x.P1014t,  
        x.L1519t,  
        x.P1519t,  
        x.L2024t,  
        x.P2024t,  
        x.L2529t,  
        x.P2529t,  
        x.L3034t,  
        x.P3034t,  
        x.L3539t,  
        x.P3539t,  
        x.L4044t,  
        x.P4044t,  
        x.L4549t,  
        x.P4549t,  
        x.L5054t,  
        x.P5054t,  
        x.L5559t,  
        x.P5559t,  
        x.L6064t,  
        x.P6064t,  
        x.L6569t,  
        x.P6569t,  
        x.L7074t,  
        x.P7074t,  
        x.L7579t,  
        x.P7579t,  
        x.L8084t,  
        x.P8084t,  
        x.L85t,  
        x.P85t,  
        x.KasusBaruL,  
        x.KasusBaruP,  
        (x.KasusBaruL + x.KasusBaruP) TotalKasusBaru,
        x.KunjunganL,
        x.KunjunganP,  
        (x.KunjunganL + x.KunjunganP) TotalKunjungan,  
        GETDATE()                 LastUpdateDateTime,  
        @p_UserID                 LastUpdateByUserID  
 FROM   #tmpRlTxReport51V2025  AS x  
 ORDER BY  
        x.RlMasterReportItemID   
   
 DROP TABLE #tmpRlTxReport51V2025   
 DROP TABLE #tmpRlTxReport51V2025_Reff