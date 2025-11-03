

ALTER TABLE MedicalDischargeSummary
	ADD 
		SRTypeOfService VARCHAR(20),
		SRCauseOfDisease VARCHAR(20),
		SRCauseOfDevelopDisorder VARCHAR(50),
		CauseOfDevelopDisorder	VARCHAR(1000),
		SRNatureOfSurgery	VARCHAR(20),
		IsInstruction1		BIT NULL,
		IsInstruction2		BIT NULL,
		IsInstruction3		BIT NULL,
		Instruction3		VARCHAR(500) NULL,
		IsInstruction4		BIT NULL,
		Instruction4		VARCHAR(500) NULL,
		IsInstruction5		BIT NULL,
		Instruction5		VARCHAR(500) NULL,
		IsInstruction6		BIT NULL,
		Instruction6		VARCHAR(500) NULL
		