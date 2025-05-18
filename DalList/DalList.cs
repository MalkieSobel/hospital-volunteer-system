using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
/// <summary>
/// Provides an implementation of the IDal interface using in-memory storage.
/// This class offers access to volunteers, calls, assignments, and configuration.
/// </summary>
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();

    private DalList() { }
        
    /// <summary>
    /// Gets an instance of the volunteer implementation.
    /// </summary>
    public IVolunteer Volunteer { get; } = new VolunteerImplementation();

    /// <summary>
    /// Gets an instance of the call implementation.
    /// </summary>
    public ICall Call { get; } = new CallImplementation();

    /// <summary>
    /// Gets an instance of the assignment implementation.
    /// </summary>
    public IAssignment Assignment { get; } = new AssignmentImplementation();

    /// <summary>
    /// Gets an instance of the configuration implementation.
    /// </summary>
    public IConfig Config { get; } = new ConfigImplementation();

    /// <summary>
    /// Resets the database, clearing all data and resetting configurations.
    /// </summary>
    public void ResetDB()
    {
        Volunteer.DeleteAll();
        Call.DeleteAll();
        Assignment.DeleteAll();
        Config.Reset();
    }
}
