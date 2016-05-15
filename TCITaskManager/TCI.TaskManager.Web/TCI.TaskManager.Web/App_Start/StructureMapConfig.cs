using Heroic.Web.IoC;
using System.Web.Http;
using System.Web.Mvc;
using StructureMap;
using StructureMap.Graph;
using TCI.TaskManager.Web.Core.Models;
using TCI.TaskManager.Web.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using System.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TCI.TaskManager.Web.StructureMapConfig), "Configure")]
namespace TCI.TaskManager.Web
{
	public static class StructureMapConfig
	{
		public static void Configure()
		{
			IoC.Container.Configure(cfg =>
			{
				cfg.Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.WithDefaultConventions();
				});

				cfg.AddRegistry(new ControllerRegistry());
				cfg.AddRegistry(new MvcRegistry());
				cfg.AddRegistry(new ActionFilterRegistry(namespacePrefix: "TCI.TaskManager.Web"));

				//Are you using ASP.NET Identity?  If so, you'll probably need to configure some additional services:
				
				//1) Make IUserStore injectable.  Replace 'ApplicationUser' with whatever your Identity user type is.
				cfg.For<IUserStore<User>>().Use<UserStore<User>>();
				
				//2) Change AppDbContext to your application's Entity Framework context.
				cfg.For<System.Data.Entity.DbContext>().Use<AppDbContext>();
				
				//3) This will allow you to inject the IAuthenticationManager.  You may not need this, but you will if you 
				//   used the default ASP.NET MVC project template as a starting point!
				cfg.For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);

				//TODO: Add other registries and configure your container (if needed)
			});

			var resolver = new StructureMapDependencyResolver();
			DependencyResolver.SetResolver(resolver);
			GlobalConfiguration.Configuration.DependencyResolver = resolver;
		}
	}
}