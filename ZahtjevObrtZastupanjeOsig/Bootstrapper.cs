using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace ZahtjevObrtZastupanjeOsig
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IEmailConfiguration, EmailConfiguration>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailService, EmailService>(new TransientLifetimeManager());

            return container;
        }
    }
}