CREATE TABLE [dbo].[Task]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [PlannedStart] DATETIME2 NULL, 
    [PlannedEnd] DATETIME2 NULL, 
    [ActualStart] DATETIME2 NULL, 
    [ActualEnd] DATETIME2 NULL, 
    [Description] VARCHAR(50) NULL,
    [StateId] INT NULL,



	[Source] VARCHAR(50) NULL, 
    [Revision] INT NULL, 
    [SeverityId] INT NULL, 
    [ReporterId] INT NULL, 
    [AssigneeId] INT NULL, 
    [ProjectId] INT NULL, 
    [Comments] VARCHAR(50) NULL, 
    CONSTRAINT FK_State_Id FOREIGN KEY (StateId) REFERENCES TaskState(Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_Task_ToSeverity] FOREIGN KEY ([SeverityId]) REFERENCES Severity(Id)


	
)
