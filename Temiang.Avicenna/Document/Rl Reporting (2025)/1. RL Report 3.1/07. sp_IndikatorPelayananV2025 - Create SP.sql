/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 05/02/2025 16:53:45
 ************************************************************/

CREATE PROCEDURE sp_IndikatorPelayananV2025
	@p_FromMonth		VARCHAR(2), 
	@p_ToMonth			VARCHAR(2),
	@p_Year				VARCHAR(4),
	@p_UserID VARCHAR(15)
AS

--DECLARE @p_FromMonth VARCHAR(2) = '',
--        @p_ToMonth VARCHAR(2) = '',
--        @p_Year VARCHAR(4) = ''

DECLARE @StartDate DATETIME = CAST(@p_FromMonth + '/1/' + @p_Year AS DATETIME)
DECLARE @PgDate DATETIME = CAST(@p_ToMonth + '/1/' + @p_Year AS DATETIME)
DECLARE @EndDate            DATETIME = DATEADD(DAY, -1, DATEADD(MONTH, 1, @PgDate))


DECLARE @ServiceUnitIDICU     VARCHAR(100) = (
            SELECT rmri.ParameterValue
            FROM   RlMasterReportItemV2025 AS rmri
            WHERE  rmri.RlMasterReportID = '1'
                   AND rmri.RlMasterReportItemNo = '02'
)

DECLARE @ServiceUnitIDNICU     VARCHAR(100) = (
            SELECT rmri.ParameterValue
            FROM   RlMasterReportItemV2025 AS rmri
            WHERE  rmri.RlMasterReportID = '1'
                   AND rmri.RlMasterReportItemNo = '03'
)

DECLARE @ServiceUnitIDPICU     VARCHAR(100) = (
            SELECT rmri.ParameterValue
            FROM   RlMasterReportItemV2025 AS rmri
            WHERE  rmri.RlMasterReportID = '1'
                   AND rmri.RlMasterReportItemNo = '04'
)

DECLARE @ServiceUnitIDIntensifLainnya     VARCHAR(100) = (
            SELECT rmri.ParameterValue
            FROM   RlMasterReportItemV2025 AS rmri
            WHERE  rmri.RlMasterReportID = '1'
                   AND rmri.RlMasterReportItemNo = '05'
)

DECLARE @HariPerawatanNonIntensif     INT =(
            SELECT SUM(cb.Balance)  AS Balance
            FROM   CensusBalance    AS cb
            WHERE  cb.CensusDate >= @StartDate
                   AND cb.CensusDate <= @EndDate
                   AND cb.ClassID = ''
                   AND cb.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
                   AND cb.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
                   AND cb.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
                   AND cb.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
)

DECLARE @HariPerawatanICU     INT =(
            SELECT SUM(cb.Balance)  AS Balance
            FROM   CensusBalance    AS cb
            WHERE  cb.CensusDate >= @StartDate
                   AND cb.CensusDate <= @EndDate
                   AND cb.ClassID = ''
                   AND cb.ServiceUnitID IN (SELECT Element
                                            FROM   dbo.func_split(@ServiceUnitIDICU, ','))
)

DECLARE @HariPerawatanNICU     INT =(
            SELECT SUM(cb.Balance)  AS Balance
            FROM   CensusBalance    AS cb
            WHERE  cb.CensusDate >= @StartDate
                   AND cb.CensusDate <= @EndDate
                   AND cb.ClassID = ''
                   AND cb.ServiceUnitID IN (SELECT Element
                                            FROM   dbo.func_split(@ServiceUnitIDNICU, ','))
)

DECLARE @HariPerawatanPICU     INT =(
            SELECT SUM(cb.Balance)  AS Balance
            FROM   CensusBalance    AS cb
            WHERE  cb.CensusDate >= @StartDate
                   AND cb.CensusDate <= @EndDate
                   AND cb.ClassID = ''
                   AND cb.ServiceUnitID IN (SELECT Element
                                            FROM   dbo.func_split(@ServiceUnitIDPICU, ','))
)

