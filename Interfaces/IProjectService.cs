using NewProject.Models;

namespace NewProject.Interfaces;

public interface IProjectService
{
    Task<List<Project>> GetAllAsync();

    Task<Project?> GetByIdAsync(string id);

    Task CreateAsync(Project project);
}
