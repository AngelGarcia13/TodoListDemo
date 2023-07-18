namespace TodoListApi.Models.Exceptions;

public class TodoItemNotFoundException : Exception
{
    private const string TodoItemNotFoundExceptionMessage = "The requested Todo Item was not found";
    public TodoItemNotFoundException()
        : base(TodoItemNotFoundExceptionMessage)
    {
    }
}