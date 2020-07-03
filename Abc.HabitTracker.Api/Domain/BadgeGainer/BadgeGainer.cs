using System.Collections.Generic;
using System;
using System.Linq;

namespace Abc.HabitTracker.Api.Domain
{
    public class BadgeGainer
    {
        private List<Logs> _logs;

        private IUserRepository _userRepository;

        private Guid _userId;
        public BadgeGainer(List<Logs> logs, Guid userID, IUserRepository userRepository)
        {
            this._logs = logs;
            this._userId = userID;
            this._userRepository = userRepository;
        }

        public void Gain()
        {
            var user = this._userRepository.FindById(_userId);
            if (this._logs.Count >= 4)
            {
                var longestStreak = this._logs.OrderByDescending(e => e.Streak).FirstOrDefault().Streak;
                if (longestStreak == 4)
                {
                    user.AssignBadge(new Badge("Longest Streak"));
                    this._userRepository.AssignBadge(user);
                }

                var countDaysoff = this._logs.Where(e => e.DaysOff == true).ToList().Count;
                if (countDaysoff == 10)
                {
                    user.AssignBadge(new Badge("Workaholic"));
                    this._userRepository.AssignBadge(user);
                }

                var newestStreak = this._logs.LastOrDefault().Streak;
                if (newestStreak == 10)
                {
                    var last10Logs = this._logs.ElementAt(this._logs.Count - 10);
                    var compareDate = last10Logs.CreatedAt - DateTime.Now;

                    if (compareDate.TotalDays == 10)
                    {
                        user.AssignBadge(new Badge("Epic Comeback"));
                        this._userRepository.AssignBadge(user);
                    }
                }
            }
        }
    }
}