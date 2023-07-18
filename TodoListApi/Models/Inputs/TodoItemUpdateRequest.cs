using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models.Inputs;

public record TodoItemUpdateRequest(int Id, string? Title, bool IsComplete);