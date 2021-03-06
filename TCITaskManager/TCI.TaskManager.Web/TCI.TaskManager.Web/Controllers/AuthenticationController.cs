﻿using System.Threading.Tasks;
using System.Web.Mvc;
using TCI.TaskManager.Web.Identity;
using TCI.TaskManager.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;


namespace TCI.TaskManager.Web.Controllers
{
	[AllowAnonymous]
	public class AuthenticationController : TCITaskManagerControllerBase
    {
		private readonly ApplicationUserManager _userManager;
		private readonly IAuthenticationManager _authManager;

		public AuthenticationController(ApplicationUserManager userManager, IAuthenticationManager authManager)
		{
			_userManager = userManager;
			_authManager = authManager;
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Login(LoginForm form)
		{
			var user = await _userManager.FindByEmailAsync(form.EmailAddress);

			if (user == null || ! (await _userManager.CheckPasswordAsync(user, form.Password)))
			{
				Response.StatusCode = 400;
				return Json("The username or password is invalid.");
			}


			var identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

			_authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

			return Json(true);
		}

		public ActionResult Logout()
		{
			_authManager.SignOut();

			return RedirectToAction("Index","Home");
		}
	}
}