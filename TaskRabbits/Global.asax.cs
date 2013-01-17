﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Logging;
using Common.Logging.Simple;
using Highway.Data;
using Highway.Data.Interfaces;
using TaskRabbits.DataAccess;

namespace TaskRabbits
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            WireUpIoC();
            

        }

        private void WireUpIoC()
        {
            //This is the bootstrap for the castle Highway.Data implementation. 
            //You IoC implementation will have a similar class, just change the namespace and
            //let intellisense be your guide
            var bootstrap = new Highway.Data.EntityFramework.Castle.CastleBootstrap();

            var container = new WindsorContainer();
            bootstrap.Install(container, null);

            //Wiring in our types
            container.Register(
                //This is Highway.Data's Context
                Component.For<IDataContext>().ImplementedBy<DataContext>()
                .DependsOn(Dependency.OnConfigValue("connectionString", 
                ConfigurationManager.ConnectionStrings["TaskRabbits"].ConnectionString)),
                //This is Highway.Data's Repository
                Component.For<IRepository>().ImplementedBy<Repository>(),
                //This is Highway.Data's relational mappings Interface, but YOUR implementation
                Component.For<IMappingConfiguration>().ImplementedBy<TaskRabbitMappings>(),
                //This is Common.Loggings log interface, feel free to supply anything that uses it *cough* log4net
                Component.For<ILog>().ImplementedBy<NoOpLogger>(),
                //This is Highway.Data's context configuration, by default use the default :-)
                Component.For<IContextConfiguration>().ImplementedBy<DefaultContextConfiguration>());


            //TODO: Wire up your types here

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            Database.SetInitializer(new TestDataSeed());
        }
    }

    public class WindsorControllerFactory : DefaultControllerFactory
    {
        readonly IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;

            var controllerTypes = Assembly.GetExecutingAssembly()
                .GetTypes().Where(t => typeof(IController).IsAssignableFrom(t));

            foreach (Type t in controllerTypes)
            {
                _container.Register(Component.For(t).Named(t.FullName).LifestyleTransient());
            }


        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return null;
            return (IController)_container.Resolve(controllerType);
        }
    }
}