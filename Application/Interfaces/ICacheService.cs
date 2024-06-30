using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string cacheKey);
        Task SetAsync<T>(string cacheKey, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null);
    }
}