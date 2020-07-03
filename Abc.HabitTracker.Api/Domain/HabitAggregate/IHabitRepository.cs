using System;
using System.Collections.Generic;

namespace Abc.HabitTracker.Api.Domain
{
    public interface IHabitRepository
    {
        void Create(Habit habit);

        void Put(Guid Id, Habit habit);

        void Delete(Habit habit);

        Habit FindById(Guid id);

        List<Habit> FindByUserId(Guid userId);

        void DoLogs(Habit habit);
    }
}