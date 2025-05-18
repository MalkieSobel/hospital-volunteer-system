using System.Xml.Linq;
namespace DO;
/// <summary>
/// Exception thrown when an entity does not exist in the data source.
/// </summary>
[Serializable]
public class DalDoesNotExistException : Exception
{
    public string EntityName { get; set; }
    public DalDoesNotExistException(string name, string? message) : base(message)
    {
        EntityName = name;
    }
}
/// <summary>
/// Exception thrown when an entity already exists in the data source.
/// </summary>
[Serializable]
public class DalAlreadyExistsException : Exception
{
    public string EntityName { get; set; }
    public DalAlreadyExistsException(string name, string? message) : base(message)
    {
        EntityName = name;
    }
}
/// <summary>
/// Exception thrown when deletion of an entity is not possible.
/// </summary>
[Serializable]
public class DalDeletionImpossible : Exception
{
    public string EntityName { get; set; }
    public DalDeletionImpossible(string name, string? message) : base(message)
    {
        EntityName = name;
    }
}
/// <summary>
/// Exception thrown when deletion of an entity is not possible.
/// </summary>
[Serializable]
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string message) : base(message) { }
}