CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL  IDENTITY, 
    [Name] NCHAR(20) NULL, 
    [Surname] NCHAR(30) NULL, 
    CONSTRAINT [PK_Employee] PRIMARY KEY ([Id])
)
