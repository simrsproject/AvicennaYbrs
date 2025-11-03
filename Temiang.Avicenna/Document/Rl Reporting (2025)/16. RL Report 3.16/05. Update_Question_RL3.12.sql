/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 03/02/2025 14:33:31
 ************************************************************/


UPDATE q
SET    q.QuestionText = 'Jenis Pelayanan Keluarga Berencana',
       q.QuestionAnswerSelectionID = 'RL3.16.JPKB'
FROM   Question AS q
WHERE  q.QuestionID = 'RL3.12.01'

UPDATE q
SET    q.QuestionText = 'Pelayanan KB',
       q.QuestionAnswerSelectionID = 'RL3.16.PKB'
FROM   Question AS q
WHERE  q.QuestionID = 'RL3.12.02'

UPDATE q
SET    q.QuestionText = 'Komplikasi KB',
       q.SRAnswerType = 'CHK',
       q.QuestionAnswerSelectionID = ''
FROM   Question AS q
WHERE  q.QuestionID = 'RL3.12.03'

UPDATE q
SET    q.QuestionText = 'Kegagalan KB',
       q.SRAnswerType = 'CHK',
       q.QuestionAnswerSelectionID = ''
FROM   Question AS q
WHERE  q.QuestionID = 'RL3.12.04'

UPDATE q
SET    q.QuestionText = 'Efek Samping'
FROM   Question AS q
WHERE  q.QuestionID = 'RL3.12.05'

UPDATE q
SET    q.QuestionText = 'Drop Out'
FROM   Question AS q
WHERE  q.QuestionID = 'RL3.12.06'

UPDATE q
SET    q.IsActive = 0
FROM   Question AS q
WHERE  q.QuestionID = 'RL3.12.07'