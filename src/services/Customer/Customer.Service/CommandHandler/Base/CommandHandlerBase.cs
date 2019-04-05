using Customer.Microservice.Application;

namespace Customer.Microservice.CommandHandler.Base
{
    public class CommandHandlerBase
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
