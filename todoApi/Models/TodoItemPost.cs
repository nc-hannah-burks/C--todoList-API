using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  //sets further requirements/rules for inputs 


namespace TodoApi.Models;

public class TodoItemPost
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public bool IsComplete { get; set; }

    public string? Details { get; set; }
    public required int UserId { get; set; } // Required foreign key property


}