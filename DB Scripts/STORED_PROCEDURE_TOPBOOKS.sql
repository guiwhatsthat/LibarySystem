-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fabrice Reiser / Steven Gasser
-- Create date: 31.08.2020
-- Description:	Stored procedure to get the top rented books
-- =============================================
CREATE PROCEDURE SP_TOPBOOKS
	-- Add the parameters for the stored procedure here	
	@TOPBOOKSAMOUNT int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP (@TOPBOOKSAMOUNT) bk.Name,COUNT(bk.Name) AS Amount_Rent FROM Librarysystem.dbo.Rent rnt
	JOIN [Librarysystem].[dbo].[User] usr ON rnt.FKey_User = usr.PKey
	JOIN [Librarysystem].[dbo].[Book] bk ON rnt.FKey_Book = bk.PKey
	GROUP BY bk.Name
	ORDER BY Amount_Rent DESC
END
GO
