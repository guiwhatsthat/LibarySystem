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
-- Description:	Trigger for setting Avaiable in table Book when Return_date is set in table Rent
-- =============================================
CREATE TRIGGER Trigger_set_avaiable
   ON  [Librarysystem].[dbo].[Rent]
   FOR UPDATE
AS DECLARE @ReturndateUpdated DATETIME,
	@FKey_BookUpdated INT,
	@BookAvaiable BIT;

SELECT @ReturndateUpdated = ins.Return_date FROM inserted ins
INNER JOIN deleted del ON ins.PKey = del.PKey;
SELECT @FKey_BookUpdated = ins.FKey_Book FROM inserted ins
INNER JOIN deleted del ON ins.PKey = del.PKey;

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here

	-- When something is in cell Return_date
	IF @ReturndateUpdated IS NOT NULL 
		-- Check if book is already avaiable or not
		SELECT @BookAvaiable = Book.Avaiable FROM Book WHERE Book.PKey = @FKey_BookUpdated;
		IF @BookAvaiable = 0
			UPDATE [Librarysystem].[dbo].[Book]
			SET Avaiable = 1
			WHERE PKey = @FKey_BookUpdated;

END
GO
