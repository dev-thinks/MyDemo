using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MyDemo.Core.Data.Entity;
using MyDemo.Core.Repositories;

namespace MyDemo.Test.Mocks
{
    public static class Utility
    {
        /// <summary>
        /// Setup and mock the IQueryable object
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="mock"></param>
        /// <param name="queryable"></param> <summary>
        /// 
        /// </summary>
        /// <param name="mock"></param>
        /// <param name="queryable"></param>
        /// <typeparam name="TRepository"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static void SetupIQueryable<TRepository, TEntity>(this Mock<TRepository> mock, IQueryable<TEntity> queryable)
                where TRepository : class, IUserRepository
                where TEntity : User, new()
        {
            mock.Setup(r => r.GetAll().GetEnumerator()).Returns(queryable.GetEnumerator());
            mock.Setup(r => r.GetAll().Provider).Returns(queryable.Provider);
            mock.Setup(r => r.GetAll().ElementType).Returns(queryable.ElementType);
            mock.Setup(r => r.GetAll().Expression).Returns(queryable.Expression);
        }
    }
}