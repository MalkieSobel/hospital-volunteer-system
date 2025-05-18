using BlApi;
using BO;
using Helpers;
namespace BlImplementation
{
    internal class AdminImplementation : IAdmin
    {
        private readonly DalApi.IDal _dal = DalApi.Factory.Get;
        /// <summary>
        /// Advances the system clock by the selected time unit (minute, hour, day, month).
        /// </summary>
        /// <param name="timeUnit">The time unit to advance the clock by.</param>
        public void ClockPromotion(BO.TimeUnit timeUnit)
        {
            switch (timeUnit)
            {
                case TimeUnit.Minute:
                    AdminManager.UpdateClock(AdminManager.Now.AddMinutes(1)); // Advances by one minute
                    break;
                case TimeUnit.Hour:
                    AdminManager.UpdateClock(AdminManager.Now.AddHours(1)); // Advances by one hour
                    break;
                case TimeUnit.Day:
                    AdminManager.UpdateClock(AdminManager.Now.AddDays(1)); // Advances by one day
                    break;
                case TimeUnit.Month:
                    AdminManager.UpdateClock(AdminManager.Now.AddMonths(1)); // Advances by one month
                    break;
                case TimeUnit.Year:
                    AdminManager.UpdateClock(AdminManager.Now.AddYears(1)); // Advances by one year
                    break;
            }
        }
        /// <summary>
        /// Returns the risk range from the system settings.
        /// </summary>
        /// <returns>TimeSpan representing the risk range.</returns>
        public TimeSpan GetRiskRange()
        {
            return AdminManager.RiskRange; // Returns the risk range
        }
        /// <summary>
        /// Returns the current system clock time.
        /// </summary>
        /// <returns>The current date and time.</returns>
        public DateTime GetSystemClock()
        {
            return DateTime.Now; // Returns the current system time
        }
        /// <summary>
        /// Initializes the database by resetting it and loading initial data.
        /// </summary>
        public void InitDB()
        {
            AdminManager.ResetDB(); // Resets the database
            DalTest.Initialization.Do(); // Performs the initial setup
            AdminManager.UpdateClock(AdminManager.Now); // Updates the clock to the initial state
        }
        /// <summary>
        /// Resets the database.
        /// </summary>
        public void ResetDB()
        {
            AdminManager.ResetDB();  // Resets the database
        }
        /// <summary>
        /// Sets the new risk range.
        /// </summary>
        /// <param name="riskRange">The new risk range value.</param>
        public void SetRiskRange(TimeSpan riskRange)
        {
            AdminManager.RiskRange = riskRange; // Updates the risk range
        }

        #region Stage 5
        public void AddClockObserver(Action clockObserver) =>
        AdminManager.ClockUpdatedObservers += clockObserver;
        public void RemoveClockObserver(Action clockObserver) =>
        AdminManager.ClockUpdatedObservers -= clockObserver;
        public void AddConfigObserver(Action configObserver) =>
       AdminManager.ConfigUpdatedObservers += configObserver;
        public void RemoveConfigObserver(Action configObserver) =>
        AdminManager.ConfigUpdatedObservers -= configObserver;
        #endregion Stage 5
    }
}
