ALTER TABLE dbo.ServiceUnitBookingOperatingNotes ADD
	ComplicationsNotes VARCHAR(MAX) NULL
GO

ALTER TABLE dbo.ServiceUnitBooking ADD
	AmountOfBleeding NUMERIC(10,2) NULL,
	AmountOfTransfusions NUMERIC(10,2) NULL
GO

