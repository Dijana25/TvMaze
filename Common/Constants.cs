using System;

namespace Common
{
    public static class Constants
    {
        public static string BASE_URL = "http://api.tvmaze.com";
        public static int POLICY_RETRY_COUNT = 5;
        public static float POLICY_INITIAL_DELAY_IN_SECONDS = 1.5f;
        public static int POLICY_JITTER_IN_MILLISECONDS = 500;
    }
}
