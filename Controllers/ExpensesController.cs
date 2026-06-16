using Microsoft.AspNetCore.Mvc;
using NewProject.Interfaces;
using NewProject.Models;

namespace NewProject.Controllers;

[ApiController]
[Route("api/projects/{projectId}/expenses")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpPost]
    public async Task<ActionResult<Expense>> Create(string projectId, Expense expense)
    {
        try
        {
            expense.ProjectId = projectId;
            await _expenseService.CreateAsync(expense);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }

        return Created(string.Empty, expense);
    }
}
