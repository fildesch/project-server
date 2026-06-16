using MongoDB.Driver;
using NewProject.Interfaces;
using NewProject.Models;

namespace NewProject.Services;

public class ExpenseService : IExpenseService
{
    private readonly IMongoCollection<Expense> _expenses;
    private readonly IProjectService _projectService;

    public ExpenseService(IMongoDatabase database, IProjectService projectService)
    {
        _expenses = database.GetCollection<Expense>("expenses");
        _projectService = projectService;
    }

    public async Task CreateAsync(Expense expense)
    {
        var project = await _projectService.GetByIdAsync(expense.ProjectId);
        if (project == null)
            throw new ArgumentException("Project not found.", nameof(expense.ProjectId));

        ValidateExpense(expense, project);

        await _expenses.InsertOneAsync(expense);
    }

    public async Task<List<Expense>> GetByProjectIdAsync(string projectId)
    {
        return await _expenses.Find(e => e.ProjectId == projectId).ToListAsync();
    }

    private static void ValidateExpense(Expense expense, Project project)
    {
        ArgumentNullException.ThrowIfNull(expense);

        if (string.IsNullOrWhiteSpace(expense.ProjectId))
            throw new ArgumentException("ProjectId is required.", nameof(expense.ProjectId));

        if (expense.Amount <= 0)
            throw new ArgumentException(
                "Expense amount must be a positive number.",
                nameof(expense.Amount)
            );

        if (!Enum.IsDefined(typeof(ExpenseCategory), expense.Category))
            throw new ArgumentException("Invalid expense category.", nameof(expense.Category));

        if (project.Status == ProjectStatus.Completed)
            throw new ArgumentException(
                "Cannot add expenses to a completed project.",
                nameof(expense.ProjectId)
            );

        var expenseUtc = expense.Date.ToUniversalTime();
        var nowUtc = DateTime.UtcNow;

        if (expenseUtc > nowUtc)
            throw new ArgumentException(
                "Expense date cannot be in the future.",
                nameof(expense.Date)
            );

        var projectStartUtc = project.StartDate.ToUniversalTime();
        if (expenseUtc < projectStartUtc)
            throw new ArgumentException(
                "Expense date cannot be before the project start date.",
                nameof(expense.Date)
            );
    }
}
