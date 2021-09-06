using System;
using System.Net;
using System.Net.Http;
using Common;
using Polly;

namespace TvMaze.Scraper
{
    public static class RetryPolicyProvider
    {
        private static readonly Random _jitterer = new Random();

        public static IAsyncPolicy<HttpResponseMessage> Get()
        {
            var policy = Policy
                .HandleResult<HttpResponseMessage>(ShouldRetryRequest)
                .WaitAndRetryAsync(Constants.POLICY_RETRY_COUNT, CalculateDelay);

            return policy;
        }

        private static bool ShouldRetryRequest(HttpResponseMessage response)
        {           
            return response.StatusCode == HttpStatusCode.TooManyRequests;
        }

        private static TimeSpan CalculateDelay(int retryAttempt)
        {
            TimeSpan exponentialBackOff = TimeSpan.FromSeconds(Constants.POLICY_INITIAL_DELAY_IN_SECONDS * Math.Pow(2, retryAttempt - 1));
            TimeSpan jitter = TimeSpan.FromMilliseconds(_jitterer.Next(0, Constants.POLICY_JITTER_IN_MILLISECONDS));

            return exponentialBackOff + jitter;
        }
    }
}
