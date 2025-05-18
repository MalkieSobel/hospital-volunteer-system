namespace DO;
/// <summary>
/// Represents an assignment of a volunteer to a call.
/// </summary>
public record Assignment
(
    int AssignmentId,     
    int CallId,           
    int? VolunteerId,      
    DateTime TreatStart,  
    DateTime? TreatEnd,    
    TypeTreatEnd? typeTreatEnd 
)
{
    public Assignment() : this(0, 0, 0, default(DateTime), default(DateTime), (TypeTreatEnd)1) { }
    /// <summary>
    /// Returns a string representation of the assignment.
    /// </summary>
    /// <returns>A string describing the assignment details.</returns>
    public override string ToString()
    {
        return $"AssignmentId: {AssignmentId}, " +
               $"CallId: {CallId}, " +
               $"VolunteerId: {VolunteerId}, " +
               $"TreatStart: {TreatStart:yyyy-MM-dd HH:mm:ss}, " +
               $"TreatEnd: {TreatEnd:yyyy-MM-dd HH:mm:ss}, " +
               $"TypeTreatEnd: {typeTreatEnd}";
    }
   
};
