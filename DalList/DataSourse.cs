using DO;
namespace Dal;
/// <summary>
/// Contains in-memory data for the system, including lists for volunteers, calls, and assignments.
/// Throws exceptions if the lists are empty or do not exist.
/// </summary>
internal static class DataSourse
{
    private static List<DO.Volunteer>? volunteers { get; } = new();
    /// <summary>
    /// Gets the list of volunteers.
    /// </summary>
    /// <exception cref="DalDoesNotExistException">Thrown if the list of volunteers is empty or does not exist.</exception>
    internal static List<DO.Volunteer>? Volunteers
    {
        get
        {
            return volunteers;
        }
    }
    private static List<DO.Assignment>? assignments = new();
    /// <summary>
    /// Gets the list of assignments.
    /// </summary>
    /// <exception cref="DalDoesNotExistException">Thrown if the list of assignments is empty or does not exist.</exception>
    internal static List<DO.Assignment>? Assignments
    {
        get
        {
            return assignments;
        }
    }
    private static List<DO.Call>? calls = new();
    /// <summary>
    /// Gets the list of calls.
    /// </summary>
    /// <exception cref="DalDoesNotExistException">Thrown if the list of calls is empty or does not exist.</exception>
    internal static List<DO.Call>? Calls
    {
        get
        {
            return calls;
        }
    }
}