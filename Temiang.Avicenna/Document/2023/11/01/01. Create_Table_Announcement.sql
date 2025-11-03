CREATE TABLE Announcement(
	AnnouncementID VARCHAR(3) NOT NULL,
	AnnouncementStartDate DATETIME NOT NULL,
	AnnouncementEndDate DATETIME NOT NULL,
	AnnouncementTitle NVARCHAR(50) NOT NULL,
	AnnouncementDesc NVARCHAR(MAX) NOT NULL,
	LastUpdateDateTime DATETIME NOT NULL,
	LastUpdateByUserID NVARCHAR(15) NOT NULL,
	IsActive BIT NOT NULL
	CONSTRAINT [PK_Announcement] PRIMARY KEY (AnnouncementID)
)
GO