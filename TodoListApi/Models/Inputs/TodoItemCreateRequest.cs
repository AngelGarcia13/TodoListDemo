using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models.Inputs;

public record TodoItemCreateRequest(string? Title, bool IsComplete);