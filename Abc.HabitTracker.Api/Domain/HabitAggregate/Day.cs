using System.Collections.Generic;
using System;

namespace Abc.HabitTracker.Api.Domain
{

    public enum DaysOfTheWeek
    {
        Mon,
        Tue,
        Wed,
        Thu,
        Fri,
        Sat,
        Sun
    }

    public class Day
    {
        private string _name;

        public string Name
        {
            get
            {
                return this._name;
            }
        }


        public Day(string name)
        {
            if (!Enum.IsDefined(typeof(DaysOfTheWeek), name))
            {
                throw new Exception("Name invalid");
            }

            this._name = name;
        }

        public static Day GetYesterday()
        {
            DateTime yesterday = DateTime.Now.AddDays(-1);

            return new Day(yesterday.DayOfWeek.ToString().Substring(0, 3));
        }


        //        override object.Equals

        public override bool Equals(object obj)
        {
            var day = obj as Day;

            if (day == null)
            {
                throw new Exception("Cannot be null");
            }

            return this._name == day._name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}