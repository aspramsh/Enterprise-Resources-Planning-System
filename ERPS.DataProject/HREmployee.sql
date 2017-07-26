CREATE TABLE [dbo].[HREmployee]
(
    [Id] INT NOT NULL IDENTITY ,
    [Phone] NVARCHAR(16) NULL, 
    [Address] VARCHAR(50) NULL, 
    [DateOfBirth] DATETIME NULL, 
    [Passport] NCHAR(15) NULL, 
    [SocialCard] NCHAR(15) NULL, 
    [Description] NVARCHAR(100) NULL, 
    [DateOfHiring] DATETIME NULL, 
    [EmploymentState] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_HREmployee] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_HREmployee_ToTable] FOREIGN KEY (id) REFERENCES Employee(id) 
  
  
)
