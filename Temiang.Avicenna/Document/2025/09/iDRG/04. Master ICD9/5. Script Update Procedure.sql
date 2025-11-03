/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 07/10/2025 16:35:37
 ************************************************************/

	
UPDATE d
SET    d.IM = a.IM,
       d.ValidCode = a.VALIDCODE,
       d.Asterisk = a.ASTERISK,
       d.Accpdx = (CASE WHEN a.ACCPDX = 'Y' THEN 1 ELSE 0 END)
FROM   [Procedure] AS d
       INNER JOIN _tmp_ICD9_MASTER AS a
            ON  CAST(d.ProcedureID as VARCHAR) = CAST(a.CODE_FORMATED AS VARCHAR)
