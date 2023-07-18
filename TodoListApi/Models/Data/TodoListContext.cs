using Microsoft.EntityFrameworkCore;
using TodoListApi.Models.Entities;

namespace TodoListApi.Models.Data;
public class TodoListContext : DbContext
{
    public TodoListContext(DbContextOptions<TodoListContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }
}
