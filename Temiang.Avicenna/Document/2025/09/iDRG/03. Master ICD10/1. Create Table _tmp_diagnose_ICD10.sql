
/****** Object:  Table [dbo].[_tmp_ICD10_MASTER]    Script Date: 07/10/2025 16:29:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[_tmp_ICD10_MASTER](
	[CODE_FORMATED] [nvarchar](max) NULL,
	[CODE] [nvarchar](max) NULL,
	[CODE2] [nvarchar](max) NULL,
	[DESCRIPTION] [nvarchar](max) NULL,
	[SYSTEM] [nvarchar](max) NULL,
	[VALIDCODE] [nvarchar](max) NULL,
	[ACCPDX] [nvarchar](max) NULL,
	[ASTERISK] [nvarchar](max) NULL,
	[IM] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


