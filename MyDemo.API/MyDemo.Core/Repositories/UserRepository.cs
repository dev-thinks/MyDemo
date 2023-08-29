using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDemo.Core.Data.Entity;

namespace MyDemo.Core.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) {            

        }

        public async Task<bool> UserIsExist(string userName)
        {
            var user = await GetItemWithConditionAsync(u=>u.Name.ToUpper() == userName.ToUpper());
            return user != null;
        }
    }
}