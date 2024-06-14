using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }



    public DbSet<TodoItem> TodoItems { get; set; } = null!;


    public DbSet<TodoCompleteCount> TodoCompleteCount { get; set; } = null!;


    public DbSet<TodoUser> TodoUsers { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>()
            .HasOne(t => t.TodoUser)
            .WithMany(u => u.TodoItems)
            .HasForeignKey(t => t.UserId);
    }

}




//notes on this file: 
/*The TodoContext class is an Entity Framework (EF) Core DbContext that serves as the primary class for interacting with the database in your application. It manages the connection to the database and provides methods for querying and saving data. Here's a detailed breakdown of what the TodoContext class does:*/