using System.Collections.Generic;

using System;

namespace Abc.HabitTracker.Api.Domain
{
    public interface IUserRepository
    {
        List<Badge> GetAllBadge(User user);

        void AssignBadge(User user);
        User FindById(Guid Id);
    }
}