CREATE PROCEDURE [dbo].[GetTasks]
AS
Begin
if not exists(Select * from TaskState)
EXEC PopulateTables;
	SELECT Task.Id, PlannedStart, PlannedEnd, ActualStart, ActualEnd, Task.Description, 
	Source, Revision, Comments, a.Name as ReporterName, b.Name as AssigneeName, c.Name as ProjectName, d.Name as State, e.Severity as TaskSeverity, ReporterId, AssigneeId, ProjectId, StateId, SeverityId From 
	((((Task left join Employee as a on ReporterId = a.Id) left join
	Employee as b on AssigneeId = b.Id) left join Project as c on c.Id = ProjectId) left join TaskState as d on d.Id = StateId) left join dbo.Severity as e on e.Id = SeverityId;
End
