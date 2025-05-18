namespace DO;
/// <summary>
/// Represents a volunteer with associated details such as contact information, location, and role.
/// </summary>
public record Volunteer
(
    int VolunteerId,        
    string FullName,        
    string PhoneNum,        
    string Email,           
    string? Password,       
    string? Adress,         
    double? Latitude,       
    double? Longitude,      
    Role role,              
    Boolean IsActive,       
    double? MaxDistance,    
    DistanceType distanceType = DistanceType.Aerial 
)
{
    public Volunteer() : this(0, "", "", "", "", "", 0, 0, (Role)1, false, 0, 0) { }
    /// <summary>
    /// Returns a string representation of the volunteer.
    /// </summary>
    /// <returns>A string describing the volunteer's details.</returns>
    public override string ToString()
    {
        return $"VolunteerId: {VolunteerId}, " +
               $"FullName: {FullName}, " +
               $"PhoneNum: {PhoneNum}, " +
               $"Email: {Email}, " +
               $"Password: {Password ?? ""}, " +
               $"Address: {Adress ?? ""}, " +
               $"Latitude: {Latitude?.ToString() ?? ""}, " +
               $"Longitude: {Longitude?.ToString() ?? ""}, " +
               $"Role: {role.ToString() ?? ""}, " +
               $"IsActive: {IsActive}, " +
               $"MaxDistance: {MaxDistance?.ToString() ?? ""}, " +
               $"DistanceType: {distanceType}";
    }
}
