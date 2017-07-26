CREATE TABLE [dbo].[Task]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [PlannedStart] DATETIME2 NULL, 
    [PlannedEnd] DATETIME2 NULL, 
    [ActualStart] DATETIME2 NULL, 
    [ActualEnd] DATETIME2 NULL, 
    [Descriprtion] VARCHAR(50) NULL,
    [StateId] INT NULL,



	[Source] VARCHAR(50) NULL, 
    [Revision] INT NULL, 
    CONSTRAINT FK_State_Id FOREIGN KEY (StateId) REFERENCES TaskState(Id) ON DELETE CASCADE


	
)
