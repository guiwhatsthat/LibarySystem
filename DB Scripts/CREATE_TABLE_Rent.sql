USE [Librarysystem]
GO

/****** Object:  Table [dbo].[Rent]    Script Date: 26.08.2020 17:07:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rent](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[Lend_date] [datetime] NOT NULL,
	[Return_date] [datetime] NULL,
	[End_rentdate] [datetime] NULL,
	[FKey_Book] [int] NOT NULL,
	[FKey_User] [int] NOT NULL,
 CONSTRAINT [PK_Rent] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Rent]  WITH CHECK ADD  CONSTRAINT [FK_Rent_Book] FOREIGN KEY([FKey_Book])
REFERENCES [dbo].[Book] ([PKey])
GO

ALTER TABLE [dbo].[Rent] CHECK CONSTRAINT [FK_Rent_Book]
GO

ALTER TABLE [dbo].[Rent]  WITH CHECK ADD  CONSTRAINT [FK_Rent_User] FOREIGN KEY([FKey_User])
REFERENCES [dbo].[User] ([PKey])
GO

ALTER TABLE [dbo].[Rent] CHECK CONSTRAINT [FK_Rent_User]
GO

