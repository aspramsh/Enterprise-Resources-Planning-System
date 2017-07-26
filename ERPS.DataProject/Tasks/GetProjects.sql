CREATE PROCEDURE [dbo].[GetProjects]
AS
BEGIN
	SELECT Project.Id, Project.Name, ProjectType.Name, ProjectType.Id FROM Project left join ProjectType on
	TypeId = ProjectType.Id;
END
