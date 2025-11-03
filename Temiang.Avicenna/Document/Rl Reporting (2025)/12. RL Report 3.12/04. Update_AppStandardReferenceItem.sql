/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 30/01/2025 09:54:07
 ************************************************************/

UPDATE asri
SET    asri.ItemName = 'Bedah Saraf (Non Stroke)'
FROM   AppStandardReferenceItem AS asri
WHERE  asri.StandardReferenceID = 'SurgerySpecialty'
       AND asri.ItemID = 003