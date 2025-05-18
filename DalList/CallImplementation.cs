namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Implementation of the ICall interface for managing call entities.
/// Provides methods for CRUD operations on calls.
/// </summary>
internal class CallImplementation : ICall
{
    /// <summary>
    /// Creates a new call entity.
    /// </summary>
    /// <param name="item">The call entity to be created.</param>
    public void Create(Call item)
    {
        int id = Config.NextCallId;
        Call copy = item with { CallId = id };
        DataSourse.Calls!.Add(copy);
    }

    /// <summary>
    /// Deletes a call entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the call to be deleted.</param>
    /// <exception cref="DalDoesNotExistException">Thrown if the call with the specified ID does not exist.</exception>
    public void Delete(int id)
    {

        if (DataSourse.Calls == null)
           throw new DalDoesNotExistException("Call", "The list of calls is empty or does not exist.");
            Call? find = DataSourse.Calls!.Find(obj => obj.CallId == id);
        if (find is null)
            throw new DalDoesNotExistException("Call", $"An object from type Call with id: {id} does not exist.");
        DataSourse.Calls.Remove(find);
    }

    /// <summary>
    /// Deletes all call entities.
    /// </summary>
    public void DeleteAll()
    {
        if (DataSourse.Calls == null)
            throw new DalDoesNotExistException("Call", "The list of calls is empty or does not exist.");
        DataSourse.Calls!.Clear();
    }

    /// <summary>
    /// Reads a call entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the call to be read.</param>
    /// <returns>The call entity if found, otherwise null.</returns>
    public Call? Read(int id)
    {
        if (DataSourse.Calls == null)
            throw new DalDoesNotExistException("Call", "The list of calls is empty or does not exist.");
        return DataSourse.Calls!.FirstOrDefault(obj => obj.CallId == id);
    }

    /// <summary>
    /// Reads a call entity based on a filter function.
    /// </summary>
    /// <param name="filter">The filter function to apply to the calls.</param>
    /// <returns>The first call that matches the filter, or null if none found.</returns>
    public Call? Read(Func<Call, bool> filter)
    {
        if (DataSourse.Calls == null)
            throw new DalDoesNotExistException("Call", "The list of calls is empty or does not exist.");
        return DataSourse.Calls!.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all call entities, optionally filtered by a condition.
    /// </summary>
    /// <param name="filter">The optional filter function to apply.</param>
    /// <returns>An enumerable collection of calls.</returns>
    public IEnumerable<Call> ReadAll(Func<Call, bool>? filter = null)
        => (filter == null) ? DataSourse.Calls!.Select(item => item) :
        DataSourse.Calls!.Where(filter);

    /// <summary>
    /// Updates an existing call entity.
    /// </summary>
    /// <param name="item">The call entity to be updated.</param>
    /// <exception cref="DalDoesNotExistException">Thrown if the call with the specified ID does not exist.</exception>
    public void Update(Call item)
    {
        Call? find = DataSourse.Calls!.Find(obj => obj.CallId == item.CallId);
        if (find is null)
            throw new DalDoesNotExistException("Call", $"An object from type Call with id: {item.CallId} does not exist.");
        DataSourse.Calls.Remove(find);
        DataSourse.Calls.Add(item);
    }
}
