namespace Customer.Microservice.Application
{
    public static class Constants
    {
        public static class Configuration
        {
            public static class ApplicationSettings
            {
                public static class DatabaseConnections
                {
                    public static string Fructose = "Fructose";
                }
            }
        }

        public static class CacheKey
        {
            public static string CustomerStatuses = "CustomerStatuses";
            public static string CustomerTypes = "CustomerTypes";
        }
    }
}
