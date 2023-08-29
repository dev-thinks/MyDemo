using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDemo.Core.Data;
using MyDemo.Core.Data.Entity;
using MyDemo.Core.Repositories;
using MyDemo.Core.Services;

namespace MyDemo.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize()]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        protected readonly ILogger<User> _logger;

        public UserController(IUserRepository userRepository, ILogger<User> logger)
        {
            this._userRepository = userRepository;
            this._logger = logger;
        }

        /// <summary>
        /// Get all users
        /// Api Url: http://website-domain/api/users  (without controller name)
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        public async Task<ActionResult<DataResultService<User>>> GetUsers(
                        int pageIndex = 0, int pageSize = 10,
                        string? sortColumn = null, string? sortOrder = null,
                        string? filterColumn = null, string? filterQuery = null)
        {
            var apiResult = new ApiResult<DataResultService<User>>();

            _logger.LogDebug("LogDebug ================  GetUsers ");

            try
            {
                var users = _userRepository.GetAll();

                apiResult.Data = await DataResultService<User>.CreateAsync(
                            users,
                            pageIndex,
                            pageSize, sortColumn,
                            sortOrder, filterColumn,
                            filterQuery);

                //apiResult.Data = await _userRepository.GetAllAsync();

                return Ok(apiResult);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUsers Exception");
                apiResult.Success = false;
                apiResult.Message = ex.Message;
                return StatusCode(500, apiResult);
            }
        }

        /// <summary>
        /// Get user by id
        /// Api Url: http://website-domain/api/user/id  (without controller name)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("user/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var apiResult = new ApiResult<User>();

            try
            {
                apiResult.Data = await _userRepository.GetItemWithConditionAsync(u => u.Id == id);

                return Ok(apiResult);

            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = ex.Message;
                return StatusCode(500, apiResult);
            }
        }

        /// <summary>
        /// Create a user
        /// Api Url: http://website-domain/api/user  (without controller name)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var apiResult = new ApiResult<User>();

            try
            {
                var isExist = await _userRepository.UserIsExist(user.Name);
                if (isExist)
                {
                    apiResult.Success = false;
                    apiResult.Message = "The user has already exists!";
                }
                else
                {
                    _userRepository.Create(user);
                    await _userRepository.SaveAsync();
                    apiResult.Data = user;
                }

                return Ok(apiResult);
            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = ex.Message;
                return StatusCode(500, apiResult);
            }
        }


        /// <summary>
        /// Update the user
        /// Api Url: http://website-domain/api/user  (without controller name)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("user")]
        public async Task<ActionResult<User>> PutUser(User user)
        {
            var apiResult = new ApiResult<User>();

            //if the id is 0, then don't allow to update it
            if (user.Id == 0)
            {
                return BadRequest();
            }

            try
            {
                //get the current DB user
                var dbUser = await _userRepository.GetItemWithConditionAsync(u => u.Id == user.Id);
                if (dbUser == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Can't found the user!";
                }
                else
                {
                    //update the user
                    _userRepository.Update(user);
                    await _userRepository.SaveAsync();
                    apiResult.Data = user;
                }

                return Ok(apiResult);
            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = ex.Message;
                return StatusCode(500, apiResult);
            }
        }

        /// <summary>
        /// Delete user
        /// Api Url: http://website-domain/api/user/id  (without controller name)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var apiResult = new ApiResult<Object>();

            try
            {
                var dbUser = await _userRepository.GetItemWithConditionAsync(u => u.Id == id);
                if (dbUser == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Can't found the user!";
                }
                else
                {
                    //delete the user
                    _userRepository.Delete(dbUser);
                    await _userRepository.SaveAsync();
                }

                return Ok(apiResult);
            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = ex.Message;
                return StatusCode(500, apiResult);
            }
        }
    }
}