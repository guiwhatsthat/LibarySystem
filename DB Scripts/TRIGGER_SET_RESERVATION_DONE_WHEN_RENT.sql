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
-- Description:	Trigger for setting done in table Reservation if entry in table Rent is inserted
-- =============================================
CREATE TRIGGER Trigger_reservation_done
   ON  [Librarysystem].[dbo].[Rent]
   AFTER INSERT
AS DECLARE 
	@FKeyBookInserted INT,
	@FKeyUserInserted INT,
	@PKeyReservation INT;

SELECT @FKeyBookInserted = ins.FKey_Book FROM INSERTED ins;
SELECT @FKeyUserInserted = ins.FKey_User FROM INSERTED ins;

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	SELECT @PKeyReservation = Reservation.PKey FROM Reservation 
	WHERE Reservation.FKey_Book = @FKeyBookInserted AND Reservation.FKey_User = @FKeyUserInserted AND Reservation.Done = 0;

	IF @PKeyReservation IS NOT NULL
		UPDATE [Librarysystem].[dbo].[Reservation]
		SET Reservation.Done = 1
		WHERE Reservation.PKey = @PKeyReservation;
END
GO
