using System.Data;
namespace DO;
/// <summary>
/// Represents a call that requires volunteer assistance.
/// </summary>
public record Call
(
    int CallId,              
    CallType callType,       
    string? VerbalDescription, 
    string Adress,           
    double Latitude,         
    double Longitude,        
    DateTime CallOpenTime,   
    DateTime MaxEndTime      
)
{
    public Call() : this(0, (CallType)1 , "", "", 0, 0, default(DateTime), default(DateTime)) { }
    /// <summary>
    /// Returns a string representation of the call.
    /// </summary>
    /// <returns>A string describing the call's details.</returns>
    public override string ToString()
    {
        return $"CallId: {CallId}, " +
               $"CallType: {callType}, " +
               $"VerbalDescription: {VerbalDescription ?? ""}, " +
               $"Adress: {Adress}, " +
               $"Latitude: {Latitude}, " +
               $"Longitude: {Longitude}, " +
               $"CallOpenTime: {CallOpenTime:yyyy-MM-dd HH:mm:ss}, " +
               $"MaxEndTime: {MaxEndTime:yyyy-MM-dd HH:mm:ss}";
    }
};
