namespace server.Infrastructure.Utils.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class UniqueAttribute : Attribute
{
    public string ErrorMessage { get; }

    public UniqueAttribute(string errorMessage = null!)
    {
        ErrorMessage = errorMessage;
    }
}
