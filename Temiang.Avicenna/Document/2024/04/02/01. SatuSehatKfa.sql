/****** Object:  Table [dbo].[SatuSehatKfa]    Script Date: 01/04/2024 16:33:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SatuSehatKfa](
	[id] [bigint] NOT NULL,
	[ss_uuid] [varchar](20) NOT NULL,
	[ss_type] [varchar](50) NOT NULL,
	[ss_nama] [varchar](1000) NOT NULL,
	[ss_result] [varchar](max) NOT NULL,
	[ss_kfa_total_data] [int] NOT NULL,
	[created_at] [varchar](50) NOT NULL,
	[updated_at] [varchar](50) NOT NULL,
	[deleted_at] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SatuSehatKfa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO