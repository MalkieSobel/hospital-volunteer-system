using DO;
using Helpers;
namespace BO
{
    /// <summary>
    /// Represents a volunteer in the system, including personal details, 
    /// activity status, location, and call handling statistics.
    /// </summary>
    public class Volunteer
    {
        /// <summary>
        /// Gets the unique identifier of the volunteer.
        /// </summary>
        public int VolunteerId { get; init; }
        /// <summary>
        /// Gets the full name of the volunteer.
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// Gets or sets the phone number of the volunteer.
        /// </summary>
        public string? PhoneNum { get; set; }
        /// <summary>
        /// Gets or sets the email address of the volunteer.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Gets or sets the password of the volunteer.
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// The address of the volunteer. This value may be null.
        /// </summary>
        public string? Adress { get; init; }
        /// <summary>
        /// Gets or sets the latitude coordinate of the volunteer's location.
        /// </summary>
        public double? Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude coordinate of the volunteer's location.
        /// </summary>
        public double? Longitude { get; set; }
        /// <summary>
        /// Gets or sets the role of the volunteer.
        /// </summary>
        public Role role { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the volunteer is active.
        /// </summary>
        public Boolean IsActive { get; set; }
        /// <summary>
        /// Gets or sets the maximum distance the volunteer is willing to travel.
        /// </summary>
        public double? MaxDistance { get; set; }
        /// <summary>
        /// Gets or sets the type of distance measurement used.
        /// </summary>
        public DistanceType? distanceType { get; set; }
        /// <summary>
        /// Gets or sets the total number of calls handled by the volunteer.
        /// </summary>
        public int? TotalCallsHandled { get; set; }
        /// <summary>
        /// Gets or sets the total number of calls canceled by the volunteer.
        /// </summary>
        public int? TotalCallsCanceled { get; set; }
        /// <summary>
        /// Gets or sets the total number of calls that expired before being handled.
        /// </summary>
        public int? TotalCallsExpired { get; set; }
        /// <summary>
        /// Gets or sets the call currently being handled by the volunteer, if any.
        /// </summary>
        public CallInProgress? CallInVolunteerCare { get; set; }

        public Volunteer() { }
        public Volunteer(int volunteerId, string fullName, string phoneNum, string email, string password, string adress, double latitude, double longitude, Role role, Boolean isActive, double maxDistance, DistanceType? distanceType, int? totalCallsHandled, int? totalCallsCanceled, int? totalCallsExpired, CallInProgress callInVolunteerCare)
        {
            VolunteerId = volunteerId;
            FullName = fullName;
            PhoneNum = phoneNum;
            Email = email;
            Password = password;
            Adress = adress;
            Latitude = latitude;
            Longitude = longitude;
            this.role = role;
            IsActive = isActive;
            MaxDistance = maxDistance;
            this.distanceType = distanceType;
            TotalCallsHandled = totalCallsHandled;
            TotalCallsCanceled = totalCallsCanceled;
            TotalCallsExpired = totalCallsExpired;
            CallInVolunteerCare = callInVolunteerCare;
        }
        public Volunteer(Volunteer volunteer)
        {
            VolunteerId = volunteer.VolunteerId;
            FullName = volunteer.FullName;
            PhoneNum = volunteer.PhoneNum;
            Email = volunteer.Email;
            Password = volunteer.Password;
            Adress = volunteer.Adress;
            Latitude = volunteer.Latitude;
            Longitude = volunteer.Longitude;
            role = volunteer.role;
            IsActive = volunteer.IsActive;
            MaxDistance = volunteer.MaxDistance;
            distanceType = volunteer.distanceType;
            TotalCallsHandled = volunteer.TotalCallsHandled;
            TotalCallsCanceled = volunteer.TotalCallsCanceled;
            TotalCallsExpired = volunteer.TotalCallsExpired;
            CallInVolunteerCare = volunteer.CallInVolunteerCare;
        }
    }

}
