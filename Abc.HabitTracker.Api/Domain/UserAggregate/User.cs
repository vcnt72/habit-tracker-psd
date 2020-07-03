using System;
using System.Collections.Generic;
namespace Abc.HabitTracker.Api.Domain
{
    public class User
    {
        private Guid _id;

        private List<Badge> _badges;

        public Guid Id
        {
            get { return this._id; }
        }

        public User(Guid id, List<Badge> badges)
        {
            this._id = id;
            this._badges = badges;
        }

        public void AssignBadge(Badge badge)
        {
            this._badges.Add(badge);
        }

    }
}