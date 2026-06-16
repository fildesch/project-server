using System.Collections.Generic;
using NewProject.Models;

namespace NewProject.Dtos;

public class ProjectDetailsDto
{
    public Project Project { get; set; } = default!;

    public List<Expense> Expenses { get; set; } = new List<Expense>();

    public decimal TotalSpent { get; set; }

    public decimal RemainingBudget { get; set; }
}
