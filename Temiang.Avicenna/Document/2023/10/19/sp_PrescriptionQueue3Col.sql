/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.0.35
 * Time: 9/19/2023 12:02:43
 ************************************************************/


-- sp_PrescriptionQueue '127.0.0.1', 'D01.02.02', 0

--SELECT * FROM ServiceUnit AS su WHERE su.ServiceUnitName LIKE '%farmasi%'

--SELECT * FROM TransPrescription AS tp WHERE tp.PrescriptionNo = 'RSO/200126-0001'

CREATE PROCEDURE sp_PrescriptionQueue3Col
	@IP VARCHAR(15),
	@ServiceUnitID VARCHAR(10),
	@iProgress INT
AS
--BEGIN
	--DECLARE 
	--	@ServiceUnitID VARCHAR(10) = 'D3.0.01.1',
	--	@IP VARCHAR(15) = '127.0.0.1', 
	--	@iProgress INT = 1
	
	DECLARE @healthcareInit VARCHAR(20) = (
	            SELECT ap.ParameterValue
	            FROM   AppParameter AS ap		WITH(NOLOCK)
	            WHERE  ap.ParameterID = 'HealthcareInitialAppsVersion'
	        )
	
	DECLARE @RowLen INT = 20,
	        @RowCount INT
	
	DECLARE @CurrentPage INT 
	
	IF NOT EXISTS(
	       SELECT [name]
	       FROM   sysobjects		WITH(NOLOCK)
	       WHERE  [name] LIKE 'PrescriptionQueueDisplay3Col'
	              AND xtype = 'U'
	   )
	BEGIN
	    CREATE TABLE PrescriptionQueueDisplay3Col
	    (
	    	[IP]              VARCHAR(15),
	    	ServiceUnitID     VARCHAR(10),
	    	Progress        INT,
	    	[CurrentPage]     INT
	    )
	END 
	
	DECLARE @Count INT = 0
	SELECT @Count = COUNT(IP)
	FROM   PrescriptionQueueDisplay3Col		WITH(NOLOCK)
	WHERE  IP                    = @IP
	       AND ServiceUnitID     = @ServiceUnitID
	       AND Progress			= @iProgress
	
	IF (@Count = 0)
	    INSERT INTO PrescriptionQueueDisplay3Col
	    VALUES
	      (
	        @IP,
	        @ServiceUnitID,
	        @iProgress,
	        0
	      )
	
	SELECT *,
	       CASE [Status]
	            WHEN 4 THEN 'Diserahkan'
	            WHEN 3 THEN 'Selesai'
	            WHEN 2 THEN 'Proses'
	            WHEN 1 THEN 'Order'
	            ELSE 'Waiting'
	       END [StatusName],
	       ROW_NUMBER() 
	       OVER(
	           ORDER BY [Status] DESC,
	           ISNULL(a.CompleteDateTime, ApprovalDateTime) /*a.Duration*/ ASC
	       ) AS rn
	INTO   #tmpPrescriptionQueDisplay
	FROM   (
	           SELECT b.PrescriptionNo,
	                  b.[Status],
	                  --b.Duration,
	                  CASE 
	                       WHEN [Status] > 2 THEN SUBSTRING(
	                                CONVERT(
	                                    VARCHAR,
	                                    CONVERT(TIME, b.CompleteDateTime - b.ApprovalDateTime),
	                                    108
	                                ),
	                                1,
	                                5
	                            )
	                       ELSE SUBSTRING(
	                                CONVERT(
	                                    VARCHAR,
	                                    CONVERT(TIME, GETDATE() - b.ApprovalDateTime),
	                                    108
	                                ),
	                                1,
	                                5
	                            )
	                  END     Duration,
	                  b.ApprovalDateTime,
	                  b.CompleteDateTime,
	                  b.PatientName,
	                  b.MedicalNo,
	                  CASE COUNT(b.IsCompound)
	                       WHEN 1 THEN ''
	                       ELSE 'R'
	                  END     Flag
	           FROM   (
	                      SELECT DISTINCT 
	                             --CASE tp.KioskQueueNo
	                             --     WHEN '' THEN tp.PrescriptionNo
	                             --     ELSE tp.KioskQueueNo
	                             --END     PrescriptionNo,	
	                             tp.PrescriptionNo, --tp.PrescriptionDate, tp.RegistrationNo,
	                             CASE 
	                                  WHEN (tp.DeliverDateTime IS NOT NULL) THEN 4
	                                  ELSE (
	                                           CASE 
	                                                WHEN (tp.CompleteDateTime IS NOT NULL) THEN 3
	                                                ELSE (
	                                                         CASE 
	                                                              WHEN (tp.IsProceedByPharmacist = 1) THEN 2
	                                                              ELSE (
	                                                                       CASE 
	                                                                            WHEN (tp.ApprovalDateTime IS NOT NULL) THEN 
	                                                                                 1
	                                                                            ELSE 0
	                                                                       END
	                                                                       --CASE WHEN ((isnull(p1.ApproveDate,isnull(p2.ApproveDate,p3.ApproveDate))) IS NOT NULL) THEN 1 ELSE 0 END
	                                                                   )
	                                                         END
	                                                     )
	                                           END
	                                       )
	                             END [Status],
	                             --SUBSTRING( convert(varchar, CONVERT(TIME,GETDATE() - tp.ApprovalDateTime),108),1,5) Duration,
	                             tp.ApprovalDateTime,
	                             --(isnull(p1.ApproveDate,isnull(p2.ApproveDate,isnull(p3.ApproveDate, tp.ApprovalDateTime)))) ApprovalDateTime,
	                             p.MedicalNo, 
	                             tp.CompleteDateTime,
	                             LEFT(
	                                 RTRIM(RTRIM(p.FirstName + ' ' + p.MiddleName + ' ') + p.LastName),
	                                 25
	                             )       PatientName,
	                             tpi.IsCompound
	                      FROM   TransPrescription AS tp		WITH(NOLOCK)
	                             INNER JOIN TransPrescriptionItem AS tpi		WITH(NOLOCK)
	                                  ON  tpi.PrescriptionNo = tp.PrescriptionNo
	                             INNER JOIN Registration AS r		WITH(NOLOCK)
	                                  ON  r.RegistrationNo = tp.RegistrationNo
	                             INNER JOIN Patient AS p		WITH(NOLOCK)
	                                  ON  p.PatientID = r.PatientID
	                      WHERE  CAST(tp.PrescriptionDate AS DATE) = CAST(GETDATE() AS DATE) --CAST('2017-04-01' AS DATE)
	                             AND tp.ServiceUnitID = @ServiceUnitID
	                  )       b
	           WHERE  b.[Status] = @iProgress --IN (1, 2, 3)
	           GROUP BY
	                  b.PrescriptionNo,
	                  b.[Status],
	                  CASE 
	                       WHEN [Status] > 2 THEN SUBSTRING(
	                                CONVERT(
	                                    VARCHAR,
	                                    CONVERT(TIME, b.CompleteDateTime - b.ApprovalDateTime),
	                                    108
	                                ),
	                                1,
	                                5
	                            )
	                       ELSE SUBSTRING(
	                                CONVERT(
	                                    VARCHAR,
	                                    CONVERT(TIME, GETDATE() - b.ApprovalDateTime),
	                                    108
	                                ),
	                                1,
	                                5
	                            )
	                  END,
	                  b.ApprovalDateTime,
	                  b.CompleteDateTime,
	                  b.PatientName,
	                  b.MedicalNo
	       )a
	ORDER BY
	       rn
	
	SELECT @CurrentPage = CurrentPage
	FROM   PrescriptionQueueDisplay3Col		WITH(NOLOCK)
	WHERE  IP                    = @IP
	       AND ServiceUnitID     = @ServiceUnitID
	       AND Progress        = @iProgress
	
	SELECT @RowCount = COUNT(rn)
	FROM   #tmpPrescriptionQueDisplay		WITH(NOLOCK)
	--PRINT (@RowLen * (@CurrentPage))
	IF ((@RowLen * @CurrentPage) < @RowCount)
	BEGIN
	    SET @CurrentPage = @CurrentPage + 1
	END
	ELSE
	BEGIN
	    SET @CurrentPage = 1
	END
	
	SELECT *
	FROM   #tmpPrescriptionQueDisplay		WITH(NOLOCK)
	WHERE  rn > (@CurrentPage - 1) * @RowLen
	       AND rn <= @CurrentPage * @RowLen
	
	UPDATE PrescriptionQueueDisplay3Col
	SET    CurrentPage = @CurrentPage
	WHERE  IP = @IP
	       AND ServiceUnitID = @ServiceUnitID
	       AND Progress = @iProgress 
	
	--SELECT * FROM #tmpPrescriptionQueDisplay
	
	DROP TABLE #tmpPrescriptionQueDisplay
--END