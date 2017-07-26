CREATE PROCEDURE [dbo].[GetStates]
AS
Begin
	SELECT Id, Name From TaskState;
	End
