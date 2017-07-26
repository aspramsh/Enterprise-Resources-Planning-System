CREATE PROCEDURE [dbo].[PopulateTables]

AS
Begin
	INSERT INTO [dbo].[TaskState]
           ([Name]
           ,[Description])
     VALUES
           ('Open', 'A new task that has just been created.'),
		   ('In Progress', 'The employee is fully in the task.'),
		   ('Resolved', 'So everyone is happy.'),
		   ('Closed', 'Forget about it.');
		   INSERT INTO [dbo].[ProjectType]
           ([Name]
           ,[Description])
     VALUES
           ('UI', 'Specified UI for each and every project'),
		   ('DB', 'Cool DB for the project'),
		   ('MiddleLayer', 'Application''s full logic')
		   End;
		   INSERT INTO [dbo].[Severity]
           ([Severity])
     VALUES
           ('Task'),
		   ('Bug');
		   INSERT INTO [dbo].[Project]
           ([Name]
           ,[TypeId])
     VALUES
           ('Cool Chat', 1),
		   ('Airline Control System', 2),
		   ('Hotel Review System', 3),
		   ('Ministry of Finance Website', 1)

GO;
