namespace Shared.ExternalServices.Interfaces
{
    public interface ICachingService
    {
        Task<object> GetAsync(string key, CancellationToken cancellationToken = default);
        Task SetAsync(string key, object data, TimeSpan? absoluteExpireTimeInMinutes = null,
                                                   TimeSpan? slidingExpireTimeInMinutes = null, CancellationToken cancellationToken = default);
        Task UpdateAsync(string key, object data, bool requiresTimeReset = true, TimeSpan? absoluteExpireTimeInMinutes = null, TimeSpan? slidingExpireTimeInMinutes = null, CancellationToken cancellationToken = default);
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}