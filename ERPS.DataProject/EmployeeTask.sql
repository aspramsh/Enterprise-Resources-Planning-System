﻿CREATE TABLE [dbo].[EmployeeTask]
(
	[TaskId] INT NOT NULL 
	, 
    [EmployeeId] INT NOT NULL,
	
	CONSTRAINT FK_Task_Id FOREIGN KEY (TaskId) REFERENCES Task(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Employee_Id FOREIGN KEY (EmployeeId) REFERENCES Employee(Id) ON DELETE CASCADE

	
)
