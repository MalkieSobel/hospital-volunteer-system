using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;



namespace Helpers
{
    public static class Tools
    {
        /// <summary>
        /// This is an extension method that generates a string representation of the properties of an object.
        /// It iterates through all properties of the object, formats their values, and returns a string 
        /// that shows the object's type name and its property names with their values.
        /// </summary>
        /// <typeparam name="T">The type of the object to be represented as a string.</typeparam>
        /// <param name="t">The object whose properties are to be represented as a string.</param>
        /// <returns>A string representation of the object's type and its property names and values.</returns>
        public static string ToStringProperty<T>(this T t)
        {
            if (t == null) return "null";

            Type type = t.GetType();
            PropertyInfo[] properties = type.GetProperties();

            StringBuilder sb = new StringBuilder();
            sb.Append($"{type.Name} [");

            foreach (PropertyInfo prop in properties)
            {
                object? value = prop.GetValue(t);
                string valueString = FormatValue(value);
                sb.Append($"{prop.Name}: {valueString}, ");
            }

            if (properties.Length > 0)
                sb.Length -= 2; // Removes trailing comma and space

            sb.Append("]");
            return sb.ToString();
        }
        /// <summary>
        /// This method formats the value of an object for display in the ToStringProperty method.
        /// It handles null values, primitive types, strings, collections, and nested objects.
        /// </summary>
        /// <param name="value">The value to be formatted into a string.</param>
        /// <returns>A formatted string representing the value of the object.</returns>
        public static string FormatValue(object? value)
        {
            if (value == null) return "null";

            if (value is string || value.GetType().IsPrimitive)
                return value.ToString()!;

            if (value is IEnumerable enumerable)
            {
                StringBuilder listSb = new StringBuilder("[");
                foreach (var item in enumerable)
                    listSb.Append($"{FormatValue(item)}, ");

                if (listSb.Length > 1)
                    listSb.Length -= 2; // Removes trailing comma and space

                listSb.Append("]");
                return listSb.ToString();
            }

            return value.ToStringProperty(); // Recursive call for complex objects
        }
        /// <summary>
        /// Validates the format of the volunteer's data, ensuring it meets the required structural rules.
        /// This includes checks for:
        /// - Phone number format: Ensures the phone number consists of only digits and is either 9 or 10 characters long.
        /// - Email format: Ensures the email follows a basic email pattern (e.g., name@domain.com).
        /// - Password: Ensures the password is not empty or null.
        /// </summary>
        /// <param name="volunteer">The volunteer object whose data will be validated.</param>
        public static void ValidateVolunteerFormat(BO.Volunteer volunteer)
        {
            // Validate phone number format (numbers only, length 9 or 10)
            if (string.IsNullOrWhiteSpace(volunteer.PhoneNum) || !Regex.IsMatch(volunteer.PhoneNum, @"^\d{9,10}$"))
                throw new ArgumentException("Invalid phone number format.");
            // Validate email format (simplified regex for email)
            if (!string.IsNullOrWhiteSpace(volunteer.Email) && !Regex.IsMatch(volunteer.Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
                throw new ArgumentException("Invalid email address format.");
            // Validate ID format (simple check for numeric ID and length of 9 digits)
            if (string.IsNullOrWhiteSpace(volunteer.VolunteerId.ToString()) || !Regex.IsMatch(volunteer.VolunteerId.ToString(), @"^\d{9}$"))
                throw new ArgumentException("Invalid volunteer ID format (must be 9 digits).");
            // Validate full name length
            if (string.IsNullOrWhiteSpace(volunteer.FullName) || volunteer.FullName.Length < 3)
                throw new ArgumentException("Full name must be at least 3 characters long.");
            // Validate longitude and latitude format
            if ((volunteer.Latitude.HasValue && !double.IsNaN(volunteer.Latitude.Value) &&
                (volunteer.Longitude.HasValue && !double.IsNaN(volunteer.Longitude.Value))))
            {
                double lat = volunteer.Latitude.Value;
                double lon = volunteer.Longitude.Value;
            }
            else
            {
                throw new ArgumentException("Latitude and Longitude must be valid numeric values.");
            }
            // Check that password is not empty
            if (string.IsNullOrWhiteSpace(volunteer.Password))
                throw new ArgumentException("Password cannot be empty.");
        }
        /// <summary>
        /// Validates the logical consistency of the volunteer's data, ensuring it adheres to business rules.
        /// This includes checks for:
        /// - Max distance: Ensures the max distance a volunteer can travel is positive if specified.
        /// - Active status: Ensures that if the volunteer has an active call in progress, they must be active.
        /// </summary>
        /// <param name="volunteer">The volunteer object whose logical consistency will be validated.</param>
        public static void ValidateVolunteerLogic(BO.Volunteer volunteer)
        {
            // Check that max distance is not negative
            if (volunteer.MaxDistance <= 0)
                throw new ArgumentException("Max distance must be positive.");
            // Check that the volunteer is active if they have an active call
            if (volunteer?.IsActive == true && !volunteer.IsActive)
                throw new ArgumentException("A volunteer with an active call must be active.");
            // If there is location data, make sure it is not empty
            if (!(volunteer.Latitude.HasValue && !double.IsNaN(volunteer.Latitude.Value) &&
                !(volunteer.Longitude.HasValue && !double.IsNaN(volunteer.Longitude.Value))))
                throw new ArgumentException("Volunteer must have a valid location (latitude and longitude).");
        }
        public static void ValidatCallFormat(BO.Call call)
        {
            if (call == null)
                throw new ArgumentNullException(nameof(call), "הקריאה לא יכולה להיות ריקה.");
            if (call.CallId <= 0)
                throw new ArgumentException("מספר קריאה חייב להיות חיובי.");
            if (string.IsNullOrWhiteSpace(call.Adress))
                throw new ArgumentException("כתובת הקריאה לא יכולה להיות ריקה.");
            if (call.Latitude < -90 || call.Latitude > 90)
                throw new ArgumentException("ערך לא חוקי לקו רוחב.");
            if (call.Longitude < -180 || call.Longitude > 180)
                throw new ArgumentException("ערך לא חוקי לקו אורך.");
            if (call.CallOpenTime >= call.MaxEndTime)
                throw new ArgumentException("זמן סיום חייב להיות אחרי זמן פתיחת הקריאה.");
        }
        public static void ValidateCallLogic(BO.Call call)
        {
            if (call.status == BO.Status.Done && DateTime.Now < call.MaxEndTime)
                throw new InvalidOperationException("אי אפשר לסגור קריאה לפני שמגיע זמן הסיום.");
        }
        public static int GetCallInListStatus(DO.Call call, DO.Assignment assignment, DateTime now, TimeSpan riskRange)
        {
            // קריאה פתוחה - לא בטיפול של אף מתנדב
            if (assignment == null)
            {
                return 1;
            }
            // קריאה בטיפול
            if (assignment.TreatEnd == null)
            {
                return 2;
            }
            // קריאה סגורה - טיפול הושלם
            if (assignment.TreatEnd <= now)
            {
                return 3;
            }
            // קריאה פג תוקף - לא הסתיימה בזמן
            if (call.MaxEndTime <= now)
            {
                return 4;
            }
            // קריאה פתוחה בסיכון
            if (call.MaxEndTime - now <= riskRange)
            {
                return 5;
            }
            // קריאה בטיפול בסיכון
            if (assignment.TreatEnd - now <= riskRange)
            {
                return 6;
            }
            return 1;
        }
        public static double CalculateDistance(DO.Volunteer volunteer, DO.Call call)
        {
            const double EarthRadiusKm = 6371; // רדיוס כדור הארץ בקילומטרים

            if (volunteer.Latitude == null || volunteer.Longitude == null ||
                call.Latitude == null || call.Longitude == null)
            {
                throw new ArgumentException("Invalid coordinates provided.");
            }

            // המרת מעלות לרדיאנים
            double lat1 = DegreesToRadians((double)volunteer.Latitude);
            double lon1 = DegreesToRadians((double)volunteer.Longitude);
            double lat2 = DegreesToRadians((double)call.Latitude);
            double lon2 = DegreesToRadians((double)call.Longitude);

            // חישוב נוסחת ההברסין
            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dLon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }
        // המרה ממעלות לרדיאנים
        public static double DegreesToRadians(double degrees) => degrees * (Math.PI / 180);
       
    }
}
