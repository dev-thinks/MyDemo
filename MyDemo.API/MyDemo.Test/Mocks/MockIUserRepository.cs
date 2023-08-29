
using System.Linq.Expressions;
using Moq;
using MyDemo.Core.Data.Entity;
using MyDemo.Core.Repositories;

namespace MyDemo.Test.Mocks
{
    public class MockIUserRepository
    {
        public static Mock<IUserRepository> GetMock()
        {
            var mock = new Mock<IUserRepository>();

            var users = GetUsers();
            
            mock.Setup(u => u.GetAll()).Returns(()=> users);

            mock.Setup(u => u.GetItemWithConditionAsync(It.IsAny<Expression<Func<User, bool>>>())).
                ReturnsAsync((Expression<Func<User, bool>> expr) => users.FirstOrDefault(expr));

            mock.Setup(u => u.GetWithConditionAsync(It.IsAny<Expression<Func<User, bool>>>())).
                ReturnsAsync((Expression<Func<User, bool>> expr) => users.Where(expr));
                
            return mock;
        }


        public static Mock<IUserRepository> CRUDMock()
        {
            var mock = new Mock<IUserRepository>();

            var users = GetUsers();

            mock.Setup(u => u.GetItemWithConditionAsync(It.IsAny<Expression<Func<User, bool>>>())).
                ReturnsAsync((Expression<Func<User, bool>> expr) => users.FirstOrDefault(expr));
           
           
            // mock.Setup(u => u.Create(It.IsAny<User>())).Callback(() => { return; });

            // mock.Setup(u => u.Delete(It.IsAny<User>())).Callback(() => { return; });

            // mock.Setup(u => u.Update(It.IsAny<User>())).Callback(() => { return; });

            mock.Setup(u => u.Save()).Returns(1);

            mock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

            return mock;
        }

        public static IQueryable<User> GetUsers()
        {
            var users = new List<User>() {
            new User() {
                Id = 100,
                Name = "Unit Tester 1",
                Email = "unit.tester1@abc.com",
                IsActive = true,
                CreatedAt = DateTime.Now.AddDays(-2),
                UpdatedAt = DateTime.Now.AddDays(-2)
            },
            new User() {
                Id = 200,
                Name = "Unit Tester 2",
                Email = "unit.tester2@abc.com",
                IsActive = true,
                CreatedAt = DateTime.Now.AddDays(-1),
                UpdatedAt = DateTime.Now.AddDays(-1)
            }
            }.AsQueryable();
            return users;
        }

        private static Mock<IQueryable<User>> GetUsersMock(IQueryable<User> users)
        {
            var mock = new Mock<IQueryable<User>>();

            mock.Setup(u => u.GetEnumerator()).Returns(users.GetEnumerator());
            mock.Setup(u => u.Provider).Returns(users.Provider);
            mock.Setup(u => u.ElementType).Returns(users.ElementType);
            mock.Setup(u => u.Expression).Returns(users.Expression);

            return mock;
        }
    }
}