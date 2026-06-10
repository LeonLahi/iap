using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Interfaces
{
    public interface IAsyncActionFilter<T> where T : class
    {
        Task<bool> OnActionExecutionAsync(T entity);
    }
}