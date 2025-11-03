/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 27/02/2025 16:03:58
 ************************************************************/

UPDATE ap
SET    ap.IsUsedBySystem = 0
FROM   AppParameter AS ap
WHERE  ap.ParameterID = 'ServiceUnitKiaId'