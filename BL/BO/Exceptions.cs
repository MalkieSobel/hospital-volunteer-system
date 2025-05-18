namespace BO;

/// <summary>
/// Exception thrown when an entity does not exist in the business logic layer.
/// </summary>
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
        : base(message, innerException) { }
}
/// <summary>
/// Exception thrown when an entity already exists in the business logic layer.
/// </summary>
[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException) { }
}
/// <summary>
/// Exception thrown when deletion of an entity is not possible in the business logic layer.
/// </summary>
[Serializable]
public class BlDeletionImpossibleException : Exception
{
    public BlDeletionImpossibleException(string? message) : base(message) { }
    public BlDeletionImpossibleException(string message, Exception innerException)
        : base(message, innerException) { }
}
/// <summary>
/// Exception thrown when a property has a null value.
/// </summary>
[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}
/// <summary>
/// Exception thrown when there is an internal issue in the business logic layer.
/// </summary>
[Serializable]
public class BlInternalException : Exception
{
    public BlInternalException(string? message, Exception? innerException = null)
        : base(message, innerException) { }
}


[Serializable]
public class UpdateFailedException : Exception
{
    public UpdateFailedException(string? message, Exception? innerException = null)
        : base(message, innerException) { }
}


