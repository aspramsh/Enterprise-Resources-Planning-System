CREATE PROCEDURE [dbo].[umsp_GetManagementEmployees]

AS
	select Id, Name, Surname, Team, Role, Project, Task
	from Employee left join mEmployee 
	on Employee.Id = mEmployee.EmployeeId
RETURN 0
