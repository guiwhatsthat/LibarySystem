USE [Librarysystem]
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 01.07.2020 15:42:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Last_name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[ZIP] [int] NOT NULL,
	[City] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

