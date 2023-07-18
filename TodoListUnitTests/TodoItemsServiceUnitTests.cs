using Microsoft.EntityFrameworkCore;
using TodoListApi.Models.Data;
using TodoListApi.Models.Entities;
using TodoListApi.Models.Exceptions;
using TodoListApi.Models.Inputs;
using TodoListApi.Services;

namespace TodoListUnitTests;

public class TodoItemsServiceUnitTests
{
    private readonly ITodoItemsService _todoItemsService;
    private readonly TodoListContext _context;

    public TodoItemsServiceUnitTests()
    {
        // Set up the mock DbContext
        var options = new DbContextOptionsBuilder<TodoListContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _context = new TodoListContext(options);
        _todoItemsService = new TodoItemsService(_context);
    }

    [Fact]
    public void GetTodoItems_ReturnsAllTodoItems()
    {
        // Arrange
        var todoItems = new List<TodoItem>
            {
                new TodoItem {Title = "Task 1", IsComplete = false },
                new TodoItem {Title = "Task 2", IsComplete = true },
                new TodoItem {Title = "Task 3", IsComplete = false }
            };
        _context.TodoItems.AddRange(todoItems);
        _context.SaveChanges();

        // Act
        var result = _todoItemsService.GetTodoItems().ToList();

        // Assert
        Assert.Equal(todoItems.Count, result.Count);
    }

    [Fact]
    public void GetTodoItem_ExistingId_ReturnsTodoItem()
    {
        // Arrange
        var todoItem = new TodoItem { Title = "Task 1", IsComplete = false };
        _context.TodoItems.Add(todoItem);
        _context.SaveChanges();

        // Act
        var result = _todoItemsService.GetTodoItem(todoItem.Id);

        // Assert
        Assert.Equal(todoItem, result);
    }

    [Fact]
    public void GetTodoItem_NonExistingId_ThrowsTodoItemNotFoundException()
    {
        // Arrange

        // Act
        Action act = () => _todoItemsService.GetTodoItem(1);

        // Assert
        Assert.Throws<TodoItemNotFoundException>(act);
    }

    [Fact]
    public void CreateTodoItem_ValidTodoItem_ReturnsCreatedTodoItem()
    {
        // Arrange
        var todoItemCreateRequest = new TodoItemCreateRequest
        (
            Title: "Task 1",
            IsComplete: false
        );

        // Act
        var result = _todoItemsService.CreateTodoItem(todoItemCreateRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(todoItemCreateRequest.Title, result.Title);
        Assert.Equal(todoItemCreateRequest.IsComplete, result.IsComplete);
    }

    [Fact]
    public void CreateTodoItem_NullOrEmptyTitle_ThrowsRequiredFieldsException()
    {
        // Arrange
        var todoItemCreateRequest = new TodoItemCreateRequest
        (
            Title: null,
            IsComplete: false
        );

        // Act
        Action act = () => _todoItemsService.CreateTodoItem(todoItemCreateRequest);

        // Assert
        Assert.Throws<RequiredFieldsException>(act);
    }

    [Fact]
    public void UpdateTodoItem_ExistingId_ValidTodoItem_UpdatesTodoItem()
    {
        // Arrange
        var existingTodoItem = new TodoItem { Title = "Task 1", IsComplete = false };
        _context.TodoItems.Add(existingTodoItem);
        _context.SaveChanges();

        var updatedTodoItem = new TodoItemUpdateRequest
        (
            Id: existingTodoItem.Id,
            Title: "Updated Task 1",
            IsComplete: true
        );

        // Act
        _todoItemsService.UpdateTodoItem(updatedTodoItem.Id, updatedTodoItem);
        var result = _context.TodoItems.Find(updatedTodoItem.Id);

        // Assert
        Assert.Equal(updatedTodoItem.Title, result.Title);
        Assert.Equal(updatedTodoItem.IsComplete, result.IsComplete);
    }

    [Fact]
    public void UpdateTodoItem_NonExistingId_ThrowsTodoItemNotFoundException()
    {
        // Arrange
        var todoItemUpdateRequest = new TodoItemUpdateRequest
        (
            Id: 9999,
            Title: "Updated Task 1",
            IsComplete: true
        );

        // Act
        Action act = () => _todoItemsService.UpdateTodoItem(todoItemUpdateRequest.Id, todoItemUpdateRequest);

        // Assert
        Assert.Throws<TodoItemNotFoundException>(act);
    }

    [Fact]
    public void DeleteTodoItem_ExistingId_DeletesTodoItem()
    {
        // Arrange
        var todoItem = new TodoItem { Title = "Task 1", IsComplete = false };
        _context.TodoItems.Add(todoItem);
        _context.SaveChanges();

        // Act
        _todoItemsService.DeleteTodoItem(todoItem.Id);
        var result = _context.TodoItems.Find(todoItem.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteTodoItem_NonExistingId_ThrowsTodoItemNotFoundException()
    {
        // Arrange

        // Act
        Action act = () => _todoItemsService.DeleteTodoItem(9999);

        // Assert
        Assert.Throws<TodoItemNotFoundException>(act);
    }

}
