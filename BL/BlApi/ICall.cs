namespace BlApi
{
    /// <summary>
    /// Interface for managing calls.
    /// </summary>
    public interface ICall : IEnumerable<BO.CallInList>, BlApi.IObservable //stage 5 
    {
        /// <summary>
        /// Requests the quantities of calls.
        /// </summary>
        /// <returns>Enumerable list of call summaries.</returns>
        public IEnumerable<BO.CallInList> GetCallCounts();
        /// <summary>
        /// Updates all open calls that have expired.
        /// </summary>
        //public void UpdateExpiredOpenCalls();
        /// <summary>
        /// Requests a list of calls with optional filtering and sorting.
        /// </summary>
        /// <param name="filterBy">Field to filter by (optional).</param>
        /// <param name="value">Value to filter with (optional).</param>
        /// <param name="sortBy">Field to sort by (optional).</param>
        /// <returns>Enumerable list of filtered and sorted calls.</returns>
        public IEnumerable<BO.CallInList> RequestACallList(BO.CallSortField? filterBy, object? value, BO.CallSortField? sortBy);
        /// <summary>
        /// Requests details of a specific call.
        /// </summary>
        /// <param name="id">The ID of the call.</param>
        /// <returns>Call details.</returns>
        public BO.Call ReadingDetailsRequest(int id);
        /// <summary>
        /// Updates the details of a call.
        /// </summary>
        /// <param name="call">The call object with updated details.</param>
        public void UpdateDetailsRequest(BO.Call call);
        /// <summary>
        /// Requests the deletion of a call.
        /// </summary>
        /// <param name="id">The ID of the call to delete.</param>
        public void DeleteCallRequest(int id);
        /// <summary>
        /// Adds a new call.
        /// </summary>
        /// <param name="call">The call object to add.</param>
        public void AddCall(BO.Call call);
        /// <summary>
        /// Retrieves a list of closed calls handled by a specific volunteer.
        /// </summary>
        /// <param name="id">The ID of the volunteer.</param>
        /// <param name="sortBy">Field to sort by (optional).</param>
        /// <param name="filterBy">Field to filter by (optional).</param>
        /// <returns>Enumerable list of closed calls.</returns>
        public IEnumerable<BO.ClosedCallInList> ListCloseCallsByVolunteer(int id, BO.CallSortField? sortBy, BO.CallFilterField? filterBy);
        /// <summary>
        /// Retrieves a list of open calls available for selection by a volunteer.
        /// </summary>
        /// <param name="id">The ID of the volunteer.</param>
        /// <param name="sortBy">Field to sort by (optional).</param>
        /// <param name="filterBy">Field to filter by (optional).</param>
        /// <returns>Enumerable list of open calls.</returns>
        public IEnumerable<BO.OpenCallInList> ListOpenCallsByVolunteer(int id, BO.CallSortField? sortBy, BO.CallFilterField? filterBy);
        /// <summary>
        /// Updates the status of a call to "treatment completed".
        /// </summary>
        /// <param name="idVolunteer">The ID of the volunteer.</param>
        /// <param name="idCall">The ID of the call.</param>
        public void UpdateEndCall(int idVolunteer, int idCall);
        /// <summary>
        /// Updates the status of a call to "treatment canceled".
        /// </summary>
        /// <param name="idVolunteer">The ID of the volunteer.</param>
        /// <param name="idCall">The ID of the call.</param>
        public void UpdateCancelCall(int idVolunteer, int idCall);
        /// <summary>
        /// Assigns a call to a volunteer for treatment.
        /// </summary>
        /// <param name="idVolunteer">The ID of the volunteer.</param>
        /// <param name="idCall">The ID of the call.</param>
        public void ChooseCallForTreatment(int idVolunteer, int idCall);
    }
}

