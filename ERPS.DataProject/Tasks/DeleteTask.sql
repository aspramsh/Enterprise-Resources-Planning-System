CREATE PROCEDURE [dbo].[DeleteTask]
	@Id int
AS
	Begin
	Delete From Task
	where Id = @Id;
End;