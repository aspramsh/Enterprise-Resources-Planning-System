CREATE PROCEDURE [dbo].[AddAttachment]
	@TaskId int
AS
Begin
	INSERT INTO [dbo].[Attachment]
           (TaskId)
     VALUES
           (@TaskId);

		   End
