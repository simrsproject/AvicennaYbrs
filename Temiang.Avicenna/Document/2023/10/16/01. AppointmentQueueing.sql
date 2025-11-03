ALTER TABLE AppointmentQueueing ADD ParamedicID VARCHAR(10) NULL
GO

UPDATE AppointmentQueueing SET ParamedicID = ''
GO