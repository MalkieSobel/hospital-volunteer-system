namespace BO
{
    /// <summary>
    /// Interface representing a closed call in the list.
    /// </summary>
    public class ClosedCallInList
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
        /// The full address of the call.
        /// </summary>
        public string Address { get; init; }
        /// <summary>
        /// The time the call was opened.
        /// </summary>
        public DateTime OpenTime { get; init; }
        /// <summary>
        /// The time the call was assigned to a volunteer.
        /// </summary>
        public DateTime AssignmentTime { get; init; }
        /// <summary>
        /// The actual time the treatment was finished (can be null).
        /// </summary>
        public DateTime? ActualFinishTime { get; init; }
        /// <summary>
        /// The type of treatment end (can be null).
        /// </summary>
        public TypeTreatEnd? TypeTreatEnd { get; init; }
    }
}


