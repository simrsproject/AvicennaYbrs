
INSERT INTO AppStandardReference(
           StandardReferenceID,
           StandardReferenceName,
           ItemLength,
           IsUsedBySystem,
           IsActive,
           StandardReferenceGroup,
           Note,
           LastUpdateDateTime,
           LastUpdateByUserID,
           HasCOA,
           IsNumericValue
       )
       VALUES
       (
           'TypeOfService' /* { StandardReferenceID } */,
           'Type Of Service' /* { StandardReferenceName } */,
           '20' /* { ItemLength } */,
           1 /* { IsUsedBySystem } */,
           1 /* { IsActive } */,
           '' /* { StandardReferenceGroup } */,
           '' /* { Note } */,
           GETDATE() /* { LastUpdateDateTime } */,
           'sci' /* { LastUpdateByUserID } */,
           0 /* { HasCOA } */,
           NULL /* { IsNumericValue } */
       )


INSERT INTO AppStandardReferenceItem(
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
       (
           'TypeOfService' /* { StandardReferenceID } */,
           '01' /* { ItemID } */,
           'Pra Rawat Inap' /* { ItemName } */,
           '' /* { Note } */,
           1 /* { IsUsedBySystem } */,
           1 /* { IsActive } */,
           GETDATE() /* { LastUpdateDateTime } */,
           'sci' /* { LastUpdateByUserID } */,
           '' /* { ReferenceID } */,
           0 /* { coaID } */,
           0 /* { subledgerID } */,
           NULL /* { CustomField } */,
           0 /* { LineNumber } */,
           0 /* { NumericValue } */,
           NULL /* { CustomField2 } */
       ),
	   (
	       'TypeOfService' /* { StandardReferenceID } */,
	       '02' /* { ItemID } */,
	       'Pasca Rawat Inap' /* { ItemName } */,
	       '' /* { Note } */,
	       1 /* { IsUsedBySystem } */,
	       1 /* { IsActive } */,
	       GETDATE() /* { LastUpdateDateTime } */,
	       'sci' /* { LastUpdateByUserID } */,
	       '' /* { ReferenceID } */,
	       0 /* { coaID } */,
	       0 /* { subledgerID } */,
	       NULL /* { CustomField } */,
	       0 /* { LineNumber } */,
	       0 /* { NumericValue } */,
	       NULL /* { CustomField2 } */
	   ),
	   (
	       'TypeOfService' /* { StandardReferenceID } */,
	       '03' /* { ItemID } */,
	       'Pra Melahirkan' /* { ItemName } */,
	       '' /* { Note } */,
	       1 /* { IsUsedBySystem } */,
	       1 /* { IsActive } */,
	       GETDATE() /* { LastUpdateDateTime } */,
	       'sci' /* { LastUpdateByUserID } */,
	       '' /* { ReferenceID } */,
	       0 /* { coaID } */,
	       0 /* { subledgerID } */,
	       NULL /* { CustomField } */,
	       0 /* { LineNumber } */,
	       0 /* { NumericValue } */,
	       NULL /* { CustomField2 } */
	   ),
	   (
	       'TypeOfService' /* { StandardReferenceID } */,
	       '04' /* { ItemID } */,
	       'Pasca Melahirkan' /* { ItemName } */,
	       '' /* { Note } */,
	       1 /* { IsUsedBySystem } */,
	       1 /* { IsActive } */,
	       GETDATE() /* { LastUpdateDateTime } */,
	       'sci' /* { LastUpdateByUserID } */,
	       '' /* { ReferenceID } */,
	       0 /* { coaID } */,
	       0 /* { subledgerID } */,
	       NULL /* { CustomField } */,
	       0 /* { LineNumber } */,
	       0 /* { NumericValue } */,
	       NULL /* { CustomField2 } */
	   ),
	   (
	       'TypeOfService' /* { StandardReferenceID } */,
	       '05' /* { ItemID } */,
	       'Pengobatan Rutin' /* { ItemName } */,
	       '' /* { Note } */,
	       1 /* { IsUsedBySystem } */,
	       1 /* { IsActive } */,
	       GETDATE() /* { LastUpdateDateTime } */,
	       'sci' /* { LastUpdateByUserID } */,
	       '' /* { ReferenceID } */,
	       0 /* { coaID } */,
	       0 /* { subledgerID } */,
	       NULL /* { CustomField } */,
	       0 /* { LineNumber } */,
	       0 /* { NumericValue } */,
	       NULL /* { CustomField2 } */
	   )

