CREATE PROCEDURE [dbo].[UpdateEvent]
	@Id int,
	@Type nvarchar(20),
	@Address nvarchar(50),
	@Date datetime
AS
BEGIN
Update Events SET Type = @Type, Address = @Address, Date = @Date WHERE Id = @Id

End
