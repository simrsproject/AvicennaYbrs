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
    
    UPDATE VitalSignEws SET ChartMaxValue = 120 WHERE VitalSignID='BP2'
    UPDATE VitalSignEws SET ChartMinValue = 60 WHERE VitalSignID='BP1'
	UPDATE VitalSignEws SET ChartMinValue = 105 WHERE VitalSignID='SPO2'
    