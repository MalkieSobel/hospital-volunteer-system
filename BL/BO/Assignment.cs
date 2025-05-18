using DO;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Represents an assignment of a volunteer to a call.
    /// </summary>
    public class Assignment
    {
        /// <summary>
        /// Gets the unique identifier of the assignment.
        /// </summary>
        public int AssignmentId { get; init; }

        /// <summary>
        /// Gets the identifier of the related call.
        /// </summary>
        public int CallId { get; init; }

        /// <summary>
        /// Gets the identifier of the assigned volunteer.
        /// </summary>
        public int VolunteerId { get; init; }

        /// <summary>
        /// Gets or sets the start time of the treatment.
        /// </summary>
        public DateTime TreatStart { get; set; }

        /// <summary>
        /// Gets or sets the end time of the treatment.
        /// </summary>
        public DateTime TreatEnd { get; set; }

        /// <summary>
        /// Gets or sets the type of treatment end.
        /// </summary>
        public TypeTreatEnd typeTreatEnd { get; set; }

        /// <summary>
        /// Returns a string representation of the assignment object.
        /// </summary>
        /// <returns>A string representation of the assignment properties.</returns>
        public override string ToString() => this.ToStringProperty();
    }
}
