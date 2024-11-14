using ecommerceTask.Data;
using ecommerceTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace ecommerceTask.Controllers
{
	public class UserController : Controller
	{

     
        public ActionResult Index()
		{

			return View();
		}

		[HttpGet]
		public ActionResult Create() {

			ViewBag.userRoleDrop = Enum.GetValues(typeof(User.UserRole)).Cast<User.UserRole>().Select(e => new SelectListItem
			{
				Text = e.ToString(),
				Value = ((int)e).ToString()
			}).ToList();

			return View();	
		}

        [HttpPost]
        public ActionResult Create(User model)
        {

			
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
		public ActionResult Delete(int id)
		{
			
			return RedirectToAction(nameof(Index));
		}

		


	}
}
