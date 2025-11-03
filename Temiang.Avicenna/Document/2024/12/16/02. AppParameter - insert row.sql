DECLARE @value VARCHAR(50) = '' //--> isi dengan program id cetakan SPK


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
	'EmployeeClinicalAssignmentLetterKomedRpt',
	'Program ID for Employee Clinical Assignment Letter - Komed',
	@value,
	'',
	GETDATE(),
	'sci',
	0,
	NULL
)