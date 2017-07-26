CREATE TABLE [dbo].[mEmployeeSkills]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [EmployeeId] INT NOT NULL, 
    [SkillId] INT NOT NULL, 
    [Percentage] INT NULL

	
	CONSTRAINT FK_Employee_Skill_Cascade FOREIGN KEY (SkillId) REFERENCES mSkill(Id)  ON DELETE CASCADE
	CONSTRAINT FK_Employee_Id_Cascade FOREIGN KEY (EmployeeId) REFERENCES Employee(Id) ON DELETE CASCADE
	check ([Percentage] >= 0 and [Percentage] <= 100)
)
