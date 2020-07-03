using Abc.HabitTracker.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abc.HabitTracker.Api
{
    public class HabitApiMapper
    {
        public HabitJson Map(Habit habit)
        {
            return new HabitJson
            {
                UserID = habit.UserId,
                ID = habit.Id,
                Logs = habit.Logs.Count == 0 ? null : habit.Logs.Select(e => e.CreatedAt).ToArray(),
                CurrentStreak = habit.Logs.Count == 0 ? (short)0 : (short)habit.Logs.LastOrDefault().Streak,
                LongestStreak = habit.Logs.Count == 0 ? (short)0 : (short)habit.Logs.OrderByDescending(e => e.Streak).First().Streak,
                LogCount = habit.Logs == null ? (short)0 : (short)habit.Logs.Count,
                Name = habit.Name,
                DaysOff = this.formatDaysOff(habit.DaysOff),
                CreatedAt = habit.CreatedAt
            };
        }


        public HabitJson[] MapMany(List<Habit> habits)
        {
            List<HabitJson> habitJsons = new List<HabitJson>();

            foreach (var habit in habits)
            {
                habitJsons.Add(this.Map(habit));
            }

            return habitJsons.ToArray();
        }


        private string[] formatDaysOff(DaysOff daysOff)
        {
            return daysOff.Days.Select(e => e.Name).ToArray();
        }
    }
}