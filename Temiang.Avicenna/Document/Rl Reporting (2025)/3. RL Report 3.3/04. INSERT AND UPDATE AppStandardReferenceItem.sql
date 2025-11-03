/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 21/01/2025 13:49:40
 ************************************************************/

INSERT INTO [AppstandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'PatientInCondition',
    N'08',
    N'LUKA-LUKA',
    '',
    0,
    1,
    '2025-01-16 01:02:22.780',
    N'kews',
    N'EMR',
    0,
    0,
    N'LK',
    0,
    0,
    ''
  );

UPDATE asri
SET    asri.ItemName = 'Psikiatrik'
FROM   AppStandardReferenceItem AS asri
WHERE  asri.StandardReferenceID = 'ERCaseType'
       AND asri.ItemID = 04
GO

UPDATE asri
SET    asri.ItemName = 'Bayi'
FROM   AppStandardReferenceItem AS asri
WHERE  asri.StandardReferenceID = 'ERCaseType'
       AND asri.ItemID = 05
GO

INSERT INTO [AppstandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'ERCaseType',
    N'06',
    N'Anak',
    '',
    0,
    1,
    '2025-01-17 14:59:41.667',
    N'kews',
    '',
    0,
    0,
    '',
    0,
    0,
    ''
  );
INSERT INTO [AppstandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'ERCaseType',
    N'07',
    N'Geriatri',
    '',
    0,
    1,
    '2025-01-17 14:59:41.723',
    N'kews',
    '',
    0,
    0,
    '',
    0,
    0,
    ''
  );

INSERT INTO [AppStandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'VisitReason',
    N'016',
    N'Kecelakaan lalu lintas darat',
    '',
    0,
    1,
    '2025-01-17 11:43:33.663',
    N'kews',
    '',
    0,
    0,
    '',
    0,
    0,
    ''
  );
INSERT INTO [AppStandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'VisitReason',
    N'017',
    N'Kecelakaan lalu lintas perairan',
    '',
    0,
    1,
    '2025-01-17 11:43:33.693',
    N'kews',
    '',
    0,
    0,
    '',
    0,
    0,
    ''
  );
INSERT INTO [AppStandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'VisitReason',
    N'018',
    N'Kecelakaan lalu lintas udara',
    '',
    0,
    1,
    '2025-01-17 11:43:33.723',
    N'kews',
    '',
    0,
    0,
    '',
    0,
    0,
    ''
  );
INSERT INTO [AppStandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'VisitReason',
    N'019',
    N'Bedah lainnya (non kecelakaan)',
    '',
    0,
    1,
    '2025-01-17 11:43:33.753',
    N'kews',
    '',
    0,
    0,
    '',
    0,
    0,
    ''
  );
INSERT INTO [AppStandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'VisitReason',
    N'020',
    N'Kekerasan terhadap Perempuan (>=18 tahun)',
    '',
    0,
    1,
    '2025-01-20 12:09:14.677',
    N'kews',
    '',
    0,
    0,
    '',
    0,
    0,
    ''
  );
INSERT INTO [AppStandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'VisitReason',
    N'021',
    N'Kekerasan terhadap Anak (<18 tahun)',
    '',
    0,
    1,
    '2025-01-17 11:43:33.827',
    N'kews',
    '',
    0,
    0,
    '',
    0,
    0,
    ''
  );
INSERT INTO [AppStandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'VisitReason',
    N'022',
    N'Kekerasan lainnya',
    '',
    0,
    1,
    '2025-01-17 11:43:33.860',
    N'kews',
    '',
    0,
    0,
    '',
    0,
    0,
    ''
  );
INSERT INTO [AppStandardReferenceItem]
  (
    [StandardReferenceID],
    [ItemID],
    [ItemName],
    [Note],
    [IsUsedBySystem],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [ReferenceID],
    [coaID],
    [subledgerID],
    [CustomField],
    [LineNumber],
    [NumericValue],
    [CustomField2]
  )
VALUES
  (
    N'VisitReason',
    N'023',
    N'Non bedah lainnya',
    '',
    0,
    1,
    '2025-01-17 11:43:33.893',
    N'kews',
    '',
    0,
    0,
    '',
    0,
    0,
    ''
  );
