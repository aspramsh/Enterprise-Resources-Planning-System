CREATE PROCEDURE [dbo].[umsp_AddEmployee]
	@employeeName nvarchar(20),
	@employeeSurname nvarchar(30)
AS
BEGIN 
	-- In future this SP will recieve Skill name, find its ID (if it exists) and add to DB a row
	Insert into [dbo].[Employee] (Name, Surname)
	values (@employeeName, @employeeSurname)
END