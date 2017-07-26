CREATE PROCEDURE [dbo].[DeleteEmployeeFinance]
	@Id int
AS
BEGIN
	Delete From EmployeeFinance
	where Id = @Id;
End;
