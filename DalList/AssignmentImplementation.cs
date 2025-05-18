namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
/// <summary>
/// Implementation of the IAssignment interface for managing assignment entities.
/// Provides methods for CRUD operations on assignments.
/// </summary>
internal class AssignmentImplementation : IAssignment
{
    /// <summary>
    /// Creates a new assignment entity.
    /// </summary>
    /// <param name="item">The assignment entity to be created.</param>
    public void Create(Assignment item)
    {
        int id = Config.NextAssignmentId;
        Assignment copy = item with { AssignmentId = id };
        DataSourse.Assignments!.Add(copy);
    }

    /// <summary>
    /// Deletes an assignment entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the assignment to be deleted.</param>
    /// <exception cref="DalDoesNotExistException">Thrown if the assignment with the specified ID does not exist.</exception>
    public void Delete(int id)
    {
        if (DataSourse.Assignments == null)
           throw new DalDoesNotExistException("Assignment", "The list of assignments is empty or does not exist.");
        
        Assignment? find = DataSourse.Assignments!.Find(obj => obj.CallId == id);
        if (find is null)
            throw new DalDoesNotExistException("Assignment", $"An object from type Assignment with id: {id} does not exist.");
        DataSourse.Assignments.Remove(find);
    }

    /// <summary>
    /// Deletes all assignment entities.
    /// </summary>
    public void DeleteAll()
    {
        if (DataSourse.Assignments == null)
            throw new DalDoesNotExistException("Assignment", "The list of assignments is empty or does not exist.");
        DataSourse.Assignments!.Clear();
    }

    /// <summary>
    /// Reads an assignment entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the assignment to be read.</param>
    /// <returns>The assignment entity if found, otherwise null.</returns>
    public Assignment? Read(int id)
    {
        if (DataSourse.Assignments == null)
            throw new DalDoesNotExistException("Assignment", "The list of assignments is empty or does not exist.");
        return DataSourse.Assignments!.FirstOrDefault(obj => obj.AssignmentId == id);
    }

    /// <summary>
    /// Reads an assignment entity based on a filter function.
    /// </summary>
    /// <param name="filter">The filter function to apply to the assignments.</param>
    /// <returns>The first assignment that matches the filter, or null if none found.</returns>
    public Assignment? Read(Func<Assignment, bool> filter)
    {
        if (DataSourse.Assignments == null)
            throw new DalDoesNotExistException("Assignment", "The list of assignments is empty or does not exist.");
        return DataSourse.Assignments!.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all assignment entities, optionally filtered by a condition.
    /// </summary>
    /// <param name="filter">The optional filter function to apply.</param>
    /// <returns>An enumerable collection of assignments.</returns>
    public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null)
        => (filter == null) ? DataSourse.Assignments!.Select(item => item) :
        DataSourse.Assignments!.Where(filter);

    /// <summary>
    /// Updates an existing assignment entity.
    /// </summary>
    /// <param name="item">The assignment entity to be updated.</param>
    /// <exception cref="DalDoesNotExistException">Thrown if the assignment with the specified ID does not exist.</exception>
    public void Update(Assignment item)
    {
        Assignment? find = DataSourse.Assignments!.Find(obj => obj.AssignmentId == item.AssignmentId);
        if (find is null)
            throw new DalDoesNotExistException("Assignment", $"An object from type Assignment with id: {item.CallId} does not exist.");
        DataSourse.Assignments.Remove(find);
        DataSourse.Assignments.Add(item);
    }
}
