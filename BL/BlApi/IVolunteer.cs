namespace BlApi
{
    /// <summary>
    /// Interface for managing volunteers.
    /// </summary>
    public interface IVolunteer : IEnumerable<BO.VolunteerInList> , BlApi.IObservable //stage 5 
    {
        /// <summary>
        /// Retrieves the role of a volunteer based on their credentials.
        /// </summary>
        /// <param name="userName">The username of the volunteer.</param>
        /// <param name="password">The password of the volunteer.</param>
        /// <returns>The role of the volunteer.</returns>
        public BO.Role GetRole(string userName, string password);
        /// <summary>
        /// Retrieves a list of volunteers with optional filtering and sorting.
        /// </summary>
        /// <param name="isActive">Filter by active status (optional).</param>
        /// <param name="sortBy">Field to sort by (optional).</param>
        /// <returns>Enumerable list of volunteers.</returns>
        public IEnumerable<BO.VolunteerInList> GetVolunteersList(bool? isActive, BO.VolunteerSortField? sortBy);
        /// <summary>
        /// Retrieves details of a specific volunteer.
        /// </summary>
        /// <param name="userId">The ID of the volunteer.</param>
        /// <returns>Volunteer details.</returns>
        public BO.Volunteer GetVolunteerDetails(int userId);
        /// <summary>
        /// Updates the details of a volunteer.
        /// </summary>
        /// <param name="userId">The ID of the volunteer.</param>
        /// <param name="volunteer">The updated volunteer object.</param>
        public void UpdateVolunteerDetails(int userId, BO.Volunteer volunteer);
        /// <summary>
        /// Deletes a volunteer from the system.
        /// </summary>
        /// <param name="userId">The ID of the volunteer to delete.</param>
        public void DeleteVolunteer(int userId);
        /// <summary>
        /// Adds a new volunteer to the system.
        /// </summary>
        /// <param name="volunteer">The volunteer object to add.</param>
        public void AddVolunteer(BO.Volunteer volunteer);
    }
}
