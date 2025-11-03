INSERT INTO Diagnose
SELECT a.CODE_FORMATED       DiagnoseID,
       NULL                  DtdNo,
       a.[DESCRIPTION]       DiagnoseName,
       0                     IsChronicDisease,
       0                     IsDisease,
       1                     IsActive,
       GETDATE()             LastUpdateDateTime,
       'sci'                 LastUpdateByUserID,
       '' 'Synonym',
       0                     IsSatuSehat,
       a.IM,
       a.VALIDCODE           ValidCode,
       a.ASTERISK         Asterisk,
       NULL                  Accpdx
FROM   _tmp_ICD10_MASTER  AS a
WHERE  NOT EXISTS (
           SELECT *
           FROM   Diagnose AS d
           WHERE  CAST(d.DiagnoseID as VARCHAR) = CAST(a.CODE_FORMATED AS VARCHAR)
       )