CREATE PROCEDURE [dbo].[umsp_GetFeedbacks]

AS
	SELECT Id, EmployeeId, ReviewerId, WorkedTogether, 
	WishToWorkTogether, PossitiveSide, NegativeSide, ThingsToImprove, Message
	from dbo.mFeedback 

RETURN 0
