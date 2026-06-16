using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewProject.Dtos;
using NewProject.Interfaces;
using NewProject.Models;

namespace NewProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IExpenseService _expenseService;

    public ProjectsController(IProjectService projectService, IExpenseService expenseService)
    {
        _projectService = projectService;
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Project>>> GetAll()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(projects);
    }

    [HttpPost]
    public async Task<ActionResult<Project>> Create(Project project)
    {
        try
        {
            await _projectService.CreateAsync(project);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var project = await _projectService.GetByIdAsync(id);

        if (project == null)
            return NotFound();

        var expenses = await _expenseService.GetByProjectIdAsync(id);

        var totalSpent = expenses.Sum(e => e.Amount);
        var remainingBudget = project.BudgetAmount - totalSpent;

        var dto = new ProjectDetailsDto
        {
            Project = project,
            Expenses = expenses,
            TotalSpent = totalSpent,
            RemainingBudget = remainingBudget,
        };

        return Ok(dto);
    }
}