DECLARE @HariPerawatanIntensifLainnya     INT =(
            SELECT SUM(cb.Balance)  AS Balance
            FROM   CensusBalance    AS cb
            WHERE  cb.CensusDate >= @StartDate
                   AND cb.CensusDate <= @EndDate
                   AND cb.ClassID = ''
                   AND cb.ServiceUnitID IN (SELECT Element
                                            FROM   dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
        )

DECLARE @LamaDiRawatNonIntensif     INT =(
            SELECT SUM(
                       CASE 
                            WHEN DATEDIFF(DAY, x.RegistrationDate, x.DischargeDate) = 0 THEN 1
                            ELSE DATEDIFF(DAY, x.RegistrationDate, x.DischargeDate)
                       END
                   )  AS LamaDiRawatNonIntensif
            FROM   (
                       SELECT r.RegistrationDate,
                              CASE 
                                   WHEN ISNULL(r.DischargeDate, @EndDate) > @EndDate THEN @EndDate
                                   ELSE ISNULL(r.DischargeDate, @EndDate)
                              END AS DischargeDate
                       FROM   Registration AS r
                       WHERE  r.DischargeDate >= @StartDate
                              AND r.DischargeDate <= @EndDate
                              AND r.IsVoid = 0
                              AND r.SRRegistrationType = 'IPR'
                              AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
                              AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
                              AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
                              AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
                   )     x
)

DECLARE @LamaDiRawatICU     INT =(
            SELECT SUM(
                       CASE 
                            WHEN DATEDIFF(DAY, x.RegistrationDate, x.DischargeDate) = 0 THEN 1
                            ELSE DATEDIFF(DAY, x.RegistrationDate, x.DischargeDate)
                       END
                   )  AS LamaDiRawatICU
            FROM   (
                       SELECT r.RegistrationDate,
                              CASE 
                                   WHEN ISNULL(r.DischargeDate, @EndDate) > @EndDate THEN @EndDate
                                   ELSE ISNULL(r.DischargeDate, @EndDate)
                              END AS DischargeDate
                       FROM   Registration AS r
                       WHERE  r.DischargeDate >= @StartDate
                              AND r.DischargeDate <= @EndDate
                              AND r.IsVoid = 0
                              AND r.SRRegistrationType = 'IPR'
                              AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
                   )     x
)

DECLARE @LamaDiRawatNICU     INT =(
            SELECT SUM(
                       CASE 
                            WHEN DATEDIFF(DAY, x.RegistrationDate, x.DischargeDate) = 0 THEN 1
                            ELSE DATEDIFF(DAY, x.RegistrationDate, x.DischargeDate)
                       END
                   )  AS LamaDiRawatNICU
            FROM   (
                       SELECT r.RegistrationDate,
                              CASE 
                                   WHEN ISNULL(r.DischargeDate, @EndDate) > @EndDate THEN @EndDate
                                   ELSE ISNULL(r.DischargeDate, @EndDate)
                              END AS DischargeDate
                       FROM   Registration AS r
                       WHERE  r.DischargeDate >= @StartDate
                              AND r.DischargeDate <= @EndDate
                              AND r.IsVoid = 0
                              AND r.SRRegistrationType = 'IPR'
                              AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
                   )     x
)

DECLARE @LamaDiRawatPICU     INT =(
            SELECT SUM(
                       CASE 
                            WHEN DATEDIFF(DAY, x.RegistrationDate, x.DischargeDate) = 0 THEN 1
                            ELSE DATEDIFF(DAY, x.RegistrationDate, x.DischargeDate)
                       END
                   )  AS LamaDiRawatPICU
            FROM   (
                       SELECT r.RegistrationDate,
                              CASE 
                                   WHEN ISNULL(r.DischargeDate, @EndDate) > @EndDate THEN @EndDate
                                   ELSE ISNULL(r.DischargeDate, @EndDate)
                              END AS DischargeDate
                       FROM   Registration AS r
                       WHERE  r.DischargeDate >= @StartDate
                              AND r.DischargeDate <= @EndDate
                              AND r.IsVoid = 0
                              AND r.SRRegistrationType = 'IPR'
                              AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
                   )     x
)

DECLARE @LamaDiRawatIntensifLainnya     INT =(
            SELECT SUM(
                       CASE 
                            WHEN DATEDIFF(DAY, x.RegistrationDate, x.DischargeDate) = 0 THEN 1
                            ELSE DATEDIFF(DAY, x.RegistrationDate, x.DischargeDate)
                       END
                   )  AS LamaDiRawatIntensifLainnya
            FROM   (
                       SELECT r.RegistrationDate,
                              CASE 
                                   WHEN ISNULL(r.DischargeDate, @EndDate) > @EndDate THEN @EndDate
                                   ELSE ISNULL(r.DischargeDate, @EndDate)
                              END AS DischargeDate
                       FROM   Registration AS r
                       WHERE  r.DischargeDate >= @StartDate
                              AND r.DischargeDate <= @EndDate
                              AND r.IsVoid = 0
                              AND r.SRRegistrationType = 'IPR'
                              AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
                   )     x
        )

DECLARE                                      @KeluarNonIntensif          INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
)

