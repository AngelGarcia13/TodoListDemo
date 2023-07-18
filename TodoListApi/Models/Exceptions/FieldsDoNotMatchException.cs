namespace TodoListApi.Models.Exceptions;

public class FieldsDoNotMatchException  : Exception
{
    private const string FieldsDoNotMatchExceptionMessage = "Request parameter 'Id' doesn't match the Body field 'Id'";
    public FieldsDoNotMatchException()
        : base(FieldsDoNotMatchExceptionMessage)
    {
    }
}