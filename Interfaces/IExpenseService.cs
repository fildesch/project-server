using NewProject.Models;

namespace NewProject.Interfaces;

public interface IExpenseService
{
    Task CreateAsync(Expense expense);

    Task<List<Expense>> GetByProjectIdAsync(string projectId);
}
