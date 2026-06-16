using MongoDB.Driver;
using NewProject.Interfaces;
using NewProject.Models;

namespace NewProject.Services;

public class ProjectService : IProjectService
{
    private readonly IMongoCollection<Project> _projects;

    public ProjectService(IMongoDatabase database)
    {
        _projects = database.GetCollection<Project>("projects");
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _projects.Find(_ => true).ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(string id)
    {
        return await _projects.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Project project)
    {
        ValidateProject(project);
        await _projects.InsertOneAsync(project);
    }

    private static void ValidateProject(Project project)
    {
        ArgumentNullException.ThrowIfNull(project);

        if (string.IsNullOrWhiteSpace(project.Name))
            throw new ArgumentException("Project name is required.", nameof(project.Name));

        if (project.BudgetAmount <= 0)
            throw new ArgumentException(
                "Budget amount must be a positive number.",
                nameof(project.BudgetAmount)
            );

        if (project.EndDate <= project.StartDate)
            throw new ArgumentException(
                "End date must be after the start date.",
                nameof(project.EndDate)
            );

        if (!Enum.IsDefined(typeof(ProjectStatus), project.Status))
            throw new ArgumentException(
                "Project status must be Active, Completed, or OnHold.",
                nameof(project.Status)
            );
    }
}
