IF NOT EXISTS (SELECT 0 FROM AppParameter AS ap WHERE ap.ParameterID = 'MaxChronicDrugPrescriptionInDays')
BEGIN
	INSERT INTO AppParameter
	(
		ParameterID,
		ParameterName,
		ParameterValue,
		ParameterType,
		LastUpdateDateTime,
		LastUpdateByUserID,
		IsUsedBySystem,
		[Message]
	)
	VALUES
	(
		'MaxChronicDrugPrescriptionInDays',
		'Maximum Chronic Drug Prescription in days',
		'30',
		'',
		GETDATE(),
		'sci',
		0,
		NULL
	)
END
ELSE
BEGIN
	UPDATE AppParameter
	SET
		ParameterValue = '30'
	WHERE ParameterID = 'MaxChronicDrugPrescriptionInDays'
END