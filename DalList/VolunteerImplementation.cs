namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
/// <summary>
/// Implementation of the IVolunteer interface for managing volunteer entities.
/// Provides methods to create, read, update, delete, and manage volunteers.
/// </summary>
internal class VolunteerImplementation : IVolunteer
{

    /// <summary>
    /// Creates a new volunteer entity.
    /// </summary>
    /// <param name="item">The volunteer entity to be created.</param>
    /// <exception cref="DalAlreadyExistsException">Thrown if the volunteer already exists.</exception>
    public void Create(Volunteer item)
    {
        if (Read(item.VolunteerId) is not null)
            throw new DalAlreadyExistsException("Volunteer", $"Volunteer with ID {item.VolunteerId} already exists.");
        DataSourse.Volunteers!.Add(item);
    }

    /// <summary>
    /// Deletes a volunteer entity by its ID.
    /// </summary>
    /// <param name="id">The unique ID of the volunteer to be deleted.</param>
    /// <exception cref="DalDoesNotExistException">Thrown if the volunteer does not exist.</exception>
    public void Delete(int id)
    {
        if (DataSourse.Volunteers == null)
            throw new DalDoesNotExistException("Volunteer", "The list of volunteers is empty or does not exist.");
        Volunteer? find = DataSourse.Volunteers!.Find(obj => obj.VolunteerId == id);
        if (find is null)
            throw new DalDoesNotExistException("Volunteer", $"An object from type Volunteer with id: {id} does not exist.");
        DataSourse.Volunteers.Remove(find);
    }

    /// <summary>
    /// Deletes all volunteer entities.
    /// </summary>
    public void DeleteAll()
    {
        if (DataSourse.Volunteers == null)
            throw new DalDoesNotExistException("Volunteer", "The list of volunteers is empty or does not exist.");
        DataSourse.Volunteers!.Clear();
    }

   

    /// <summary>
    /// Retrieves a volunteer entity by its ID.
    /// </summary>
    /// <param name="id">The unique ID of the volunteer.</param>
    /// <returns>The volunteer entity if found.</returns>
    /// <exception cref="DalDoesNotExistException">Thrown if the volunteer list is empty or if no volunteer with the given ID exists.</exception>
    public Volunteer Read(int id)
    {
        if (DataSourse.Volunteers == null || !DataSourse.Volunteers.Any())
            return null;

        Volunteer? volunteer = DataSourse.Volunteers.FirstOrDefault(obj => obj.VolunteerId == id);

        if (volunteer == null)
            return null; ;

        return volunteer;
    }


    /// <summary>
    /// Reads a volunteer entity based on a filter function.
    /// </summary>
    /// <param name="filter">The filter function to apply to the volunteer entities.</param>
    /// <returns>The first volunteer that matches the filter, or null if none found.</returns>
    public Volunteer? Read(Func<Volunteer, bool> filter)
    {
        if (DataSourse.Volunteers == null)
            throw new DalDoesNotExistException("Volunteer", "The list of volunteers is empty or does not exist.");
        return DataSourse.Volunteers!.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all volunteer entities, optionally filtered by a condition.
    /// </summary>
    /// <param name="filter">The optional filter function to apply.</param>
    /// <returns>An enumerable of volunteer entities.</returns>
    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool>? filter = null)
        => (filter == null) ? DataSourse.Volunteers!.Select(item => item) :
        DataSourse.Volunteers!.Where(filter);

    /// <summary>
    /// Updates an existing volunteer entity.
    /// </summary>
    /// <param name="item">The volunteer entity to be updated.</param>
    /// <exception cref="DalDoesNotExistException">Thrown if the volunteer does not exist.</exception>
    public void Update(Volunteer item)
    {
        Volunteer? find = DataSourse.Volunteers!.Find(obj => obj.VolunteerId == item.VolunteerId);
        if (find is null)
            throw new DalDoesNotExistException("Volunteer", $"An object from type Volunteer with id: {item.VolunteerId} does not exist.");
        DataSourse.Volunteers.Remove(find);
        DataSourse.Volunteers.Add(item);
    }
}
