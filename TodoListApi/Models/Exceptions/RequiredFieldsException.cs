namespace TodoListApi.Models.Exceptions;

public class RequiredFieldsException  : Exception
{
    public RequiredFieldsException(string message)
        : base(message)
    {
    }
}