﻿using Core.Configurations;
using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;
public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly CacheConfiguration _cacheConfig;
    private MemoryCacheEntryOptions _cacheOptions;
    public MemoryCacheService(IMemoryCache memoryCache, IOptions<CacheConfiguration> cacheConfig)
    {
        _memoryCache = memoryCache;
        _cacheConfig = cacheConfig.Value;
        if (_cacheConfig != null)
        {
            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(_cacheConfig.AbsoluteExpirationInHours),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(_cacheConfig.SlidingExpirationInMinutes)
            };
        }
    }
    public bool TryGet<T>(string cacheKey, out T value)
    {
        _memoryCache.TryGetValue(cacheKey, out value);
        if (value == null) return false;
        else return true;
    }
    public T Set<T>(string cacheKey, T value)
    {
        return _memoryCache.Set(cacheKey, value, _cacheOptions);
    }
    public void Remove(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);
    }
}