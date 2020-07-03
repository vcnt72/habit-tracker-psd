using System;
using System.Collections.Generic;
using System.Linq;
using Abc.HabitTracker.Api.Domain;

namespace Abc.HabitTracker.Api.Domain
{

    public class Habit
    {
        private Guid _id;

        private string _name;

        private DaysOff _daysOff;

        private Guid _userId;

        private List<Logs> _logs;

        private DateTime _createdAt;



        public Guid Id
        {
            get
            {
                return this._id;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public DaysOff DaysOff
        {
            get
            {
                return this._daysOff;
            }
        }

        public Guid UserId
        {
            get
            {
                return this._userId;
            }
        }


        public List<Logs> Logs
        {
            get
            {
                return this._logs;
            }
        }

        public DateTime CreatedAt
        {
            get
            {
                return this._createdAt;
            }
        }

        public Habit(Guid id, string name, DaysOff daysOff, Guid userId)
        {

            if (name.Length < 2 || name.Length > 100)
            {
                throw new Exception("Invalid name");
            }

            this._id = id;
            this._name = name;
            this._daysOff = daysOff;
            this._userId = userId;
            this._logs = new List<Logs>();
            this._createdAt = DateTime.Now;
        }


        public Habit(Guid id, string name, DaysOff daysOff, Guid userId, List<Logs> logs, DateTime createdAt)
        {

            if (name.Length < 2 || name.Length > 100)
            {
                throw new Exception("Invalid name");
            }

            this._id = id;
            this._name = name;
            this._daysOff = daysOff;
            this._userId = userId;
            this._logs = logs;
            this._createdAt = createdAt;
        }

        public static Habit createHabit(string name, string[] daysoff, Guid userID)
        {
            return new Habit(Guid.NewGuid(), name, DaysOff.createDaysOff(daysoff), userID);
        }

        public void DoLogs()
        {
            bool dayoff = DaysOff.IsDayOff(this.GetLogsDay());
            if (this.IsAlreadyLogging())
            {
                throw new Exception("Can't do logs");
            }


            if (this._logs.Count == 0 || this.IsYesterdayLogging())
            {
                this._logs.Add(new Logs(1, dayoff));
            }
            else
            {
                this._logs.Add(new Logs(this.GetNewestLogs().Streak + 1, dayoff));
            }
        }

        public Logs GetNewestLogs()
        {
            return this._logs.LastOrDefault();
        }


        private Day GetLogsDay()
        {

            DateTime dateTime = DateTime.Now;

            return new Day(dateTime.DayOfWeek.ToString().Substring(0, 3));
        }


        private bool IsAlreadyLogging()
        {
            DateTime dateTime = DateTime.Now;

            var newestLogs = this.GetNewestLogs();

            if (newestLogs == null)
            {
                return false;
            }

            return dateTime.ToShortDateString() == this.GetNewestLogs().CreatedAt.ToShortDateString();
        }

        private bool IsYesterdayLogging()
        {
            DateTime lastDateLogs = this.GetNewestLogs().CreatedAt;
            DateTime yesterday = DateTime.Now.AddDays(-1);

            if (this._daysOff.IsDayOff(new Day(yesterday.DayOfWeek.ToString().Substring(0, 3))))
            {
                return true;
            }

            if (lastDateLogs.DayOfWeek == yesterday.DayOfWeek)
            {
                return true;
            }

            return false;
        }
    }
}