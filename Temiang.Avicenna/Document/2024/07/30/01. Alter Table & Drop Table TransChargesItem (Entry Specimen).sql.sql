ALTER TABLE TransChargesItem DROP COLUMN CollectDate 
ALTER TABLE TransChargesItem DROP COLUMN ReceiveDate 
ALTER TABLE TransChargesItem DROP COLUMN CollectByUserID 
ALTER TABLE TransChargesItem DROP COLUMN ReceiveByUserID 
ALTER TABLE TransChargesItem DROP COLUMN IsCollectByUser 
ALTER TABLE TransChargesItem DROP COLUMN IsReceiveByUser 
ALTER TABLE TransChargesItem DROP COLUMN SRCollectMethod 


ALTER TABLE TransChargesItem ADD SpecimenCollectDateTime DATETIME NULL
ALTER TABLE TransChargesItem ADD SpecimenReceiveDateTime DATETIME NULL
ALTER TABLE TransChargesItem ADD SpecimenCollectByUserID VARCHAR(15) NULL
ALTER TABLE TransChargesItem ADD SpecimenReceiveByUserID VARCHAR(15) NULL
ALTER TABLE TransChargesItem ADD SRCollectMethod VARCHAR(20) NULL