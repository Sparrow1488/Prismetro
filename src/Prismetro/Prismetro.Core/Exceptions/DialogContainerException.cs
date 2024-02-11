namespace Prismetro.Core.Exceptions;

public class DialogContainerException : Exception
{
    public DialogContainerException()
    {
    }

    public DialogContainerException(string? message) : base(message)
    {
    }

    public DialogContainerException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}