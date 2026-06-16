using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NewProject.Models;

public class Expense
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public ExpenseCategory Category { get; set; }

    public string ProjectId { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; } = string.Empty;
}

public enum ExpenseCategory
{
    Labour,
    Materials,
    Travel,
    Software,
    Other,
}
