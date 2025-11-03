-- Hanya untuk RS GPI yg meminta Tekanan darah Sistolok dan Diastolik nya dipisah ---

IF (
       EXISTS(
           SELECT *
           FROM   Healthcare AS h
           WHERE  h.HealthcareID = 'RSGPI'
       )
   )
BEGIN
    UPDATE VitalSignEws
    SET    VitalSignID = 'BP1'
    WHERE  VitalSignID = 'BP'
    
    INSERT INTO VitalSignEws
      (
        VitalSignID
       ,StartAgeInDay
       ,IndexNo
       ,ChartMinValue
       ,ChartMaxValue
       ,LastUpdateDateTime
       ,LastUpdateByUserID
       ,IsExcludeFromScoreEws
      )
    SELECT 'BP2'
          ,StartAgeInDay
          ,IndexNo
          ,ChartMinValue
          ,ChartMaxValue
          ,LastUpdateDateTime
          ,LastUpdateByUserID
          ,IsExcludeFromScoreEws
    FROM   VitalSignEws AS vse
    WHERE  vse.VitalSignID = 'BP1'
END