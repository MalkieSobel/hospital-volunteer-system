namespace DO;
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
public enum CallType { NurseHelp = 1, SongCheer, KidsActivity, CoockFood, Delivery }

/// <summary>
/// Represents different types of treatment end statuses.
/// </summary>
public enum TypeTreatEnd { Treated = 1, SelfCancle, ManagerCancle, Expired }
