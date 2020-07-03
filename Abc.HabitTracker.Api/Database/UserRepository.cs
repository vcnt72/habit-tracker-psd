using System;
using System.Collections.Generic;
using Abc.HabitTracker.Api.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Abc.HabitTracker.Api.Database
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public void AssignBadge(User user)
        {
            throw new NotImplementedException();
        }

        public User FindById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public List<Badge> GetAllBadge(User user)
        {
            throw new NotImplementedException();
        }
    }
}