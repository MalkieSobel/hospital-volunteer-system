using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Represents a call in the system.
    /// </summary>
    public class Call
    {
        /// <summary>
        /// Gets the unique identifier of the call.
        /// </summary>
        public int CallId { get; init; }
        /// <summary>
        /// Gets or sets the type of the call.
        /// </summary>
        public CallType callType { get; set; }
        /// <summary>
        /// Gets or sets the verbal description of the call.
        /// </summary>
        public string? VerbalDescription { get; set; }
        /// <summary>
        /// Gets or sets the address related to the call.
        /// </summary>
        public string? Adress { get; set; }
        /// <summary>
        /// Gets or sets the latitude of the call location.
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude of the call location.
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Gets or sets the time when the call was opened.
        /// </summary>
        public DateTime CallOpenTime { get; set; }
        /// <summary>
        /// Gets or sets the maximum time by which the call should be completed.
        /// </summary>
        public DateTime MaxEndTime { get; set; }
        /// <summary>
        /// Gets or sets the current status of the entity.
        /// </summary>
        public Status status { get; set; }
        /// <summary>
        /// A list of BO.CallAssignInList entities for past and current call assignments.
        /// Returns null if no assignments exist.
        /// </summary>
        public List<BO.CallAssignInList> CallAssignments { get; set; }
        /// <summary>
        /// Returns a string representation of the call object.
        /// </summary
        public override string ToString() => this.ToStringProperty();

        public Call() { }
        public Call(int callId, CallType callType, string verbalDescription, string adress, double latitude, double longitude, DateTime callOpenTime, DateTime maxEndTime, Status status, List<CallAssignInList> callAssignments)
        {
            CallId = callId;
            this.callType = callType;
            VerbalDescription = verbalDescription;
            Adress = adress;
            Latitude = latitude;
            Longitude = longitude;
            CallOpenTime = callOpenTime;
            MaxEndTime = maxEndTime;
            this.status = status;
            CallAssignments = callAssignments;
        }
        public Call(Call call)
        {
            CallId = call.CallId;
            callType = call.callType;
            VerbalDescription = call.VerbalDescription;
            Adress = call.Adress;
            Latitude = call.Latitude;
            Longitude = call.Longitude;
            CallOpenTime = call.CallOpenTime;
            MaxEndTime = call.MaxEndTime;
            status = call.status;
            CallAssignments = call.CallAssignments;
        }
    }
}
