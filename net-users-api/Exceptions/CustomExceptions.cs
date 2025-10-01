namespace NetUsersApi.Exceptions;

/// <summary>
/// Exception thrown when a requested resource is not found
/// </summary>
public class ResourceNotFoundException : Exception
{
    public string ResourceType { get; }
    public string ResourceId { get; }

    public ResourceNotFoundException(string resourceType, string resourceId)
        : base($"{resourceType} with ID '{resourceId}' was not found")
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }

    public ResourceNotFoundException(string resourceType, string resourceId, Exception innerException)
        : base($"{resourceType} with ID '{resourceId}' was not found", innerException)
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }
}

/// <summary>
/// Exception thrown when validation fails
/// </summary>
public class ValidationException : Exception
{
    public IDictionary<string, string[]> ValidationErrors { get; }

    public ValidationException(string message) : base(message)
    {
        ValidationErrors = new Dictionary<string, string[]>();
    }

    public ValidationException(string message, IDictionary<string, string[]> validationErrors) : base(message)
    {
        ValidationErrors = validationErrors;
    }
}

/// <summary>
/// Exception thrown when a business rule is violated
/// </summary>
public class BusinessRuleException : Exception
{
    public string RuleCode { get; }

    public BusinessRuleException(string ruleCode, string message) : base(message)
    {
        RuleCode = ruleCode;
    }

    public BusinessRuleException(string ruleCode, string message, Exception innerException) : base(message, innerException)
    {
        RuleCode = ruleCode;
    }
}