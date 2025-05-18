namespace BO;
/// <summary>
/// Represents different roles within the system.
/// </summary>
public enum Role { Manager, Volunteer }
/// <summary>
/// Represents different types of distance measurements.
/// </summary>
public enum DistanceType { Aerial, Walking, Driving }
/// <summary>
/// Represents various call types that require volunteer assistance.
/// </summary>
public enum CallType { NurseHelp = 1, SongCheer, KidsActivity, CoockFood, Delivery, None }
/// <summary>
/// Represents different types of treatment end statuses.
/// </summary>
/// Enumeration representing the possible outcomes of a treatment process.
public enum TypeTreatEnd { Treated = 1, SelfCancle, ManagerCancle, Expired }
/// <summary>
/// Represents the various statuses a process or task can have.
/// </summary>
public enum Status { Open = 1, InProgress, Done, Expired, OpenAtRisk, InProgressAtRisk }
/// <summary>
/// Defines sorting fields for volunteers.
/// </summary>
public enum VolunteerSortField { Id = 1, Name, PhoneNum, Email }
/// <summary>
/// Defines the filtering fields available for call records.
/// </summary>
public enum CallFilterField { Id = 1, Name, Adress }
/// <summary>
/// Defines sorting fields for call records.
/// </summary>
public enum CallSortField { Id = 1, Type, Adress }
/// <summary>
/// Represents different units of time measurement.
/// </summary>
public enum TimeUnit { Minute = 1, Hour, Day, Month, Year }

