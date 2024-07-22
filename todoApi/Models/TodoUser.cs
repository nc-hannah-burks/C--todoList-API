using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;    //sets further requirements/rules for inputs 
namespace TodoApi.Models;

public class TodoUser
{
    [Key]
    public int UserId { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string UserName { get; set; }


    [Display(Name = "Email address")]
    [Required(ErrorMessage = "The email address is required")]
    [EmailAddress(ErrorMessage = "E-mail is not valid")]
    public required string UserEmail { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6)]
    public required string UserPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("UserPassword", ErrorMessage = "Passwords do not match.")]
    public required string ConfirmPassword { get; set; }
    // Navigation property
    [JsonIgnore]
    public ICollection<TodoItem> TodoItems { get; } = new List<TodoItem>();

}

