UPDATE q SET q.RelatedEntityName = 'BirthRecord' FROM Question AS q WHERE q.QuestionID IN ('AAPATRI29','AAPATRI30')
UPDATE q SET q.RelatedColumnName = 'Weight' FROM Question AS q WHERE q.QuestionID = 'AAPATRI29'
UPDATE q SET q.IsUpdateRelatedEntity = 1 FROM Question AS q WHERE q.QuestionID = 'AAPATRI29'
UPDATE q SET q.RelatedColumnName = 'Length' FROM Question AS q WHERE q.QuestionID = 'AAPATRI30'
UPDATE q SET q.IsUpdateRelatedEntity = 1 FROM Question AS q WHERE q.QuestionID = 'AAPATRI30'
