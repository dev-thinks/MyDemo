using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyDemo.Core.Data.Entity;
using MyDemo.Core.Repositories.Interfaces;

namespace MyDemo.Core.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> UserIsExist(string userName);
    }
}