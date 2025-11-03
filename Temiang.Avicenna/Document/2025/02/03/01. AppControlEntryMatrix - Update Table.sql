/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 2/3/2025 1:38:27 PM
 ************************************************************/

-- Query ini hanya dijalankan untuk RSI

USE RSI

UPDATE acem
SET    acem.ControlID = 'FollowUpPlanV2Ctl'
FROM   AppControlEntryMatrix AS acem
WHERE  acem.ControlID = 'FollowUpPlanOprCtl'
       AND acem.HealthcareInitialAppsVersion = 'YBRSGKP'
       AND acem.EntryType = 'ASSESSMENT-MCUGN'
       
       