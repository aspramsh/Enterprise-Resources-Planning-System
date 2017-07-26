CREATE PROCEDURE [umsp_UpdateEmployee]
	@employeeId int, 
	--@skillId int
	--@name nvarchar(20), 
	--@surname nvarchar(30),
	@team nvarchar(50) = NULL,
	@role nvarchar(50) = NULL,
	@project nvarchar(50) = NULL,
	@task nvarchar(50) = NULL
AS
BEGIN 
	-- Updating skill of an employee
	--exec umsp_UpdateEmployeeSkill @employeeId, @skillId;
	-- Updating role of an employee e.t.c.
	-- Calling other SPs
	
	--old
	--update dbo.mEmployee
	--set Team = @team, Role = @role, Project = @project, Task = @task
	--where EmployeeId = @employeeId


	begin tran
	if exists (select * from dbo.mEmployee where EmployeeId = @employeeId)
	begin
		update dbo.mEmployee
		set Team = @team, Role = @role, Project = @project, Task = @task
		where EmployeeId = @employeeId
	end
	else
	begin
		insert into dbo.mEmployee(EmployeeId, Team, Role, Project, Task)
		values (@employeeId, @team, @role, @project, @task)
	end
	commit tran
	--update dbo.Employee
	--set Name = @name, Surname = @surname
	--where Id = @employeeId
END
