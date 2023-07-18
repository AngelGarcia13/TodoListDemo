using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models.Entities;

public class TodoItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public bool IsComplete { get; set; }
}