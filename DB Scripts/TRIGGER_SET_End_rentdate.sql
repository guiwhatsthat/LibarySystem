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
-- Description:	Trigger for End_rentdate
-- =============================================
CREATE TRIGGER Trigger_set_endrentdate
   ON  [Librarysystem].[dbo].[Rent] 
   FOR INSERT
AS DECLARE @PKeyInsertedRent INT,
		@LenddateInsertedRent DATETIME;

SELECT @PKeyInsertedRent = ins.PKey FROM INSERTED ins;
SELECT @LenddateInsertedRent = ins.Lend_date FROM INSERTED ins;

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	DECLARE @DATE DATETIME = GETDATE();
	DECLARE @DATEPLUS30DAYS DATETIME = DATEADD(DAY,30,@DATE);
	
	UPDATE [Librarysystem].[dbo].[Rent]
	SET End_rentdate = @DATEPLUS30DAYS
	WHERE PKey = @PKeyInsertedRent;

END
GO
