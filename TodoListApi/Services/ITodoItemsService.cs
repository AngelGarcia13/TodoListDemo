using TodoListApi.Models.Entities;
using TodoListApi.Models.Inputs;

namespace TodoListApi.Services;
public interface ITodoItemsService
    {
        IEnumerable<TodoItem> GetTodoItems();
        TodoItem GetTodoItem(int id);
        TodoItem CreateTodoItem(TodoItemCreateRequest todoItem);
        void UpdateTodoItem(int id, TodoItemUpdateRequest todoItem);
        void DeleteTodoItem(int id);
    }