IF NOT EXISTS (SELECT 0 FROM AppParameter AS ap WHERE ap.ParameterID = 'IsUsingRoundingDownWithBalancing')
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
		'IsUsingRoundingDownWithBalancing',
		'Is Using Rounding Down With Balancing (Exp. amount=1450|1245, rounding=500, result=Yes:1500|1000;No:1000|1000)? (Yes/No)',
		'No',
		'',
		GETDATE(),
		'sci',
		0,
		NULL
	)
END
