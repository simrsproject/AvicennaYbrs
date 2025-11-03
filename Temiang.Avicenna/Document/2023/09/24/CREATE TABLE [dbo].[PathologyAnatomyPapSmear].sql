
/****** Object:  Table [dbo].[PathologyAnatomyPapSmear]    Script Date: 9/22/2023 11:58:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PathologyAnatomyPapSmear](
	[ResultNo] [varchar](20) NOT NULL,
	[MaturationIndex] [varchar](max) NULL,
	[IsCytoplasmVacuolization] [bit] NULL,
	[IsCytoplasmic] [bit] NULL,
	[IsMultinucleation] [bit] NULL,
	[IsKoilocyte] [bit] NULL,
	[IsClueCell] [bit] NULL,
	[IsMoldedCore] [bit] NULL,
	[IsCoreGroundGlass] [bit] NULL,
	[IsAscUs] [bit] NULL,
	[IsAscH] [bit] NULL,
	[IsLight] [bit] NULL,
	[IsCurrently] [bit] NULL,
	[IsHeavy] [bit] NULL,
	[IsMalignantCells] [bit] NULL,
	[IsTrichomonasvaginalis] [bit] NULL,
	[IsCandidaspp] [bit] NULL,
	[IsActinomycesspp] [bit] NULL,
	[EndocervicalCells] [varchar](max) NULL,
	[IsEndometrialCells] [bit] NULL,
	[IsSquamousMetaplasia] [bit] NULL,
	[IsAtypicalCells] [bit] NULL,
	[IsAdenocarcinomaInSitu] [bit] NULL,
	[IsAdenocarcinoma] [bit] NULL,
	[IsNeutrophils] [bit] NULL,
	[IsLymphocytes] [bit] NULL,
	[IsHistiocytes] [bit] NULL,
	[IsOtherInflammatory] [bit] NULL,
	[IsErythrocytes] [bit] NULL,
	[IsSpermatozoa] [bit] NULL,
	[IsOtherFindings] [bit] NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedByUserID] [varchar](15) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL
	
 CONSTRAINT [PK_PathologyAnatomyPapSmear] PRIMARY KEY CLUSTERED 
(
	[ResultNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


