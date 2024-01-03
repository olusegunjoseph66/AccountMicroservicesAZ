using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.ExternalServices.Configurations;
using Shared.ExternalServices.DTOs;
using Shared.ExternalServices.Interfaces;

namespace Shared.ExternalServices.APIServices
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _cache;
        private readonly CacheServiceSetting _cachingSetting;
        private readonly IConfiguration _config;

        public CachingService(IDistributedCache cache, IOptions<CacheServiceSetting> cachingSetting, IConfiguration _config)
        {
            _cache = cache;
            _cachingSetting = cachingSetting.Value;
            this._config = _config;
        }

        /// <summary>
        /// To store a new value in the cache using the Key provided.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The name of the Key used to store a value.</param>
        /// <param name="data">The Generic Data to be stored</param>
        /// <param name="cancellationToken"></param>
        /// <param name="absoluteExpireTimeInMinutes">Time set for the expiry of the Cache item. Not setting a value here will mean using the default value of the system.</param>
        /// <param name="slidingExpireTimeInMinutes">The time of inactivity before the cache expires.</param>
        /// <returns></returns>
        public async Task SetAsync(string key, object data, TimeSpan? absoluteExpireTimeInMinutes = null,
                                                   TimeSpan? slidingExpireTimeInMinutes = null, CancellationToken cancellationToken = default)
        {
            // Check to see if a value with this key already exist. If yes, remove it to re-add.
            await RemoveAsync(key, cancellationToken);

            //var keyExpiryTimeSpan = absoluteExpireTimeInMinutes ?? TimeSpan.FromMinutes(_cachingSetting.DefaultExpiryTimeInMinutes);
            var keyExpiryTimeSpan = absoluteExpireTimeInMinutes ?? TimeSpan.FromMinutes(Convert.ToInt32(_config["CacheServiceSetting:DefaultExpiryTimeInMinutes"]));
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTimeInMinutes ?? TimeSpan.FromMinutes(Convert.ToInt32(_config["CacheServiceSetting:DefaultExpiryTimeInMinutes"])),
                SlidingExpiration = slidingExpireTimeInMinutes, 
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(keyExpiryTimeSpan.TotalMinutes)
            };

            CacheDto parser = new(data, DateTime.UtcNow, options.AbsoluteExpiration.Value, options.AbsoluteExpirationRelativeToNow, options.SlidingExpiration);
            var jsonData = JsonConvert.SerializeObject(parser);
            await _cache.SetStringAsync(key, jsonData, options, cancellationToken); 
        }

        /// <summary>
        /// To Update an existing Cache data using the Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The name of the Key used to store a value.</param>
        /// <param name="data">The Generic Data to be stored</param>
        /// <param name="requiresTimeReset">Used in updating the cache with an already existing key. If true, the initial time left for key expiry is forgotten, else, it is remembered and the cache will still expire at the initial set time.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="absoluteExpireTimeInMinutes">Time set for the expiry of the Cache item. Not setting a value here will mean using the default value of the system.</param>
        /// <param name="slidingExpireTimeInMinutes">The time of inactivity before the cache expires.</param>
        /// <returns></returns>
        public async Task UpdateAsync(string key, object data,bool requiresTimeReset = true, TimeSpan? absoluteExpireTimeInMinutes = null, TimeSpan? slidingExpireTimeInMinutes = null, CancellationToken cancellationToken = default)
        {
            var retrievedData = await _cache.GetStringAsync(key, cancellationToken);

            var options = new DistributedCacheEntryOptions();
            var initiationDate = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(retrievedData))
            {
                var deserializedData = JsonConvert.DeserializeObject<CacheDto>(retrievedData);
                initiationDate = deserializedData.InitiationTime;

                if (deserializedData.AbsoluteExpiration < DateTime.UtcNow)
                    requiresTimeReset = true;

                if (requiresTimeReset)
                {
                    var keyExpiryTimeSpan = absoluteExpireTimeInMinutes ?? TimeSpan.FromMinutes(Convert.ToInt32(_config["CacheServiceSetting:DefaultExpiryTimeInMinutes"]));
                    options = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = absoluteExpireTimeInMinutes ?? TimeSpan.FromMinutes(Convert.ToInt32(_config["CacheServiceSetting:DefaultExpiryTimeInMinutes"])),
                        SlidingExpiration = slidingExpireTimeInMinutes,
                        AbsoluteExpiration = DateTime.UtcNow.AddMinutes(keyExpiryTimeSpan.TotalMinutes)
                    };
                }
                else
                {
                    options = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = absoluteExpireTimeInMinutes ?? TimeSpan.FromMinutes(Convert.ToInt32(_config["CacheServiceSetting:DefaultExpiryTimeInMinutes"])),
                        SlidingExpiration = deserializedData.SlidingExpiration,
                        AbsoluteExpiration = deserializedData.AbsoluteExpiration
                    };
                }

                await _cache.RemoveAsync(key, cancellationToken);
            }
            else
            {
                var keyExpiryTimeSpan = absoluteExpireTimeInMinutes ?? TimeSpan.FromMinutes(Convert.ToInt32(_config["CacheServiceSetting:DefaultExpiryTimeInMinutes"]));
                options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = absoluteExpireTimeInMinutes ?? TimeSpan.FromMinutes(Convert.ToInt32(_config["CacheServiceSetting:DefaultExpiryTimeInMinutes"])),
                    SlidingExpiration = slidingExpireTimeInMinutes,
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(keyExpiryTimeSpan.TotalMinutes)
                };
            }

            CacheDto parser = new(data, initiationDate, options.AbsoluteExpiration.Value, options.AbsoluteExpirationRelativeToNow, options.SlidingExpiration);

            var jsonData = JsonConvert.SerializeObject(parser);
            await _cache.SetStringAsync(key, jsonData, options, cancellationToken);
        }

        /// <summary>
        /// To get a previously stored value in the cache using the Key provided.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The name of the Key used to store a value.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<object> GetAsync(string key, CancellationToken cancellationToken = default)
        {
            var jsonString = await _cache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrWhiteSpace(jsonString))
                return default;

            var response = JsonConvert.DeserializeObject<CacheDto>(jsonString);
            return response.Data;
        }

        /// <summary>
        /// To remove a previously stored value in the cache using the Key provided.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The name of the Key used to store a value.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            var retrievedData = await _cache.GetStringAsync(key, cancellationToken);
            if (!string.IsNullOrWhiteSpace(retrievedData))
            {
                await _cache.RemoveAsync(key, cancellationToken);
            }
        }
    }
}
