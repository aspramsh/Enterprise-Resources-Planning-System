CREATE PROCEDURE [dbo].[GetEmployees]
	AS
Begin
	SELECT Id, Name, Surname From Employee;
	End

