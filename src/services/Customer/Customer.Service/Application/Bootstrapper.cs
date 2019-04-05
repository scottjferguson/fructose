using Core.Application;
using Core.IoC;
using Core.Plugins.Application;
using Core.Plugins.Utilities;

namespace Customer.Microservice.Application
{
    public class Bootstrapper : IBootstrapper<ApplicationComposition, StartupResult>
    {
        public StartupResult Startup(ApplicationComposition input)
        {
            var assemblyScanner = new AssemblyScanner();
            var iocContainerBootstrapper = new IoCContainerBootstrapper(assemblyScanner);

            IIoCContainer iocContainer = iocContainerBootstrapper.Startup(input);
            
            return new StartupResult(true, null, null, iocContainer);
        }
    }
}