DECLARE                                      @KeluarICU          INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
)

DECLARE                                      @KeluarNICU          INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
)

DECLARE                                      @KeluarPICU          INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
)

DECLARE                                      @KeluarIntensifLainnya          INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
        )

DECLARE @DischargeConditionDieLessThen48     VARCHAR(20) = (
            SELECT ap.ParameterValue
            FROM   AppParameter AS ap
            WHERE  ap.ParameterID = 'DischargeConditionDieLessThen48'
        )

DECLARE @DischargeConditionDieMoreThen48     VARCHAR(20) = (
            SELECT ap.ParameterValue
            FROM   AppParameter AS ap
            WHERE  ap.ParameterID = 'DischargeConditionDieMoreThen48'
        )

DECLARE @KeluarMati48NonIntensif     INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.SRDischargeCondition IN (@DischargeConditionDieMoreThen48)
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
)

DECLARE @KeluarMati48ICU     INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.SRDischargeCondition IN (@DischargeConditionDieMoreThen48)
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
)

DECLARE @KeluarMati48NICU     INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.SRDischargeCondition IN (@DischargeConditionDieMoreThen48)
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
)

DECLARE @KeluarMati48PICU     INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.SRDischargeCondition IN (@DischargeConditionDieMoreThen48)
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
)

DECLARE @KeluarMati48IntensifLainnya     INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.SRDischargeCondition IN (@DischargeConditionDieMoreThen48)
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
        )

DECLARE @KeluarMatiNonIntensif     INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.SRDischargeCondition IN (@DischargeConditionDieLessThen48, @DischargeConditionDieMoreThen48)
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
                   AND r.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
)

DECLARE @KeluarMatiICU     INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.SRDischargeCondition IN (@DischargeConditionDieLessThen48, @DischargeConditionDieMoreThen48)
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
)

DECLARE @KeluarMatiNICU     INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.SRDischargeCondition IN (@DischargeConditionDieLessThen48, @DischargeConditionDieMoreThen48)
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
)

DECLARE @KeluarMatiPICU     INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.SRDischargeCondition IN (@DischargeConditionDieLessThen48, @DischargeConditionDieMoreThen48)
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
)

DECLARE @KeluarMatiIntensifLainnya     INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.DischargeDate >= @StartDate
                   AND r.DischargeDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'IPR'
                   AND r.SRDischargeCondition IN (@DischargeConditionDieLessThen48, @DischargeConditionDieMoreThen48)
                   AND r.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
        )

/*start total tempat tidur*/
DECLARE         @BedDate        DATETIME =(
            SELECT TOP 1 nob.StartingDate
            FROM   NumberOfBed AS nob
            WHERE  nob.StartingDate <= @EndDate
            ORDER BY
                   nob.StartingDate DESC
        )

DECLARE @TtNonIntensif     INT =(
            SELECT SUM(nob.NumberOfBed)
            FROM   NumberOfBed AS nob
            WHERE  nob.StartingDate = @BedDate
            AND nob.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
            AND nob.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
            AND nob.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
            AND nob.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
)

DECLARE @TtICU     INT =(
            SELECT SUM(nob.NumberOfBed)
            FROM   NumberOfBed AS nob
            WHERE  nob.StartingDate = @BedDate
            AND nob.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
)

DECLARE @TtNICU     INT =(
            SELECT SUM(nob.NumberOfBed)
            FROM   NumberOfBed AS nob
            WHERE  nob.StartingDate = @BedDate
            AND nob.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
)

DECLARE @TtPICU     INT =(
            SELECT SUM(nob.NumberOfBed)
            FROM   NumberOfBed AS nob
            WHERE  nob.StartingDate = @BedDate
            AND nob.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
)

DECLARE @TtIntensifLainnya     INT =(
            SELECT SUM(nob.NumberOfBed)
            FROM   NumberOfBed AS nob
            WHERE  nob.StartingDate = @BedDate
            AND nob.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
        )

