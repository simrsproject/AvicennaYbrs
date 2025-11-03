ALTER TABLE dbo.MedicalDischargeSummaryBak ADD 
HomeCare VARCHAR(500),
EducationAtHome VARCHAR(500),
Consul VARCHAR(500),
MedicalSupport VARCHAR(500),
InLocation VARCHAR(500),
CollectionDateTime DATETIME,
InitialDiagnose VARCHAR(4000)

GO
ALTER TABLE dbo.MedicalDischargeSummaryBak ADD
	SRTypeOfService VARCHAR(20) NULL,
	SRCauseOfDisease VARCHAR(20) NULL,
	SRCauseOfDevelopDisorder VARCHAR(50) NULL,
	CauseOfDevelopDisorder VARCHAR(1000) NULL,
	SRNatureOfSurgery VARCHAR(20) NULL,
	IsInstruction1 BIT NULL,
	IsInstruction2 BIT NULL,
	IsInstruction3 BIT NULL,
	Instruction3 VARCHAR(500) NULL,
	IsInstruction4 BIT NULL,
	Instruction4 VARCHAR(500) NULL,
	IsInstruction5 BIT NULL,
	Instruction5 VARCHAR(500) NULL,
	IsInstruction6 BIT NULL,
	Instruction6 VARCHAR(500) NULL