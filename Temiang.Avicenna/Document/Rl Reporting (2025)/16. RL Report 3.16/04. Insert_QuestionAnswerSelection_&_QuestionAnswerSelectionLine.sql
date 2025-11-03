/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 31/01/2025 16:11:32
 ************************************************************/

-- QuestionAnswerSelection

INSERT INTO [QuestionAnswerSelection]
  (
    [QuestionAnswerSelectionID],
    [QuestionAnswerSelectionText],
    [LastUpdateDateTime],
    [LastUpdateByUserID]
  )
VALUES
  (
    N'RL3.16.JPKB',
    N'RL3.16 - Jenis Pelayanan Keluarga Berencana',
    '2025-01-30 16:42:57.930',
    N'sci'
  );
INSERT INTO [QuestionAnswerSelection]
  (
    [QuestionAnswerSelectionID],
    [QuestionAnswerSelectionText],
    [LastUpdateDateTime],
    [LastUpdateByUserID]
  )
VALUES
  (
    N'RL3.16.PKB',
    N'RL3.16 - Pelayanan KB',
    '2025-01-30 16:34:53.090',
    N'sci'
  );


--QuestionAnswerSelectionLine Jenis Pelayanan KB


INSERT INTO [QuestionAnswerSelectionLine] (
    [QuestionAnswerSelectionID],
    [QuestionAnswerSelectionLineID],
    [QuestionAnswerSelectionLineText],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [Score]
)
SELECT  
    N'RL3.16.JPKB',
    rmriv.RlMasterReportItemID,
    rmriv.RlMasterReportItemName,
    GETDATE(),
    N'sci',
    NULL
FROM RlMasterReportItemV2025 AS rmriv
WHERE rmriv.RlMasterReportID = 16
ORDER BY rmriv.RlMasterReportItemNo ASC

--QuestionAnswerSelectionLine Pelayanan KB

INSERT INTO [QuestionAnswerSelectionLine]
  (
    [QuestionAnswerSelectionID],
    [QuestionAnswerSelectionLineID],
    [QuestionAnswerSelectionLineText],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [Score]
  )
VALUES
  (
    N'RL3.16.PKB',
    N'01',
    N'Pasca Persalinan',
    '2025-01-30 16:34:53.090',
    N'sci',
    NULL
  );
INSERT INTO [QuestionAnswerSelectionLine]
  (
    [QuestionAnswerSelectionID],
    [QuestionAnswerSelectionLineID],
    [QuestionAnswerSelectionLineText],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [Score]
  )
VALUES
  (
    N'RL3.16.PKB',
    N'02',
    N'Pasca Keguguran',
    '2025-01-30 16:34:53.090',
    N'sci',
    NULL
  );
INSERT INTO [QuestionAnswerSelectionLine]
  (
    [QuestionAnswerSelectionID],
    [QuestionAnswerSelectionLineID],
    [QuestionAnswerSelectionLineText],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [Score]
  )
VALUES
  (
    N'RL3.16.PKB',
    N'03',
    N'Interval',
    '2025-01-30 16:34:53.090',
    N'sci',
    NULL
  );
