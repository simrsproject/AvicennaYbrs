
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
           'CauseOfDevelopDisorder' /* { StandardReferenceID } */,
           'Cause Of Development Disorder' /* { StandardReferenceName } */,
           '50' /* { ItemLength } */,
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
           'CauseOfDevelopDisorder' /* { StandardReferenceID } */,
           'CauseOfDevelopDisorder-001' /* { ItemID } */,
           'Psikis / Psikomatis' /* { ItemName } */,
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
	       'CauseOfDevelopDisorder' /* { StandardReferenceID } */,
	       'CauseOfDevelopDisorder-002' /* { ItemID } */,
	       'Penyakit Menular Seksual' /* { ItemName } */,
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
	       'CauseOfDevelopDisorder' /* { StandardReferenceID } */,
	       'CauseOfDevelopDisorder-003' /* { ItemID } */,
	       'Lain-lain' /* { ItemName } */,
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