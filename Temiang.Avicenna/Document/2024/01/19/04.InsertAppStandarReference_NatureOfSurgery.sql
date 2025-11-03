
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
           'NatureOfSurgery' /* { StandardReferenceID } */,
           'Nature Of Surgery' /* { StandardReferenceName } */,
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
           'NatureOfSurgery' /* { StandardReferenceID } */,
           'NatureOfSurgery-001' /* { ItemID } */,
           'Terencana' /* { ItemName } */,
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
	       'NatureOfSurgery' /* { StandardReferenceID } */,
	       'NatureOfSurgery-002' /* { ItemID } */,
	       'Emergency' /* { ItemName } */,
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
	       'NatureOfSurgery' /* { StandardReferenceID } */,
	       'NatureOfSurgery-003' /* { ItemID } */,
	       'Berhubungan dengan kecelakaan kerja' /* { ItemName } */,
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