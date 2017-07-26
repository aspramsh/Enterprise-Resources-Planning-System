CREATE PROCEDURE [dbo].[GetBirthdays]
AS
Begin
	SELECT Employee.Id,Name,SurName,DateOfBirth From Employee left join HREmployee on Employee.Id=HREmployee.Id where month(DateOfBirth) = month(GETDATE()) AND DAY(DateOfBirth) = DAY(GETDATE()) 
	End
	GO