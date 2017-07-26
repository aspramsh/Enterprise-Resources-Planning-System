CREATE PROCEDURE [dbo].[GetEmployeeFinanceById]
	@Id int
AS
BEGIN
	SELECT Id, Name,  Surname, Amount From Employee left Join Salary
	on Id = EmployeeId
	Where Id = @Id;
End;
