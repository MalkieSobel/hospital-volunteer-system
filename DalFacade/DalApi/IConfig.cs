namespace DalApi;
/// <summary>
/// Interface for system configuration settings.
/// Provides properties for system time and risk range, 
/// and a method to reset the configuration.
/// </summary>
public interface IConfig
{
    /// <summary>
    /// Gets or sets the system clock time.
    /// </summary>
    DateTime Clock { get; set; }

    /// <summary>
    /// Gets or sets the risk range for assignments.
    /// </summary>
    TimeSpan RiskRange { get; set; }

    /// <summary>
    /// Resets the configuration to its initial state.
    /// </summary>
    void Reset();
}
