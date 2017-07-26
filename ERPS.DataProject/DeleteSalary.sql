CREATE PROCEDURE [dbo].[DeleteSalary]
	@Id int
AS
BEGIN
	Delete From Salary
	where EmployeeId = @Id;
End;
