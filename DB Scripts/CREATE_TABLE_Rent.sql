USE [Librarysystem]
GO

/****** Object:  Table [dbo].[Rent]    Script Date: 29.06.2020 22:30:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rent](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[Lend_date] [datetime] NOT NULL,
	[Return_date] [datetime] NULL,
	[End_rentdate] [datetime] NOT NULL,
	[FKey_Book] [int] NOT NULL,
	[FKey_Customer] [int] NOT NULL,
	[FKey_Employee] [int] NOT NULL,
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

ALTER TABLE [dbo].[Rent]  WITH CHECK ADD  CONSTRAINT [FK_Rent_Customer] FOREIGN KEY([FKey_Customer])
REFERENCES [dbo].[Customer] ([PKey])
GO

ALTER TABLE [dbo].[Rent] CHECK CONSTRAINT [FK_Rent_Customer]
GO

ALTER TABLE [dbo].[Rent]  WITH CHECK ADD  CONSTRAINT [FK_Rent_Employee] FOREIGN KEY([FKey_Employee])
REFERENCES [dbo].[Employee] ([PKey])
GO

ALTER TABLE [dbo].[Rent] CHECK CONSTRAINT [FK_Rent_Employee]
GO

