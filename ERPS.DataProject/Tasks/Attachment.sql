CREATE TABLE [dbo].[Attachment]
(
	[TaskId] INT NOT NULL, 
    CONSTRAINT [FK_Attachment_ToTask] FOREIGN KEY ([TaskId]) REFERENCES [Task]([Id]) ON Delete Cascade
)
