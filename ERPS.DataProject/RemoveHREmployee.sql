CREATE PROCEDURE [dbo].[RemoveHREmployee]
	@Id int
AS
BEGIN
	UPDATE HREmployee SET EmploymentState = 0 
	WHERE Id = @Id
END