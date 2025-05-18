namespace BO
{
    public class CallInProgress
    {
        /// <summary>
        /// Gets the unique identifier of the Assignment.
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Gets the unique identifier of the call.
        /// </summary>
        public int CallId { get; init; }
        /// <summary>
        /// Gets or sets the type of the call.
        /// </summary>
        public CallType CallType { get; init; }
        /// <summary>
        /// Gets or sets the verbal description of the call.
        /// </summary>
        public string? VerbalDescription { get; init; }
        /// <summary>
        /// Gets or sets the address related to the call.
        /// </summary>
        public string? Adress { get; init; }
        /// <summary>
        /// Gets or sets the time when the call was opened.
        /// </summary>
        public DateTime CallOpenTime { get; init; }
        /// <summary>
        /// Gets or sets the maximum time by which the call should be completed.
        /// </summary>
        public DateTime MaxEndTime { get; init; }
        /// <summary>
        /// Represents the time when the treatment was entered.
        /// </summary>
        public DateTime TimeEnterTreatment { get; init; }
        /// <summary>
        /// Represents the distance from the volunteer to the treatment location.
        /// </summary>
        double DistanceFromVolunteer { get; init; }
        /// <summary>
        /// Gets or sets the current status of the entity.
        /// </summary>
        public Status Status { get; init; }

        public CallInProgress() { }
    }

}
