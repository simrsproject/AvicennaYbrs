INSERT INTO AppStandardReferenceItem
(
	StandardReferenceID,
	ItemID,
	ItemName,
	Note,
	IsUsedBySystem,
	IsActive,
	LastUpdateDateTime,
	LastUpdateByUserID,
	ReferenceID,
	coaID,
	subledgerID,
	CustomField,
	LineNumber,
	NumericValue,
	CustomField2
)
VALUES
(	'PrescReview',	'SERUN',	'Appropriate Room/Service Unit where the prescription originates',	'',	0,	1,	GETDATE(),	'',	'',	NULL,	NULL,	NULL,	16,	NULL,	NULL),
(	'PrescReview',	'DGSTB',	'Appropriate drug stability',	'',	0,	1,	GETDATE(),	'',	'',	NULL,	NULL,	NULL,	17,	NULL,	NULL)

DELETE AppStandardReferenceItemBridging WHERE StandardReferenceID='PrescReview'

DECLARE @SatuSehatBridgingTypeID AS VARCHAR(20)
SELECT @SatuSehatBridgingTypeID = ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'SatuSehatBridgingTypeID'

INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'SERUN',@SatuSehatBridgingTypeID,N'1.4',N'Apakah ruangan/unit asal resep sudah sesuai?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'DGSTB',@SatuSehatBridgingTypeID,N'2.3',N'Apakah stabilitas obat sudah sesuai?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'BWBH',@SatuSehatBridgingTypeID,N'1.1',N'Apakah nama, umur, jenis kelamin, berat badan dan tinggi badan pasien sudah sesuai?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'CONFR',@SatuSehatBridgingTypeID,N'3.1',N'Apakah ketepatan indikasi, dosis, dan waktu penggunaan obat sudah sesuai?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'CONMT',@SatuSehatBridgingTypeID,N'2.4',N'Apakah aturan dan cara penggunaan obat sudah sesuai?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'DRGDS',@SatuSehatBridgingTypeID,N'2.2',N'Apakah dosis dan jumlah obat sudah sesuai?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'DRGNM',@SatuSehatBridgingTypeID,N'2.1',N'Apakah nama obat, bentuk dan kekuatan sediaan sudah sesuai?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'NOALG',@SatuSehatBridgingTypeID,N'3.3',N'Apakah terdapat alergi dan reaksi obat yang tidak dikehendaki (ROTD)?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'NOCON',@SatuSehatBridgingTypeID,N'3.4',N'Apakah terdapat kontraindikasi pengobatan?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'NODUP',@SatuSehatBridgingTypeID,N'3.2',N'Apakah terdapat duplikasi pengobatan?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'NOINT',@SatuSehatBridgingTypeID,N'3.5',N'Apakah terdapat dampak interaksi obat?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'PREDT',@SatuSehatBridgingTypeID,N'1.3',N'Apakah tanggal resep sudah sesuai?',NULL,NULL);
INSERT INTO AppStandardReferenceItemBridging VALUES(N'PrescReview',N'PSCID',@SatuSehatBridgingTypeID,N'1.2',N'Apakah nama, nomor ijin, alamat dan paraf dokter sudah sesuai?',NULL,NULL);


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
	'SatuSehatConsentUrl',
	'SatuSehat Consent Url',
	'https://api-satusehat.kemkes.go.id/consent/v1',
	'',
	GETDATE(),
	'',
	1,
	''
)