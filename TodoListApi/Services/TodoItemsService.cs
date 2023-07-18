using Microsoft.EntityFrameworkCore;
using TodoListApi.Models.Data;
using TodoListApi.Models.Entities;
using TodoListApi.Models.Exceptions;
using TodoListApi.Models.Inputs;

namespace TodoListApi.Services;

public class TodoItemsService : ITodoItemsService
{
    private readonly TodoListContext _context;

    public TodoItemsService(TodoListContext context)
    {
        _context = context;
    }

    public IEnumerable<TodoItem> GetTodoItems()
    {
        return _context.TodoItems.ToList();
    }

    public TodoItem GetTodoItem(int id)
    {
        return _context.TodoItems.Find(id) ?? throw new TodoItemNotFoundException();
    }

    public TodoItem CreateTodoItem(TodoItemCreateRequest todoItem)
    {
        ValidateTodoItemTitle(todoItem.Title);
        var newToDoItem = new TodoItem
        {
            Title = todoItem.Title,
            IsComplete = todoItem.IsComplete
        };
        _context.TodoItems.Add(newToDoItem);
        _context.SaveChanges();

        return newToDoItem;
    }

    public void UpdateTodoItem(int id, TodoItemUpdateRequest todoItemUpdated)
    {
        ValidateTodoItemTitle(todoItemUpdated.Title);
        if (id != todoItemUpdated.Id)
        {
            throw new FieldsDoNotMatchException();
        }
        var todoItemToUpdate = GetTodoItem(id);
        todoItemToUpdate.Title = todoItemUpdated.Title;
        todoItemToUpdate.IsComplete = todoItemUpdated.IsComplete;
        _context.Entry(todoItemToUpdate).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteTodoItem(int id)
    {
        var todoItem = GetTodoItem(id);

        _context.TodoItems.Remove(todoItem);
        _context.SaveChanges();
    }

    private static void ValidateTodoItemTitle(string todoItemTitle)
    {
        if (string.IsNullOrEmpty(todoItemTitle))
        {
            throw new RequiredFieldsException("Title is required");
        }
    }
}