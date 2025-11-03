/***************************************************************************************************
Procedure:          spHis_Emr_MedicationHist_PrescriptionDetail
Create Date:        2014-10-17
Author:             Handono
Description:        Record untuk tampilan Prescription History
Used By:            Temiang.Avicenna.Module.RADT.Emr.MainContent.MedicalHistCtl.PrescriptionDetailInHTML 
Parameter(s):       @p_RegistrationNo - RegistrationNo
                    
Usage:              EXEC spHis_Emr_MedicationHist_PrescriptionDetail 
                      @p_RegistrationNo = 'REG/IP/241016-0036'
****************************************************************************************************
SUMMARY OF CHANGES
Date(yyyy-mm-dd)    Author              Comments
------------------- ------------------- ------------------------------------------------------------

***************************************************************************************************/

CREATE PROCEDURE spHis_Emr_MedicationHist_PrescriptionDetail (
  @p_RegistrationNo varchar(20)
) AS
BEGIN
SET 
  NOCOUNT ON;

SELECT 
  b.[PrescriptionNo], 
  b.[IsApproval], 
  b.[IsVoid], 
  b.[CreatedByUserID], 
  u.[UserName] AS 'CreatedByUserName', 
  b.[CreatedDateTime], 
  b.[VerifiedByUserID], 
  b.[VerifiedDateTime], 
  b.[CompleteDateTime], 
  b.[DeliverDateTime], 
  b.[IsUnitDosePrescription], 
  a.[SequenceNo], 
  b.[PrescriptionDate], 
  b.[ParamedicID], 
  c.[ItemName], 
  ii.[ItemName] AS 'ItemNameIntervention', 
  a.[ResultQty], 
  a.[SRItemUnit], 
  e.SRConsumeMethodName + ' ' + COALESCE(acdcpc.ItemName, '') AS SRConsumeMethodName, 
  COALESCE(
    a.[IsRFlag], 
    CAST(0 AS BIT)
  ) AS 'IsRFlag', 
  COALESCE(
    a.[IsCompound], 
    CAST(0 AS BIT)
  ) AS 'IsCompound', 
  g.[EmbalaceLabel], 
  a.[DosageQty], 
  a.[SRDosageUnit], 
  a.[EmbalaceQty], 
  b.[Note], 
  a.[ConsumeQty], 
  a.[SRConsumeUnit], 
  (a.ParentNo + a.SequenceNo) as OrderKey, 
  a.[LineAmount], 
  a.[Notes] 
FROM 
  (
    SELECT 
	b.[RegistrationNo],
        b.[PrescriptionNo], 
  b.[IsApproval], 
  b.[IsVoid], 
  b.[CreatedByUserID], 
  b.[CreatedDateTime], 
  b.[VerifiedByUserID], 
  b.[VerifiedDateTime], 
  b.[CompleteDateTime], 
  b.[DeliverDateTime], 
  b.[IsUnitDosePrescription], 
  b.[PrescriptionDate], 
  b.[ParamedicID], 
  b.[Note]
    FROM 
      [TransPrescription] b 
    WHERE 
      b.[RegistrationNo] = @p_RegistrationNo 
      AND b.[IsVoid] = 0
  ) b 
  INNER JOIN [TransPrescriptionItem] a ON a.[PrescriptionNo] = b.[PrescriptionNo] 
  LEFT JOIN [AppUser] u ON b.[CreatedByUserID] = u.[UserID] 
  INNER JOIN [Item] c ON a.[ItemID] = c.[ItemID] 
  LEFT JOIN [Item] ii ON a.[ItemInterventionID] = ii.[ItemID] 
  LEFT JOIN [ConsumeMethod] e ON a.[SRConsumeMethod] = e.[SRConsumeMethod] 
  LEFT JOIN [Embalace] g ON a.[EmbalaceID] = g.[EmbalaceID] 
  LEFT JOIN [AppStandardReferenceItem] acdcpc ON (
    acdcpc.[StandardReferenceID] = 'MedicationConsume' AND a.[Acpcdc] = acdcpc.[ItemID] 
  ) 
WHERE 
  b.[RegistrationNo] = @p_RegistrationNo  AND b.[IsVoid] = 0 
ORDER BY 
  b.[PrescriptionDate] DESC, 
  b.[PrescriptionNo] DESC, 
  [OrderKey] ASC
  END