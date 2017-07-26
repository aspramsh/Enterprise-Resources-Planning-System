CREATE TABLE [dbo].[mEmployee]
(
	[EmployeeId] INT NOT NULL PRIMARY KEY,
    [Team] NVARCHAR(50) NULL, 
    [Role] NVARCHAR(50) NULL, 
    [Project] NVARCHAR(50) NULL, 
    [Task] NVARCHAR(50) NULL,

	CONSTRAINT FK_EmployeeId_Cascade FOREIGN KEY (EmployeeId) REFERENCES Employee(Id)  ON DELETE CASCADE, 
)
