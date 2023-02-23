using System;
using System.Threading;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace RateLimiter
{
    /// <summary>
    /// TimeLimiter implementation
    /// </summary>
    public class TimeLimiter
    {
        private readonly TokenBucketRateLimiter _tokenBucketRateLimiter;
        
        internal TimeLimiter(TokenBucketRateLimiter tokenBucketRateLimiter)
        {
            _tokenBucketRateLimiter = tokenBucketRateLimiter;
        }

        /// <summary>
        /// Perform the given task respecting the time constraint
        /// returning the result of given function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="perform"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> Enqueue<T>(Func<Task<T>> perform, CancellationToken cancellationToken)
        {
            using (RateLimitLease lease = await _tokenBucketRateLimiter.AcquireAsync(permitCount: 1, cancellationToken))
            {
                if (lease.IsAcquired)
                {
                    return await perform();
                }
                throw new Exception("Failed to acquire lease");
            }
        }

        /// <summary>
        /// Perform the given task respecting the time constraint
        /// returning the result of given function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="perform"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public T Enqueue<T>(Func<T> perform, CancellationToken cancellationToken)
        {
            using (RateLimitLease lease = _tokenBucketRateLimiter.AcquireAsync(permitCount: 1, cancellationToken).Result)
            {
                if (lease.IsAcquired)
                {
                    return perform();
                }
                throw new Exception("Failed to acquire lease");
            }
        }

        /// <summary>
        /// Returns a TimeLimiter based on a maximum number of times
        /// during a given period
        /// </summary>
        /// <param name="tokenLimit"></param>
        /// <param name="replenishmentPeriod"></param>
        /// <returns></returns>
        public static TimeLimiter GetFromMaxCountByInterval(int tokenLimit, TimeSpan replenishmentPeriod)
        {
            return new TimeLimiter(
                new TokenBucketRateLimiter(
                    new TokenBucketRateLimiterOptions()
                    {
                        TokenLimit = tokenLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 1,
                        ReplenishmentPeriod = replenishmentPeriod,
                        TokensPerPeriod = 1,
                        AutoReplenishment = true
                    }));
        }
    }
}
