CREATE PROCEDURE [dbo].[AddTask]
	@Id int,
	@PlannedStart datetime,
	@PlannedEnd datetime,
	@ActualStart datetime,
	@ActualEnd datetime,
	@Description varchar(50),
	@Source varchar(50),
	@Revision int,
	@ReporterId int,
	@ProjectId int, 
	@AssigneeId int,
	@StateId int,
	@SeverityId int,
	@Comments varchar(50)
AS
BEGIN
	If (exists(select * FROM Task WHERE Id = @Id))
	Update Task SET PlannedStart = @PlannedStart, PlannedEnd = @PlannedEnd, ActualStart = @ActualStart,
	ActualEnd = @ActualEnd, Description = @Description, Source = @Source,
	Revision = @Revision, ReporterId = @ReporterId, ProjectId = @ProjectId, AssigneeId = @AssigneeId,
	StateId = @StateId, SeverityId = @SeverityId, Comments = @Comments
	Where Id = @Id
	else
	Insert Into Task(PlannedStart, PlannedEnd, ActualStart, ActualEnd, 
	Description, Source, Revision, ReporterId, ProjectId, AssigneeId, StateId, SeverityId, Comments)
	VALUES(@PlannedStart, @PlannedEnd, @ActualStart, @ActualEnd, @Description, @Source, @Revision, @ReporterId, 
	@ProjectId, @AssigneeId, @StateId, @SeverityId, @Comments);
End;
