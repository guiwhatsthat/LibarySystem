USE [Librarysystem]
GO

/****** Object:  Table [dbo].[Reservation]    Script Date: 26.08.2020 17:07:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Reservation](
	[PKey] [int] IDENTITY(1,1) NOT NULL,
	[Reservation_date] [datetime] NOT NULL,
	[Done] [bit] NOT NULL,
	[FKey_Book] [int] NOT NULL,
	[FKey_User] [int] NOT NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[PKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Reservation] ADD  CONSTRAINT [DF_Reservation_Done]  DEFAULT ((0)) FOR [Done]
GO

ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Book] FOREIGN KEY([FKey_Book])
REFERENCES [dbo].[Book] ([PKey])
GO

ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Book]
GO

ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User] FOREIGN KEY([FKey_User])
REFERENCES [dbo].[User] ([PKey])
GO

ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User]
GO

