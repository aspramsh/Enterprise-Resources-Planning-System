CREATE PROCEDURE [dbo].[AddTask]
	@PlannedStart datetime,
	@PlannedEnd datetime,
	@ActualStart datetime,
	@ActualEnd datetime,
	@Description varchar(50),
	@Source varchar(50),
	@Revision int
AS
BEGIN
	Insert Into Task(PlannedStart, PlannedEnd, ActualStart, ActualEnd, 
	Descriprtion, Source, Revision)
	VALUES(@PlannedStart, @PlannedEnd, @ActualStart, @ActualEnd, @Description, @Source, @Revision);
End;
