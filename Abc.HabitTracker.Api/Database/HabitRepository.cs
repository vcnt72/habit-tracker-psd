using System;
using System.Collections.Generic;
using Abc.HabitTracker.Api.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Abc.HabitTracker.Api.Database
{
    public class HabitRepository : IHabitRepository
    {

        private readonly DatabaseContext _context;
        public HabitRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public void Create(Habit habit)
        {

            var newHabit = new HabitDb
            {
                Id = habit.Id,
                Name = habit.Name,
                DaysOff = habit.DaysOff.Days.Select(e => e.Name).ToArray(),
                UserId = habit.UserId,
            };

            this._context.Set<HabitDb>().Add(newHabit);
        }

        public void Delete(Habit habit)
        {
            var habitDb = this.FindByIdDb(habit.Id);
            this._context.Set<HabitDb>().Remove(habitDb);
        }

        public Habit FindById(Guid id)
        {


            var habit = this.FindByIdDb(id);
            Console.WriteLine(habit.Id);
            return new Habit(habit.Id, habit.Name, DaysOff.createDaysOff(habit.DaysOff), habit.UserId, this.MapLogs(habit.Logs), habit.CreatedAt);

        }

        public List<Habit> FindByUserId(Guid userId)
        {
            List<Habit> habits = new List<Habit>();
            var habitDbs = this._context.Set<HabitDb>()
            .Where(e => e.UserId == userId).Include(e => e.Logs).Include(e => e.User)
            .ToList();

            foreach (var habit in habitDbs)
            {
                habits.Add(new Habit(habit.Id, habit.Name, DaysOff.createDaysOff(habit.DaysOff), habit.UserId, this.MapLogs(habit.Logs), habit.CreatedAt));
            }

            return habits;
        }

        public void Put(Guid id, Habit habit)
        {
            var habitDb = this._context.Set<HabitDb>().Include(e => e.Logs).Include(e => e.User).FirstOrDefault();
            habitDb.Name = habit.Name;
            habitDb.DaysOff = habit.DaysOff.Days.Select(e => e.Name).ToArray();
        }

        private HabitDb FindByIdDb(Guid id)
        {

            var habitDb = this._context.Set<HabitDb>().Where(e => e.Id == id).Include(e => e.Logs).Include(e => e.User).FirstOrDefault();


            if (habitDb == null)
            {
                throw new NullReferenceException("Habit cannot be null");
            }
            return habitDb;
        }

        public void DoLogs(Habit habit)
        {
            var logs = new LogDb
            {
                HabitId = habit.Id,
                UserId = habit.UserId,
                Streak = habit.GetNewestLogs().Streak
            };

            this._context.Set<LogDb>().Add(logs);
        }


        private List<Logs> MapLogs(IEnumerable<LogDb> logDbs)
        {
            return logDbs.Select(e => new Logs(e.Streak, e.DaysOff, e.CreatedAt)).ToList();
        }
    }
}