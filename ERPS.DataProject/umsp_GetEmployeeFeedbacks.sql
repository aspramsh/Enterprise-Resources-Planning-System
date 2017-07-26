CREATE PROCEDURE [dbo].[umsp_GetEmployeeFeedbacks]
	@employeeId int
AS
	SELECT Id, EmployeeId, ReviewerId, WorkedTogether, WishToWorkTogether,
		   PossitiveSide, NegativeSide, ThingsToImprove, Message
	FROM DBO.mFeedback
	WHERE EmployeeId = @employeeId
RETURN 0
