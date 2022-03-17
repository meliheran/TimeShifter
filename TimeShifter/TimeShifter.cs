using System;
using System.Threading.Tasks;

namespace meliheran
{
    /// <summary>
    /// This class used for postponing the request which waits for the user interruption to finish and idle for a specific of time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TimeShifter<T>
    {
        private bool _isShifting = false;
        private int _shiftTimePool;
        private int _checkPeriod = 200;
        private T _obj;

        public delegate void TimeShiftHandler(T obj);
        public event TimeShiftHandler OnEndShiftingTime;

        public int ShiftingTime { get; set; }
        public bool IsShifting
        {
            get
            {
                return _isShifting;
            }
            private set 
            {
                _isShifting = value;
            }
        }

        /// <summary>
        /// Initializes a new Instance of the TimeShifter class using the specified shiftingTime and checkPeriodTime.
        /// </summary>
        /// <param name="shiftingTime">The Shifting Time to postpone next call in milliseconds.</param>
        /// <param name="checkPeriodTime">The Suggested Check Period of shifting event in milliseconds.</param>
        public TimeShifter(int shiftingTime = 1500, int checkPeriodTime = 200)
        {
            ShiftingTime = shiftingTime;
            _checkPeriod = checkPeriodTime; 
        }

        /// <summary>
        /// Method that shifts the until next request. Every shit call postpones the end of shift event until specfied Shifting Time
        /// </summary>
        /// <param name="obj">The object that will be retrieved when the time shifting end.</param>
        /// <returns>Returns Time Is Shifted until next call.</returns>
        public async Task<bool> Shift(T obj)
        {
            try
            {
                _obj = obj;

                if (IsShifting)
                {
                    _shiftTimePool = ShiftingTime;
                    return true;
                }


                //first start
                //or end of shifting, reset
                if (_shiftTimePool <= 0)
                {
                    _shiftTimePool = ShiftingTime;
                    IsShifting = true;
                }

                while (IsShifting)
                {
                    //_stillShifting = true;
                    await Task.Delay(_checkPeriod);

                    if (_shiftTimePool <= 0)
                    {
                        IsShifting = false;
                        if (OnEndShiftingTime != null) OnEndShiftingTime(_obj);
                        break;
                    }

                    _shiftTimePool -= _checkPeriod;
                }

                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        /// Returns the left time to call end of shift event
        /// </summary>
        /// <returns>Time in milliseconds</returns>
        public int GetShiftPoolTime()
        {
            return _shiftTimePool;
        }
    }
}
