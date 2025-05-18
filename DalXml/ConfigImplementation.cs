namespace Dal;
using DalApi;
using System;
/// <summary>
/// Implementation of the IConfig interface for managing configuration settings.
/// Provides methods to get/set clock time, risk range, and reset configuration.
/// </summary>
internal class ConfigImplementation : IConfig
{
    /// <summary>
    /// Gets or sets the system clock time.
    /// </summary>
    public DateTime Clock
    {
        get => Config.Clock;
        set => Config.Clock = value;
    }

    /// <summary>
    /// Gets or sets the risk range for assignments.
    /// </summary>
    public TimeSpan RiskRange
    {
        get => Config.RiskRange;
        set => Config.RiskRange = value;
    }

    /// <summary>
    /// Resets the configuration to its initial state.
    /// </summary>
    public void Reset()
    {
        Config.Reset();
    }
}
