using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;   //sets further requirements/rules for inputs 
namespace TodoApi.Models;



public class TodoItemDTO
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    // Foreign key for TodoUser - THIS needs to be fixed to allow me to give the relevant userId in post
    [ForeignKey("TodoUser")]
    public int UserId { get; set; }

    // Navigation property
    public TodoUser? TodoUser { get; set; }

}