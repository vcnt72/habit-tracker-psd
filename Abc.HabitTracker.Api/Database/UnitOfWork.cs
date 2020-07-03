using Abc.HabitTracker.Api.Domain;

namespace Abc.HabitTracker.Api.Database
{
    public class UnitOfWork : IUnitOfWork
    {


        private readonly DatabaseContext _databaseContext;
        private IHabitRepository _habitRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(DatabaseContext databaseContext)
        { _databaseContext = databaseContext; }

        public IHabitRepository HabitRepository
        {
            get
            {
                return this._habitRepository = _habitRepository ?? new HabitRepository(_databaseContext);
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                return this._userRepository = _userRepository ?? new UserRepository(_databaseContext);

            }
        }

        public void Commit()
        {
            _databaseContext.SaveChanges();
        }

        public void Rollback()
        {
            _databaseContext.Dispose();
        }
    }
}