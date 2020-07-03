using Abc.HabitTracker.Api.Domain;

namespace Abc.HabitTracker.Api.Database
{
    public interface IUnitOfWork
    {
        IHabitRepository HabitRepository { get; }
        IUserRepository UserRepository { get; }
        void Commit();
        void Rollback();

    }
}