namespace BuildingBlocks.Exceptions;

public class InternalServerException : Exception
{
    public string? Details { get; }

    public InternalServerException(string message, string? details = null) : base(message)
    {
        Details = details;
    }

    public InternalServerException(string message) : base(message)
    {
        
    }
}