CREATE TABLE #msBed
(
	StartingDate      DATE,
	akhir             DATE,
	hr                NUMERIC(18, 2),
	ServiceUnitID     VARCHAR(20),
	Jbed              NUMERIC(18, 2),
	Tbed              NUMERIC(18, 2)
)

INSERT INTO #msBed
SELECT DISTINCT nob.StartingDate,
       ISNULL(
           (
               SELECT TOP 1 DATEADD(DAY, -1, nb.StartingDate) hr
               FROM   NumberOfBed AS nb
               WHERE  nb.StartingDate > nob.StartingDate
               ORDER BY
                      nb.StartingDate ASC
           ),
           @EndDate
       )               akhir,
       (
           DATEDIFF(
               "Day",
               nob.StartingDate,
               (
                   CASE 
                        WHEN CAST(
                                 ISNULL(
                                     (
                                         SELECT TOP 1 DATEADD(DAY, -1, nb.StartingDate) hr
                                         FROM   NumberOfBed AS nb
                                         WHERE  nb.StartingDate > nob.StartingDate
                                         ORDER BY
                                                nb.StartingDate ASC
                                     ),
                                     @EndDate
                                 ) AS DATE
                             ) > CAST(@EndDate AS DATE) THEN @EndDate
                        ELSE ISNULL(
                                 (
                                     SELECT TOP 1 DATEADD(DAY, -1, nb.StartingDate) hr
                                     FROM   NumberOfBed AS nb
                                     WHERE  nb.StartingDate > nob.StartingDate
                                     ORDER BY
                                            nb.StartingDate ASC
                                 ),
                                 @EndDate
                             )
                   END
               )
           ) + 1
       )               hr,
       nob.ServiceUnitID,
       0,
       0
FROM   NumberOfBed  AS nob
WHERE  nob.StartingDate <= @EndDate

UPDATE mb
SET    mb.Jbed = (
           SELECT SUM(nob.NumberOfBed)
           FROM   NumberOfBed AS nob
           WHERE  nob.StartingDate = mb.StartingDate
                  AND nob.ServiceUnitID = mb.ServiceUnitID
       ),
       mb.Tbed = mb.hr * (
           SELECT SUM(nob.NumberOfBed)
           FROM   NumberOfBed AS nob
           WHERE  nob.StartingDate = mb.StartingDate
                  AND nob.ServiceUnitID = mb.ServiceUnitID
       )
FROM   #msBed AS mb

DECLARE @JTtNonIntensif INT =(
            SELECT SUM(mb.Tbed)
            FROM   #msBed mb
            WHERE mb.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
            AND mb.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
            AND mb.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
            AND mb.ServiceUnitID NOT IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
)

DECLARE @JTtICU INT =(
            SELECT SUM(mb.Tbed)
            FROM   #msBed mb
            WHERE mb.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDICU, ','))
)

DECLARE @JTtNICU INT =(
            SELECT SUM(mb.Tbed)
            FROM   #msBed mb
            WHERE mb.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDNICU, ','))
)

DECLARE @JTtPICU INT =(
            SELECT SUM(mb.Tbed)
            FROM   #msBed mb
            WHERE mb.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDPICU, ','))
)

DECLARE @JTtIntensifLainnya INT =(
            SELECT SUM(mb.Tbed)
            FROM   #msBed mb
            WHERE mb.ServiceUnitID IN (SELECT Element FROM dbo.func_split(@ServiceUnitIDIntensifLainnya, ','))
        )

DROP TABLE #msBed
/*end total tempat tidur*/

DECLARE @HariDlmSatuPeriode     INT = DATEDIFF(DAY, @StartDate, @EndDate) + 1

DECLARE @Kunjungan              INT =(
            SELECT COUNT(r.RegistrationNo)
            FROM   Registration AS r
            WHERE  r.RegistrationDate >= @StartDate
                   AND r.RegistrationDate <= @EndDate
                   AND r.IsVoid = 0
                   AND r.SRRegistrationType = 'OPR'
        )

DELETE RlTxReport31ItemV2025
WHERE  PeriodMonth >= @p_FromMonth
       AND PeriodMonth <= @p_ToMonth
       AND PeriodYear = @p_Year

