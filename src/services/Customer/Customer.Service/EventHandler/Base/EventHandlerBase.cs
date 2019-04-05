using Customer.Microservice.Application;

namespace Customer.Microservice.EventHandler.Base
{
    public class EventHandlerBase
    {
        public string FructoseRepository
        {
            get
            {
                return Constants.Configuration.ApplicationSettings.DatabaseConnections.Fructose;
            }
        }
    }
}
