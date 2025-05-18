namespace BO
{
    /// <summary>
    /// Interface representing an open call in the list.
    /// </summary>
    public class OpenCallInList
    {
        /// <summary>
        /// The identifier of the call.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// The type of the call.
        /// </summary>
        public CallType CallType { get; init; }

        /// <summary>
        /// A textual description of the call (can be null).
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// The full address of the call.
        /// </summary>
        public string Address { get; init; }

        /// <summary>
        /// The time the call was opened.
        /// </summary>
        public DateTime OpenTime { get; init; }

        /// <summary>
        /// The maximum time by which the call should be finished (can be null).
        /// </summary>
        public DateTime? MaxFinishTime { get; init; }

        /// <summary>
        /// The distance of the call from the volunteer.
        /// </summary>
        public double DistanceFromVolunteer { get; init; }
    }
}
