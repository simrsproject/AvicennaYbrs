ALTER TABLE dbo.PatientAllergy ADD
AllergenDate datetime NULL,
SRAllergyClinicalStatus VARCHAR(50) NULL,
SRAllergyVerificationStatus VARCHAR(50) NULL,
SRAllergyCategory VARCHAR(50) NULL

INSERT INTO appStandardReferenceItem VALUES(N'AllergyCategory',N'AllergyCategory-001',N'Food','',1,1,'2009-03-05T13:17:45.267',N'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO appStandardReferenceItem VALUES(N'AllergyCategory',N'AllergyCategory-002',N'Medication','',1,1,'2009-03-05T13:17:45.267',N'sci',N'AllergySatuSehat',NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO appStandardReferenceItem VALUES(N'AllergyCategory',N'AllergyCategory-003',N'Environment','',1,1,'2009-03-05T13:17:45.267',N'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO appStandardReferenceItem VALUES(N'AllergyCategory',N'AllergyCategory-004',N'Biologic','',1,1,'2009-03-05T13:17:45.267',N'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO appStandardReferenceItem VALUES(N'AllergyClinicalStatus',N'AllergyClinicalStatus-001',N'Active','',1,1,'2009-03-05T13:17:45.267',N'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO appStandardReferenceItem VALUES(N'AllergyClinicalStatus',N'AllergyClinicalStatus-002',N'Inactive','',1,1,'2009-03-05T13:17:45.267',N'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO appStandardReferenceItem VALUES(N'AllergyClinicalStatus',N'AllergyClinicalStatus-003',N'Resolved','',1,1,'2009-03-05T13:17:45.267',N'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO appStandardReferenceItem VALUES(N'AllergyVerificationStatus',N'AllergyVerificationStatus-001',N'Unconfirmed','',1,1,'2009-03-05T13:17:45.267',N'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO appStandardReferenceItem VALUES(N'AllergyVerificationStatus',N'AllergyVerificationStatus-002',N'Confirmed','',1,1,'2009-03-05T13:17:45.267',N'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO appStandardReferenceItem VALUES(N'AllergyVerificationStatus',N'AllergyVerificationStatus-003',N'Refuted','',1,1,'2009-03-05T13:17:45.267',N'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO appStandardReferenceItem VALUES(N'AllergyVerificationStatus',N'AllergyVerificationStatus-004',N'Entered-in-error','',1,1,'2009-03-05T13:17:45.267',N'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
