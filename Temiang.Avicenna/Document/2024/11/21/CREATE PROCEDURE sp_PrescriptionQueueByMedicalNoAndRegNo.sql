
CREATE PROCEDURE sp_PrescriptionQueueByMedicalNoAndRegisNo

		@MedicalNo VARCHAR(50),
		@RegistrationNo VARCHAR(50)
AS

--DECLARE @MedicalNo          VARCHAR(50) = '',
--        @RegistrationNo     VARCHAR(50) = 'REG/EM/241120-0059'

DECLARE @healthcareInit     VARCHAR(20)
DECLARE @p1                 VARCHAR(50) = 'Processing'
DECLARE @p2                 VARCHAR(50) = 'Processing'

-- Ambil parameter nilai healthcare
SET @healthcareInit = (
    SELECT ap.ParameterValue
    FROM   AppParameter AS ap
    WHERE  ap.ParameterID = 'HealthcareInitialAppsVersion'
);

-- Cek apakah @MedicalNo atau @RegistrationNo memiliki nilai
IF (@MedicalNo <> '' AND @MedicalNo IS NOT NULL)
BEGIN
    -- Menampilkan hasil berdasarkan @MedicalNo
    SELECT *,
           CASE [Status]
                WHEN 4 THEN 'Delivered'
                WHEN 3 THEN 'Finish'
                WHEN 2 THEN @p2
                WHEN 1 THEN @p1
                ELSE 'Waiting'
           END [StatusName]
    FROM (
        SELECT b.MedicalNo,
               b.RegistrationNo,
               b.Ssn,
               b.ExternalQueNo,
               b.PrescriptionNo,
               b.[Status],
               b.PatientName,
               CASE COUNT(b.IsCompound)
                    WHEN 1 THEN ''
                    ELSE 'R'
               END Flag
        FROM (
            SELECT DISTINCT 
                   p.MedicalNo,
                   r.RegistrationNo,
                   p.Ssn,
                   tp.KioskQueueNo ExternalQueNo,
                   tp.PrescriptionNo,
                   CASE 
                        WHEN (tp.DeliverDateTime IS NOT NULL) THEN 4
                        WHEN (tp.CompleteDateTime IS NOT NULL) THEN 3
                        WHEN (tp.IsProceedByPharmacist = 1) THEN 2
                        ELSE 1
                   END [Status],
                   tp.CompleteDateTime,
                   LEFT(RTRIM(RTRIM(p.FirstName + ' ' + p.MiddleName + ' ') + p.LastName), 25) PatientName,
                   tpi.IsCompound
            FROM TransPrescription AS tp
            INNER JOIN TransPrescriptionItem AS tpi
                ON tpi.PrescriptionNo = tp.PrescriptionNo
            INNER JOIN Registration AS r
                ON r.RegistrationNo = tp.RegistrationNo
            INNER JOIN Patient AS p
                ON p.PatientID = r.PatientID
            WHERE p.MedicalNo = @MedicalNo
              AND CAST(tp.ApprovalDateTime AS DATE) = CAST(GETDATE() AS DATE)
              AND tp.IsVoid = 0
        ) b
        WHERE b.[Status] IN (1, 2, 3)
          AND b.ExternalQueNo IS NOT NULL
        GROUP BY b.MedicalNo, b.RegistrationNo, b.Ssn, b.ExternalQueNo, b.PrescriptionNo, b.[Status], b.PatientName
    ) a
