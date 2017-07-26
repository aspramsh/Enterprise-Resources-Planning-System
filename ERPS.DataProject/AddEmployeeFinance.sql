CREATE PROCEDURE [dbo].[AddEmployeeFinance]
	@Id int,
	@Name VARCHAR(15),
	@Salary int
AS
BEGIN
	Insert Into EmployeeFinance(Id, Name, Salary)
	VALUES(@Id, @Name, @Salary);
End;