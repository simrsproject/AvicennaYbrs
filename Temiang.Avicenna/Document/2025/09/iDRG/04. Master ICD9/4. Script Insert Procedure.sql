/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 07/10/2025 16:35:06
 ************************************************************/


INSERT INTO [Procedure]
SELECT a.CODE_FORMATED      ProcedureID,
       a.[DESCRIPTION]      ProcedureName,
       GETDATE()            LastUpdateDateTime,
       'sci'                LastUpdateByUserID,
       a.IM,
       a.VALIDCODE          ValidCode,
       a.ASTERISK        Asterisk,
       1                    Accpdx
FROM   _tmp_ICD9_MASTER  AS a
WHERE  NOT EXISTS (
           SELECT *
           FROM   [Procedure] AS d
           WHERE  CAST(d.ProcedureID as VARCHAR) = CAST(a.CODE_FORMATED AS VARCHAR)
       )
          