CREATE TABLE [dbo].[EmployeeProject]
(
	CONSTRAINT FK_Employee_Id_ FOREIGN KEY (EmployeeId) REFERENCES Employee(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Project_Id_ FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE, 
    [EmployeeId] INT NULL, 
    [ProjectId] INT NULL
	
)
