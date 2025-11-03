-- Query ini hanya dijalankan untuk RSI

USE RSI

INSERT INTO AppControl
(
	ControlID,
	ControlType,
	[Description],
	ControlUrl
)
VALUES
(
	'FollowUpPlanOprCtl',
	'Assessment',
	'',
	'~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/FollowUpPlanOprCtl.ascx'
)

UPDATE acem SET acem.ControlID = 'FollowUpPlanOprCtl'
FROM   AppControlEntryMatrix AS acem
WHERE  acem.ControlID LIKE '%Follow%'
       AND acem.HealthcareInitialAppsVersion = 'YBRSGKP'
       AND acem.EntryType IN ('ASSESSMENT-DENTS', 'ASSESSMENT-EYE', 'ASSESSMENT-GENRL', 'ASSESSMENT-HEART', 
                                  'ASSESSMENT-IGD', 'ASSESSMENT-INTER' , 'ASSESSMENT-LUNG', 
								  'ASSESSMENT-NEURO', 'ASSESSMENT-NURSE', 'ASSESSMENT-NUTRI', 'ASSESSMENT-PKAND', 
                                  'ASSESSMENT-PSYCY', 'ASSESSMENT-REHAB', 'ASSESSMENT-SKIN', 'ASSESSMENT-SURGI', 
                                  'ASSESSMENT-THT', 'ASSESSMENT-CONT')