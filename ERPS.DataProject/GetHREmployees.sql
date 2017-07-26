CREATE PROCEDURE [dbo].[GetHREmployees]
AS
Begin
	SELECT Employee.id, Name,Surname,Phone,Address,DateOfBirth,Passport,SocialCard,Description,DateOfHiring
	From Employee left join HREmployee on Employee.Id=HREmployee.Id where EmploymentState=1
End