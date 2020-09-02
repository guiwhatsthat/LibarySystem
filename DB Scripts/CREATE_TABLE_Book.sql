USE [Librarysystem]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 26.08.2020 21:18:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Book](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[ISBN] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](150) NOT NULL,
	[Publisher] [nvarchar](150) NOT NULL,
	[Amount] [int] NOT NULL,
	[Avaiable] [bit] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_Amount]  DEFAULT ((1)) FOR [Amount]
GO

ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_Avaiable]  DEFAULT ((1)) FOR [Avaiable]
GO

