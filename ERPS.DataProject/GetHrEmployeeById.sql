CREATE PROCEDURE [dbo].[GetHrEmployeeById]
		@HRID int
AS
Begin
SELECT Employee.Id,Name,SurName,Phone,Address,DateOfBirth,Passport,SocialCard,Description,DateOfHiring From Employee  join HREmployee on Employee.id=HREmployee.Id Where EmploymentState=1 AND Employee.id=@HRID
		End
		Go

