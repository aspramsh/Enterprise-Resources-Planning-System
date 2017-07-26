CREATE PROCEDURE [dbo].[umsp_LeaveFeedback]
	@employeeId int,
	@reviewerId int,
	@workedTogether bit,
	@wishToWorkTogether bit,
	@possitiveSide nvarchar(1000) = NULL,
	@negativeSide nvarchar(1000) = NULL,
	@thingsToImprove nvarchar(1000) = NULL,
	@message nvarchar(1000) = NULL
AS
	INSERT INTO dbo.mFeedback (EmployeeId, ReviewerId, WorkedTogether, 
	WishToWorkTogether, PossitiveSide, NegativeSide, ThingsToImprove, Message)
	VALUES (@employeeId,	   
			@reviewerId, 	   
			@workedTogether,   
			@wishToWorkTogether, 
			@possitiveSide, 
			@negativeSide, 
			@thingsToImprove, 
			@message)
RETURN 0
