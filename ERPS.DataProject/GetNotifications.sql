CREATE PROCEDURE [dbo].[GetNotifications]
AS
Begin
	SELECT Id,Type,Date,Address,EveryYear From Events where cast (Date as Date) = cast (GETDATE() as Date) AND EveryYear = 0 OR month(Date) = month(GETDATE()) AND DAY(Date) = DAY(GETDATE()) AND EveryYear = 1;
	End
	GO
