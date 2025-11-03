INSERT INTO Appparameter(
           ParameterID,
           ParameterName,
           ParameterValue,
           ParameterType,
           LastUpdateDateTime,
           LastUpdateByUserID,
           IsUsedBySystem,
           MESSAGE
       ) VALUES(
           N'IsiDRGIntegration',
           N'Is iDRG Integration',
           N'Yes',
           NULL,
           '2025-07-02T16:40:42.803',
           N'sci',
           0,
           NULL
       );


--==========================


ALTER TABLE EpisodeProcedure 
ADD QtyICD INT
