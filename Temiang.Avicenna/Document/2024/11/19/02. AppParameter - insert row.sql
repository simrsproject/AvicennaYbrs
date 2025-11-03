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
	'RegistrationWithMobileAppDayLimit',
	'Registration With Mobile App Day Limit (H + ? from today)',
	'2',
	'',
	GETDATE(),
	'sci',
	0,
	NULL
)