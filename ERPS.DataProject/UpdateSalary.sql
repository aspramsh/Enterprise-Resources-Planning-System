CREATE PROCEDURE [dbo].[UpdateSalary]
	@Id int,
	@Name VARCHAR(15),
	@LastName VARCHAR(15),
	@Salary int
AS
BEGIN
	Insert Into Salary(EmployeeId, Amount, ChangeDate)
	VALUES(@Id, @Salary, GETDATE());
End;