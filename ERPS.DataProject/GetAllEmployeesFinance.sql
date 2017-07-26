CREATE PROCEDURE [dbo].[GetSalaries]
AS
BEGIN
	SELECT Id, Name, Surname, Amount, ChangeDate FROM Employee LEFT JOIN Salary
	on Id = Salary.EmployeeId order by ChangeDate desc;
END
