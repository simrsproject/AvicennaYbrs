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

ALTER PROCEDURE spHis_Emr_MedicationHist_PrescriptionDetail (
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
  CreatedByUserName = (SELECT TOP 1 UserName FROM [AppUser] WHERE [UserID] = a.[CreatedByUserID]), 
  b.[CreatedDateTime], 
  b.[VerifiedByUserID], 
  b.[VerifiedDateTime], 
  b.[CompleteDateTime], 
  b.[DeliverDateTime], 
  b.[IsUnitDosePrescription], 
  a.[SequenceNo], 
  b.[PrescriptionDate], 
  b.[ParamedicID], 
  ItemName = ( SELECT TOP 1 ItemName FROM Item WHERE ItemID = a.ItemID), 
  ItemNameIntervention = ISNULL(( SELECT TOP 1 ItemName FROM Item WHERE ItemID = a.ItemInterventionID),''),
  a.[ResultQty], 
  a.[SRItemUnit],
  SRItemUnitName = ISNULL(iu.ItemName,''),
  e.SRConsumeMethodName + ' ' + ISNULL(acdcpc.ItemName, '') AS SRConsumeMethodName, 
  ISNULL(a.[IsRFlag], CAST(0 AS BIT)) AS 'IsRFlag', 
  ISNULL(a.[IsCompound], CAST(0 AS BIT)) AS 'IsCompound', 
  g.[EmbalaceLabel], 
  a.[DosageQty], 
  a.[SRDosageUnit], 
  SRDosageUnitName = ISNULL(du.ItemName,''),
  a.[EmbalaceQty], 
  b.[Note], 
  a.[ConsumeQty], 
  a.[SRConsumeUnit], 
  SRConsumeUnitName = ISNULL(cu.ItemName,''),
  (a.ParentNo + a.SequenceNo) as OrderKey, 
  a.[LineAmount], 
  a.[Notes] 
FROM 
 [TransPrescription] b
  INNER JOIN [TransPrescriptionItem] a ON a.[PrescriptionNo] = b.[PrescriptionNo] 
  LEFT JOIN [Embalace] g ON a.[EmbalaceID] = g.[EmbalaceID] 
  LEFT JOIN [ConsumeMethod] e ON  e.[SRConsumeMethod] = a.[SRConsumeMethod]
  LEFT JOIN [AppStandardReferenceItem] acdcpc ON acdcpc.[StandardReferenceID] = 'MedicationConsume' AND acdcpc.[ItemID] = a.[Acpcdc]  
  LEFT JOIN [AppStandardReferenceItem] iu ON iu.[StandardReferenceID] = 'ItemUnit' AND iu.[ItemID] = a.SRItemUnit 
  LEFT JOIN [AppStandardReferenceItem] cu ON cu.[StandardReferenceID] = 'DosageUnit' AND cu.[ItemID] = a.SRConsumeUnit 
  LEFT JOIN [AppStandardReferenceItem] du ON du.[StandardReferenceID] = 'DosageUnit' AND du.[ItemID] = a.SRDosageUnit
  
WHERE 
  b.[RegistrationNo] = @p_RegistrationNo  AND b.[IsVoid] = 0 

END