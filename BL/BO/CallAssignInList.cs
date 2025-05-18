namespace BO
{
    /// <summary>
    /// Interface representing an assigned call in the list.
    /// </summary>
    public class CallAssignInList
    {
        /// <summary>
        /// The volunteer's ID (can be null if the assignment was created artificially).
        /// </summary>
        public int? VolunteerId { get; init; }

        /// <summary>
        /// The name of the volunteer (can be null if the assignment was created artificially).
        /// </summary>
        public string? VolunteerName { get; init; }

        /// <summary>
        /// The time when the call was assigned for treatment.
        /// </summary>
        public DateTime AssignmentTime { get; init; }

        /// <summary>
        /// The actual time when the treatment was completed (can be null if the treatment is not finished).
        /// </summary>
        public DateTime? ActualFinishTime { get; init; }

        /// <summary>
        /// The type of treatment end (can be null if not defined).
        /// </summary>
        public TypeTreatEnd? TypeTreatEnd { get; init; }
    }
}

