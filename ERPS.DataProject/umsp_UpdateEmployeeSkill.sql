CREATE PROCEDURE [dbo].[umsp_UpdateEmployeeSkill]
	@employeeId int, 
	@skillId int
AS
BEGIN 
	-- In future this SP will recieve Skill name, find its ID (if it exists) and add to DB a row
	Insert into [dbo].[mEmployeeSkills] (EmployeeId, SkillId)
	values (@employeeId, @skillId)
END
