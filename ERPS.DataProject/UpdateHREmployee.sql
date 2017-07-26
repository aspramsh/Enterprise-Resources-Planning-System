CREATE PROCEDURE [dbo].[UpdateHREmployee]
	@id int,
	@Name nvarchar(30),
	@SurName nvarchar(30),
	@Phone nvarchar(16),
	@Address varchar(50),
	@DateOfBirth datetime,
	@Passport nchar(15),
	@SocialCard nchar(15),
	@Description nvarchar(100),
	@DateOfHiring datetime
AS
BEGIN
	UPDATE HREmployee SET 
	Phone = @Phone, 
	Address = @Address, 
	DateOfBirth = @DateOfBirth, 
	Passport = @Passport, 
	SocialCard = @SocialCard, 
	Description = @Description, 
	DateOfHiring = @DateOfHiring
	WHERE Id = @id
	update dbo.Employee
	set Name = @Name, Surname = @SurName
	where Id = @id
END