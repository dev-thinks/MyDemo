using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDemo.Core.Data
{
    public class ApiResult<T>
    {
        public ApiResult()
        {
            this.Success = true;
        }
        /// <summary>
        /// TRUE if the Api attempt is successful, FALSE otherwise.
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Api return result message
        /// </summary>
        public string Message { get; set; } = null!;
        /// <summary>
        /// Return the data for Api
        /// </summary>
        public T? Data { get; set; }
    }
}