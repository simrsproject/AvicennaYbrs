ALTER TABLE TransChargesItem ADD CollectDate DATETIME NULL
ALTER TABLE TransChargesItem ADD ReceiveDate DATETIME NULL
ALTER TABLE TransChargesItem ADD CollectByUserID VARCHAR(40) NULL
ALTER TABLE TransChargesItem ADD ReceiveByUserID VARCHAR(40) NULL
ALTER TABLE TransChargesItem ADD IsCollectByUser BIT NULL
ALTER TABLE TransChargesItem ADD IsReceiveByUser BIT NULL
ALTER TABLE TransChargesItem ADD SRCollectMethod VARCHAR(20) NULL
