SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CompliancePatientFallRiskPrevention] (
		[TransactionNo]          [varchar](20) NOT NULL,
		[TransactionDate]        [datetime] NOT NULL,
		[ObserverID]             [int] NOT NULL,
		[EmployeeID]             [int] NOT NULL,
		[DepartmentID]           [int] NULL,
		[DivisionID]             [int] NULL,
		[SubDivisionID]          [int] NULL,
		[ServiceUnitID]          [varchar](10) NULL,
		[IsApproved]             [bit] NULL,
		[ApprovedDateTime]       [datetime] NULL,
		[ApprovedByUserID]       [varchar](15) NULL,
		[IsVoid]                 [bit] NULL,
		[VoidDateTime]           [datetime] NULL,
		[VoidByUserID]           [varchar](15) NULL,
		[CreatedDateTime]        [datetime] NULL,
		[CreatedByUserID]        [varchar](15) NULL,
		[LastUpdateDateTime]     [datetime] NULL,
		[LastUpdateByUserID]     [varchar](15) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompliancePatientFallRiskPrevention]
	ADD
	CONSTRAINT [PK_CompliancePatientFallRiskPrevention]
	PRIMARY KEY
	CLUSTERED
	([TransactionNo])
	ON [PRIMARY]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CompliancePatientFallRiskPreventionDetail] (
		[TransactionNo]                  [varchar](20) NOT NULL,
		[RegistrationNo]                 [varchar](20) NOT NULL,
		[SRFallRiskStatus]               [varchar](10) NULL,
		[SRFallRiskPreventionEffort]     [varchar](10) NULL,
		[CreatedDateTime]                [datetime] NULL,
		[CreatedByUserID]                [varchar](15) NULL,
		[LastUpdateDateTime]             [datetime] NULL,
		[LastUpdateByUserID]             [varchar](15) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompliancePatientFallRiskPreventionDetail]
	ADD
	CONSTRAINT [PK_CompliancePatientFallRiskPreventionDetail]
	PRIMARY KEY
	CLUSTERED
	([TransactionNo], [RegistrationNo])
	ON [PRIMARY]
GO