END
ELSE IF (@RegistrationNo <> '' AND @RegistrationNo IS NOT NULL)
BEGIN
    -- Menampilkan hasil berdasarkan @RegistrationNo
    SELECT *,
           CASE [Status]
                WHEN 4 THEN 'Delivered'
                WHEN 3 THEN 'Finish'
                WHEN 2 THEN @p2
                WHEN 1 THEN @p1
                ELSE 'Waiting'
           END [StatusName]
    FROM (
        SELECT b.MedicalNo,
               b.RegistrationNo,
               b.Ssn,
               b.ExternalQueNo,
               b.PrescriptionNo,
               b.[Status],
               b.PatientName,
               CASE COUNT(b.IsCompound)
                    WHEN 1 THEN ''
                    ELSE 'R'
               END Flag
        FROM (
            SELECT DISTINCT 
                   p.MedicalNo,
                   r.RegistrationNo,
                   p.Ssn,
                   tp.KioskQueueNo ExternalQueNo,
                   tp.PrescriptionNo,
                   CASE 
                        WHEN (tp.DeliverDateTime IS NOT NULL) THEN 4
                        WHEN (tp.CompleteDateTime IS NOT NULL) THEN 3
                        WHEN (tp.IsProceedByPharmacist = 1) THEN 2
                        ELSE 1
                   END [Status],
                   tp.CompleteDateTime,
                   LEFT(RTRIM(RTRIM(p.FirstName + ' ' + p.MiddleName + ' ') + p.LastName), 25) PatientName,
                   tpi.IsCompound
            FROM TransPrescription AS tp
            INNER JOIN TransPrescriptionItem AS tpi
                ON tpi.PrescriptionNo = tp.PrescriptionNo
            INNER JOIN Registration AS r
                ON r.RegistrationNo = tp.RegistrationNo
            INNER JOIN Patient AS p
                ON p.PatientID = r.PatientID
            WHERE r.RegistrationNo = @RegistrationNo
              AND tp.IsVoid = 0
        ) b
        WHERE b.[Status] IN (1, 2, 3)
          AND b.ExternalQueNo IS NOT NULL
        GROUP BY b.MedicalNo, b.RegistrationNo, b.Ssn, b.ExternalQueNo, b.PrescriptionNo, b.[Status], b.PatientName
    ) a
END
ELSE
BEGIN
    -- Menampilkan hasil berdasarkan @MedicalNo dan @RegistrationNo
    SELECT *,
           CASE [Status]
                WHEN 4 THEN 'Delivered'
                WHEN 3 THEN 'Finish'
                WHEN 2 THEN @p2
                WHEN 1 THEN @p1
                ELSE 'Waiting'
           END [StatusName]
    FROM (
        SELECT b.MedicalNo,
               b.RegistrationNo,
               b.Ssn,
               b.ExternalQueNo,
               b.PrescriptionNo,
               b.[Status],
               b.PatientName,
               CASE COUNT(b.IsCompound)
                    WHEN 1 THEN ''
                    ELSE 'R'
               END Flag
        FROM (
            SELECT DISTINCT 
                   p.MedicalNo,
                   r.RegistrationNo,
                   p.Ssn,
                   tp.KioskQueueNo ExternalQueNo,
                   tp.PrescriptionNo,
                   CASE 
                        WHEN (tp.DeliverDateTime IS NOT NULL) THEN 4
                        WHEN (tp.CompleteDateTime IS NOT NULL) THEN 3
                        WHEN (tp.IsProceedByPharmacist = 1) THEN 2
                        ELSE 1
                   END [Status],
                   tp.CompleteDateTime,
                   LEFT(RTRIM(RTRIM(p.FirstName + ' ' + p.MiddleName + ' ') + p.LastName), 25) PatientName,
                   tpi.IsCompound
            FROM TransPrescription AS tp
            INNER JOIN TransPrescriptionItem AS tpi
                ON tpi.PrescriptionNo = tp.PrescriptionNo
            INNER JOIN Registration AS r
                ON r.RegistrationNo = tp.RegistrationNo
            INNER JOIN Patient AS p
                ON p.PatientID = r.PatientID
            WHERE p.MedicalNo = @MedicalNo
              AND r.RegistrationNo = @RegistrationNo
              AND tp.IsVoid = 0
        ) b
        WHERE b.[Status] IN (1, 2, 3)
          AND b.ExternalQueNo IS NOT NULL
        GROUP BY b.MedicalNo, b.RegistrationNo, b.Ssn, b.ExternalQueNo, b.PrescriptionNo, b.[Status], b.PatientName
    ) a
END
