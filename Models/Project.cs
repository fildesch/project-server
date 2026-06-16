using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NewProject.Models;

public class Project
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ClientName { get; set; } = string.Empty;

    public decimal BudgetAmount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string ProjectManagerName { get; set; } = string.Empty;

    public ProjectStatus Status { get; set; } = ProjectStatus.Active;
}

public enum ProjectStatus
{
    Active,
    Completed,
    OnHold,
}
