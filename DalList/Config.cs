namespace Dal;
/// <summary>
/// Provides static configuration settings for the application.
/// Includes properties like the current clock, risk range, and methods for resetting configuration.
/// </summary>
internal static class Config
{
    internal const int startCallId = 0;
    private static int nextCallId = startCallId;
    /// <summary>
    /// Gets the next available call ID.
    /// </summary>
    internal static int NextCallId { get => nextCallId++; }

    internal const int startAssignmentId = 0;
    private static int nextAssignmentId = startAssignmentId;
    /// <summary>
    /// Gets the next available assignment ID.
    /// </summary>
    internal static int NextAssignmentId { get => nextAssignmentId++; }

    internal static DateTime Clock { set; get; } = DateTime.Now;
    /// <summary>
    /// Gets or sets the system clock.
    /// </summary>
    internal static TimeSpan RiskRange { set; get; } = TimeSpan.FromMinutes(30);

    /// <summary>
    /// Resets the configuration to its initial state, including resetting IDs and the clock.
    /// </summary>
    internal static void Reset()
    {
        nextCallId = startCallId;
        nextAssignmentId = startAssignmentId;
        Clock = DateTime.Now;
    }
}
