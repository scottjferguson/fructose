namespace Customer.Infrastructure.Repository.Base
{
    public class RepositoryBase
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
