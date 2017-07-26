CREATE PROCEDURE [dbo].[umsp_DeleteEmployee]
	@Id int
AS
BEGIN
	Delete From Employee
	where Id = @Id;
End;
