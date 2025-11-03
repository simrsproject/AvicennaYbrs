IF EXISTS(SELECT 0 FROM GuarantorAutoBillItem AS gabi)
BEGIN
	INSERT INTO GuarantorAutoBillItem
	(
		GuarantorID,
		ServiceUnitID,
		ItemID,
		Quantity,
		IsGenerateOnRegistration,
		IsGenerateOnNewRegistration,
		IsGenerateOnReferral,
		IsGenerateOnFirstRegistration,
		IsActive,
		LastUpdateDateTime,
		LastUpdateByUserID
	)
	SELECT gabi.GuarantorID,
		x.ServiceUnitID,
		gabi.ItemID,
		gabi.Quantity,
		gabi.IsGenerateOnRegistration,
		gabi.IsGenerateOnNewRegistration,
		gabi.IsGenerateOnReferral,
		gabi.IsGenerateOnFirstRegistration,
		gabi.IsActive,
		gabi.LastUpdateDateTime,
		gabi.LastUpdateByUserID 
	FROM GuarantorAutoBillItem AS gabi 
	CROSS JOIN (SELECT * FROM ServiceUnit AS su WHERE su.SRRegistrationType = 'IPR' AND su.IsActive = 1) x
	WHERE gabi.ServiceUnitID = 'IPR'
	
	INSERT INTO GuarantorAutoBillItem
	(
		GuarantorID,
		ServiceUnitID,
		ItemID,
		Quantity,
		IsGenerateOnRegistration,
		IsGenerateOnNewRegistration,
		IsGenerateOnReferral,
		IsGenerateOnFirstRegistration,
		IsActive,
		LastUpdateDateTime,
		LastUpdateByUserID
	)
	SELECT gabi.GuarantorID,
		x.ServiceUnitID,
		gabi.ItemID,
		gabi.Quantity,
		gabi.IsGenerateOnRegistration,
		gabi.IsGenerateOnNewRegistration,
		gabi.IsGenerateOnReferral,
		gabi.IsGenerateOnFirstRegistration,
		gabi.IsActive,
		gabi.LastUpdateDateTime,
		gabi.LastUpdateByUserID 
	FROM GuarantorAutoBillItem AS gabi 
	CROSS JOIN (SELECT * FROM ServiceUnit AS su WHERE su.SRRegistrationType = 'EMR' AND su.IsActive = 1) x
	WHERE gabi.ServiceUnitID = 'EMR'
	
	INSERT INTO GuarantorAutoBillItem
	(
		GuarantorID,
		ServiceUnitID,
		ItemID,
		Quantity,
		IsGenerateOnRegistration,
		IsGenerateOnNewRegistration,
		IsGenerateOnReferral,
		IsGenerateOnFirstRegistration,
		IsActive,
		LastUpdateDateTime,
		LastUpdateByUserID
	)
	SELECT gabi.GuarantorID,
		x.ServiceUnitID,
		gabi.ItemID,
		gabi.Quantity,
		gabi.IsGenerateOnRegistration,
		gabi.IsGenerateOnNewRegistration,
		gabi.IsGenerateOnReferral,
		gabi.IsGenerateOnFirstRegistration,
		gabi.IsActive,
		gabi.LastUpdateDateTime,
		gabi.LastUpdateByUserID 
	FROM GuarantorAutoBillItem AS gabi 
	CROSS JOIN (SELECT * FROM ServiceUnit AS su WHERE su.SRRegistrationType = 'OPR' AND su.IsActive = 1) x
	WHERE gabi.ServiceUnitID = 'OPR'

	INSERT INTO GuarantorAutoBillItem
	(
		GuarantorID,
		ServiceUnitID,
		ItemID,
		Quantity,
		IsGenerateOnRegistration,
		IsGenerateOnNewRegistration,
		IsGenerateOnReferral,
		IsGenerateOnFirstRegistration,
		IsActive,
		LastUpdateDateTime,
		LastUpdateByUserID
	)
	SELECT gabi.GuarantorID,
		x.ServiceUnitID,
		gabi.ItemID,
		gabi.Quantity,
		gabi.IsGenerateOnRegistration,
		gabi.IsGenerateOnNewRegistration,
		gabi.IsGenerateOnReferral,
		gabi.IsGenerateOnFirstRegistration,
		gabi.IsActive,
		gabi.LastUpdateDateTime,
		gabi.LastUpdateByUserID 
	FROM GuarantorAutoBillItem AS gabi 
	CROSS JOIN (SELECT * FROM ServiceUnit AS su WHERE su.SRRegistrationType = 'MCU' AND su.IsActive = 1) x
	WHERE gabi.ServiceUnitID = 'MCU'
	
	DELETE GuarantorAutoBillItem WHERE ServiceUnitID IN ('IPR', 'OPR', 'EMR', 'MCU')
END