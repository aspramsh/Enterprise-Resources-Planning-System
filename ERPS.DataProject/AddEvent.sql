CREATE PROCEDURE [dbo].[AddEvent]
	@Type nvarchar(20),
	@Address nvarchar(50),
	@Date datetime,
	@EveryYear bit
AS
BEGIN
	INSERT INTO Events (Type, Address, Date, EveryYear)
	VALUES (@Type, @Address, @Date, @EveryYear)
END

