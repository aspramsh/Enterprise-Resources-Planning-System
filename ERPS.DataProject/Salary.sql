CREATE TABLE [dbo].[Salary]
(
	[EmployeeId] INT NOT NULL, 
    [Amount] INT NULL, 
    [ChangeDate] DATETIME NULL, 
    CONSTRAINT [FK_Salary_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) 
)
