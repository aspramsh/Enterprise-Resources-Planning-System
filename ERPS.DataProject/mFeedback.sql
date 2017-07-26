CREATE TABLE [dbo].[mFeedback]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [EmployeeId] INT NOT NULL, 
    [ReviewerId] INT NOT NULL, 
    [WorkedTogether] BIT NULL, 
    [WishToWorkTogether] BIT NULL, 
    [PossitiveSide] NVARCHAR(1000) NULL, 
    [NegativeSide] NVARCHAR(1000) NULL, 
    [ThingsToImprove] NVARCHAR(1000) NULL, 
    [Message] NVARCHAR(1000) NULL,

	CONSTRAINT FK_FeedbackEmployeeId FOREIGN KEY (EmployeeId) REFERENCES Employee(Id),
	CONSTRAINT FK_FeedbackReviewerId FOREIGN KEY (ReviewerId) REFERENCES Employee(Id)
)
