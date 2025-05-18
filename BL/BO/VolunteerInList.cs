namespace BO
{
    /// <summary>
    /// Interface representing a volunteer in the list.
    /// </summary>
    public class VolunteerInList
    {
        /// <summary>
        /// The identifier of the volunteer.
        /// </summary>
        public int VolunteerId { get; init; }

        /// <summary>
        /// The full name of the volunteer (first and last name).
        /// </summary>
        public string FullName { get; init; }

        /// <summary>
        /// Indicates whether the volunteer is active.
        /// </summary>
        public bool IsActive { get; init; }

        /// <summary>
        /// The total number of calls handled by the volunteer.
        /// </summary>
        public int HandledCallsCount { get; init; }

        /// <summary>
        /// The total number of calls canceled by the volunteer.
        /// </summary>
        public int CanceledCallsCount { get; init; }

        /// <summary>
        /// The total number of calls expired by the volunteer.
        /// </summary>
        public int ExpiredCallsCount { get; init; }

        /// <summary>
        /// The identifier of the current call being handled by the volunteer, if any.
        /// Can be null if the volunteer is not currently handling a call.
        /// </summary>
        public int? CurrentCallId { get; init; }

        /// <summary>
        /// Specifies the type of the call. This property is read-only and can only be set during object initialization.
        /// </summary>
        public CallType CallType { get; init; }
    }
}
