using System;

namespace Abc.HabitTracker.Api.Domain
{
    public class Logs
    {

        private int _streak;

        private bool _daysOff;

        private DateTime _createdAt;

        public int Streak
        {
            get
            {
                return this._streak;
            }
        }


        public bool DaysOff
        {
            get
            {
                return this._daysOff;
            }
        }

        public DateTime CreatedAt
        {
            get
            {
                return this._createdAt;
            }
        }

        public Logs(int streak, bool daysoff)
        {

            this._daysOff = daysoff;
            this._streak = streak;
            this._createdAt = DateTime.Now;
        }

        public Logs(int streak, bool daysoff, DateTime createdAt)
        {
            this._daysOff = daysoff;
            this._streak = streak;
            this._createdAt = createdAt;

        }


        public bool IsStreak()
        {
            return this._streak == 1 ? false : true;
        }
    }
}