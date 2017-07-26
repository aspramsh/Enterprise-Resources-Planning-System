﻿CREATE TABLE [dbo].[Events]
(
	[Id] INT NOT NULL IDENTITY , 
    [Type] NVARCHAR(20) NOT NULL, 
	[Address] VARCHAR(50) NULL, 
    [Date] DATETIME NULL,
	[EveryYear] BIT NOT NULL DEFAULT 0,
	PRIMARY KEY ([Id])
	) 
