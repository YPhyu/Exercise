using System.Data.Entity;
using System.Web;
using TCI.TaskManager.Web.Core.Models;
using TCI.TaskManager.Web.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using StructureMap;

namespace TCI.TaskManager.Web.Identity
{
    public class AspNetIdentityRegistry : Registry
    {
        public AspNetIdentityRegistry()
        {
            For<IUserStore<User>>().Use<UserStore<User>>();
            For<DbContext>().Use(() => new AppDbContext());
            For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);           
        }
    }
}