CREATE PROCEDURE [dbo].[AddHREmployee]
	@Name nvarchar(30),
	@SurName nvarchar(30),
	@Phone nvarchar(16)=Null,
	@Address varchar(50)=Null,
	@DateOfBirth datetime,
	@Passport nchar(15),
	@SocialCard nchar(15)=Null,
	@Description nvarchar(100)=Null,
	@DateOfHiring datetime
AS
BEGIN
	insert into Employee(Name,Surname) Values(@Name,@SurName)
    INSERT INTO HREmployee ( Phone, Address, DateOfBirth, Passport, SocialCard, Description, DateOfHiring)
	VALUES ( @Phone, @Address, @DateOfBirth, @Passport, @SocialCard, @Description, @DateOfHiring)


	
END
