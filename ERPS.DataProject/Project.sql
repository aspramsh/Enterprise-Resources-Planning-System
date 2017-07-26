﻿CREATE TABLE [dbo].[Project]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] VARCHAR(50) NULL, 
    [TypeId] INT NULL,


	CONSTRAINT FK_Type_Id FOREIGN KEY (TypeId) REFERENCES ProjectType(Id) ON DELETE CASCADE

)
