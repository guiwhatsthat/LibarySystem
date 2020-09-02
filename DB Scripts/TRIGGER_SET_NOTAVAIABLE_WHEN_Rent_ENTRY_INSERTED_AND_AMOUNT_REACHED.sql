-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fabrice Reiser / Steven Gasser
-- Create date: 26.08.2020
-- Description:	Trigger for setting Avaiable false in table Book when rent entry is inserted and book amount is reached by rents
-- =============================================
CREATE TRIGGER Trigger_set_notavaiable
   ON  [Librarysystem].[dbo].[Rent]
   AFTER INSERT
AS DECLARE 
	@FKeyBookInserted INT, 
	@COUNTRentedBooks INT,
	@AmountBooks INT;

SELECT @FKeyBookInserted = ins.FKey_Book FROM INSERTED ins;

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	-- Get amount of the inserted book in rent
	SELECT @COUNTRentedBooks = 
		COUNT(rnt.PKey) FROM [Librarysystem].[dbo].[Rent] rnt 
		WHERE rnt.FKey_Book = @FKeyBookInserted
		AND rnt.Return_date IS NULL;

	-- Get the amount of the book
	SELECT @AmountBooks = Amount FROM [Librarysystem].[dbo].[Book] WHERE PKey = @FKeyBookInserted;
	
	-- Check if all books are in rent
	IF @COUNTRentedBooks = @AmountBooks
		-- If yes, update table Book Avaiable to 0 (false)
		UPDATE [Librarysystem].[dbo].[Book]
		SET Avaiable = 0
		WHERE PKey = @FKeyBookInserted;
END
GO
