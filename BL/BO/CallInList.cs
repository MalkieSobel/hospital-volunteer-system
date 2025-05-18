namespace BO
{
    /// <summary>
    /// Interface representing a call in the list.
    /// </summary>
    public class CallInList
    {
        /// <summary>
        /// The identifier of the assignment entity.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// The unique identifier of the call entity.
        /// </summary>
        public int CallId { get; init; }

        /// <summary>
        /// The type of the call.
        /// </summary>
        public CallType callType { get; init; }

        /// <summary>
        /// The open time of the call.
        /// </summary>
        public DateTime OpenTime { get; init; }

        /// <summary>
        /// The total time remaining to complete the call.
        /// Can be null if there is no maximum time defined.
        /// </summary>
        public TimeSpan? TimeLeft { get; init; }

        /// <summary>
        /// The name of the last volunteer who took the call.
        /// Can be null if the call was not handled by any volunteer.
        /// </summary>
        public string? LastVolunteerName { get; init; }

        /// <summary>
        /// The total time to complete the call.
        /// Can be null if the call has not been completed.
        /// </summary>
        public TimeSpan? TimeToComplete { get; init; }

        /// <summary>
        /// The status of the call.
        /// </summary>
        public Status Status { get; init; }

        /// <summary>
        /// The total number of assignments for the current call.
        /// </summary>
        public int AssignmentsCount { get; init; }
    }
}

