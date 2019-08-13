USE [S_FOOD]
GO

/****** Object:  Table [dbo].[UNI_DelveryAddress]    Script Date: 13-08-2019 09:17:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UNI_DelveryAddress](
	[id] [bigint] NOT NULL,
	[clientId] [varchar](250) NULL,
	[firstName] [varchar](50) NULL,
	[lastName] [varchar](50) NULL,
	[country] [varchar](50) NULL,
	[fullAddress] [varchar](250) NULL,
	[city] [varchar](50) NULL,
	[state] [varchar](50) NULL,
	[pincode] [varchar](50) NULL,
	[emailAddress] [varchar](50) NULL,
	[phone] [varchar](10) NULL,
 CONSTRAINT [PK_UNI_DelveryAddress] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


