namespace BlApi
{
    /// <summary>
    /// Defines administrative operations for system management.
    /// </summary>
    public interface IAdmin
    {
        /// <summary>
        /// Retrieves the current system clock time.
        /// </summary>
        public DateTime GetSystemClock();
        /// <summary>
        /// Advances the system clock by a specified time unit.
        /// </summary>
        /// <param name="timeUnit">The unit of time to advance the clock.</param>
        public void ClockPromotion(BO.TimeUnit timeUnit);
        /// <summary>
        /// Gets the current risk range duration.
        /// </summary>
        /// <returns>A TimeSpan representing the risk range.</returns>
        public TimeSpan GetRiskRange();
        /// <summary>
        /// Sets the risk range duration.
        /// </summary>
        /// <param name="riskRange">The TimeSpan value to set as the risk range.</param>
        public void SetRiskRange(TimeSpan riskRange);
        /// <summary>
        /// Resets the database to its initial state.
        /// </summary>
        public void ResetDB();
        /// <summary>
        /// Initializes the database with default values.
        /// </summary>
        public void InitDB();

        #region Stage 5
        void AddConfigObserver(Action configObserver);
        void RemoveConfigObserver(Action configObserver);
        void AddClockObserver(Action clockObserver);
        void RemoveClockObserver(Action clockObserver);
        #endregion Stage 5
    }

}
