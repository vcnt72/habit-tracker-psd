using System;
using System.Collections.Generic;
using System.Linq;
namespace Abc.HabitTracker.Api.Domain
{


    public class DaysOff
    {
        private List<Day> _days = new List<Day>();


        public List<Day> Days
        {
            get
            {
                return this._days;
            }
        }

        public DaysOff(List<Day> days)
        {

            var container = days.Select(e => e.Name).Distinct().ToList();

            if (container.Count == 7)
            {
                throw new Exception("Days cannot be 7");
            }

            this._days = container.Select(e => new Day(e)).ToList();

        }

        public static DaysOff createDaysOff(string[] values)
        {

            List<Day> days = new List<Day>();

            foreach (var value in values)
            {
                days.Add(new Day(value));
            }

            return new DaysOff(days);
        }

        public bool IsDayOff(Day currentDay)
        {
            return this._days.FirstOrDefault(e => e.Name == currentDay.Name) == null ? false : true;
        }
    }
}