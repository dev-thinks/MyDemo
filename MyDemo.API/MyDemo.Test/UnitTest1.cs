using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyDemo.Controllers;
using MyDemo.Core.Data;
using MyDemo.Core.Data.Entity;
using MyDemo.Test.Mocks;

namespace MyDemo.Test;


public class UnitTest1
{
    [Fact]
    public void Test1_GetUsers()
    {
        var mockUserRepository = MockIUserRepository.GetMock();
        var users = mockUserRepository.Object.GetAll();

        Assert.NotNull(users);
        Assert.IsAssignableFrom<IQueryable<User>>(users);
        Assert.Equal(2, users.Count());
    }


    [Fact]
    public void Test2_GetUserById()
    {
        var mockUserRepository = MockIUserRepository.GetMock();

        //mock the logger
        var logger = Mock.Of<ILogger<User>>();

        var userController = new UserController(mockUserRepository.Object, logger);

        var result = userController.GetUser(100);

        var objResult = result.Result.Result as ObjectResult;

        var apiResult = objResult.Value as ApiResult<User>;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
        Assert.IsAssignableFrom<User>(apiResult.Data);
        Assert.Equal("Unit Tester 1", apiResult.Data.Name);
    }

    [Fact]
    public void Test3_CreateUser()
    {
        var mock = MockIUserRepository.GetMock();

        //mock the logger
        var logger = Mock.Of<ILogger<User>>();

        var userController = new UserController(mock.Object, logger);

        var result = userController.PostUser(new User()
        {
            Id = 300,
            Name = "new user",
            Email = "newTester@abc.com",
            IsActive = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        });

        var objResult = result.Result.Result as ObjectResult;

        var apiResult = objResult.Value as ApiResult<User>;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
        Assert.Equal(true, apiResult.Success);
        Assert.Equal(300, apiResult.Data.Id);
    }

    [Fact]
    public void Test4_UpdateUser()
    {
        var mock = MockIUserRepository.GetMock();

        //mock the logger
        var logger = Mock.Of<ILogger<User>>();

        var userController = new UserController(mock.Object, logger);

        var result = userController.PutUser(new User()
        {
            Id = 100,
            Name = "update user",
            Email = "updateTester@abc.com",
            IsActive = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        });

        var objResult = result.Result.Result as ObjectResult;

        var apiResult = objResult.Value as ApiResult<User>;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
        Assert.Equal(true, apiResult.Success);
        Assert.Equal("update user", apiResult.Data.Name);
    }

    
    [Fact]
    public void Test5_DeleteUserById()
    {
        var mock = MockIUserRepository.GetMock();

        //mock the logger
        var logger = Mock.Of<ILogger<User>>();

        var userController = new UserController(mock.Object, logger);

        var result = userController.DeleteUser(100);

        var objResult = result.Result as ObjectResult;

        var apiResult = objResult.Value as ApiResult<Object>;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
        Assert.Equal(true, apiResult.Success);
    }
}

