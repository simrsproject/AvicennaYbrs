     
UPDATE d
SET    d.IM = a.IM,
       d.ValidCode = a.VALIDCODE,
       d.Asterisk = a.ASTERISK,
       d.Accpdx = (CASE WHEN a.ACCPDX = 'Y' THEN 1 ELSE 0 END)
FROM   Diagnose AS d
       INNER JOIN _tmp_ICD10_MASTER AS a
            ON  CAST(d.DiagnoseID as VARCHAR) = CAST(a.CODE_FORMATED AS VARCHAR)