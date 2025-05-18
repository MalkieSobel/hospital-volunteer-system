using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DalApi;
/// <summary>
/// Defines CRUD (Create, Read, Update, Delete) operations 
/// for managing entities of type T.
/// </summary>
/// <typeparam name="T">The type of the entity to be managed.</typeparam>
public interface ICrud<T> where T : class
{
    /// <summary>
    /// Creates a new entity of type T.
    /// </summary>
    /// <param name="item">The entity to be created.</param>
    void Create(T item);
    /// <summary>
    /// Reads an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity if found, otherwise null.</returns>
    T? Read(int id);
    /// <summary>
    /// Reads an entity based on a filter function.
    /// </summary>
    /// <param name="filter">The filter function to apply to the entities.</param>
    /// <returns>The first entity that matches the filter, or null if none found.</returns>
    T? Read(Func<T, bool> filter);
    /// <summary>
    /// Reads all entities, optionally filtered by a condition.
    /// </summary>
    /// <param name="filter">The optional filter function to apply.</param>
    /// <returns>An enumerable of entities.</returns>
    IEnumerable<T> ReadAll(Func<T, bool>? filter = null);
    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="item">The entity to be updated.</param>
    void Update(T item);
    /// <summary>
    /// Deletes an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to be deleted.</param>
    void Delete(int id);
    /// <summary>
    /// Deletes all entities of type T.
    /// </summary>
    void DeleteAll();
}
