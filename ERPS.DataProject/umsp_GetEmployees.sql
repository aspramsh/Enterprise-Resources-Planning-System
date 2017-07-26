CREATE PROCEDURE [dbo].[umsp_GetEmployees]
AS
	SELECT Id, Name, Surname 
	FROM [dbo].[Employee]
RETURN 0