INSERT INTO RlTxReport31ItemV2025
(
	PeriodMonth,
	PeriodYear,
	HariPerawatanNonIntensif,
	HariPerawatanICU,
	HariPerawatanNICU,
	HariPerawatanPICU,
	HariPerawatanIntensifLainnya,
	LamaDirawatNonIntensif,
	LamaDirawatICU,
	LamaDirawatNICU,
	LamaDirawatPICU,
	LamaDirawatIntensifLainnya,
	KeluarNonIntensif,
	KeluarICU,
	KeluarNICU,
	KeluarPICU,
	KeluarIntensifLainnya,
	KeluarMati48NonIntensif,
	KeluarMati48ICU,
	KeluarMati48NICU,
	KeluarMati48PICU,
	KeluarMati48IntensifLainnya,
	KeluarMatiNonIntensif,
	KeluarMatiICU,
	KeluarMatiNICU,
	KeluarMatiPICU,
	KeluarMatiIntensifLainnya,
	TtNonIntensif,
	TtICU,
	TtNICU,
	TtPICU,
	TtIntensifLainnya,
	HariDlmSatuPeriode,
	Kunjungan,
	LastUpdateDateTime,
	LastUpdateByUserID,
	JTtNonIntensif,
	JTtICU,
	JTtNICU,
	JTtPICU,
	JTtIntensifLainnya
)
SELECT
	@p_FromMonth,
	@p_Year,
	ISNULL(@HariPerawatanNonIntensif, 0) AS HariPerawatanNonIntensif,
	ISNULL(@HariPerawatanICU, 0) AS HariPerawatanICU,
	ISNULL(@HariPerawatanNICU, 0) AS HariPerawatanNICU,
	ISNULL(@HariPerawatanPICU, 0) AS HariPerawatanPICU,
	ISNULL(@HariPerawatanIntensifLainnya, 0) AS HariPerawatanIntensifLainnya,
	ISNULL(@LamaDirawatNonIntensif, 0) AS LamaDirawatNonIntensif,
	ISNULL(@LamaDirawatICU, 0) AS LamaDirawatICU,
	ISNULL(@LamaDirawatNICU, 0) AS LamaDirawatNICU,
	ISNULL(@LamaDirawatPICU, 0) AS LamaDirawatPICU,
	ISNULL(@LamaDirawatIntensifLainnya, 0) AS LamaDirawatIntensifLainnya,
	ISNULL(@KeluarNonIntensif, 0) AS KeluarNonIntensif,
	ISNULL(@KeluarICU, 0) AS KeluarICU,
	ISNULL(@KeluarNICU, 0) AS KeluarNICU,
	ISNULL(@KeluarPICU, 0) AS KeluarPICU,
	ISNULL(@KeluarIntensifLainnya, 0) AS KeluarIntensifLainnya,
	ISNULL(@KeluarMati48NonIntensif, 0) AS KeluarMati48NonIntensif,
	ISNULL(@KeluarMati48ICU, 0) AS KeluarMati48ICU,
	ISNULL(@KeluarMati48NICU, 0) AS KeluarMati48NICU,
	ISNULL(@KeluarMati48PICU, 0) AS KeluarMati48PICU,
	ISNULL(@KeluarMati48IntensifLainnya, 0) AS KeluarMati48IntensifLainnya,
	ISNULL(@KeluarMatiNonIntensif, 0) AS KeluarMatiNonIntensif,
	ISNULL(@KeluarMatiICU, 0) AS KeluarMatiICU,
	ISNULL(@KeluarMatiNICU, 0) AS KeluarMatiNICU,
	ISNULL(@KeluarMatiPICU, 0) AS KeluarMatiPICU,
	ISNULL(@KeluarMatiIntensifLainnya, 0) AS KeluarMatiIntensifLainnya,
	ISNULL(@TtNonIntensif, 0) AS TtNonIntensif,
	ISNULL(@TtICU, 0) AS TtICU,
	ISNULL(@TtNICU, 0) AS TtNICU,
	ISNULL(@TtPICU, 0) AS TtPICU,
	ISNULL(@TtIntensifLainnya, 0) AS TtIntensifLainnya,
	ISNULL(@HariDlmSatuPeriode, 0) AS HariDlmSatuPeriode,
	ISNULL(@Kunjungan, 0) AS Kunjungan,
	GETDATE(),
	@p_UserID,
	ISNULL(@JTtNonIntensif, 0) AS JTtNonIntensif,
	ISNULL(@JTtICU, 0) AS JTtICU,
	ISNULL(@JTtNICU, 0) AS JTtNICU,
	ISNULL(@JTtPICU, 0) AS JTtPICU,
	ISNULL(@JTtIntensifLainnya, 0) AS JTtIntensifLainnya