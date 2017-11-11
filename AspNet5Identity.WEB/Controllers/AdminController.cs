using AspNet5Identity.BLL.DTO;
using AspNet5Identity.BLL.Interfaces;
using AspNet5Identity.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspNet5Identity.WEB.Controllers
{
    public class AdminController : Controller
    {
        private IUserService UserService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IUserService>(); }
        }

        [Authorize(Roles="admin")]
        public async Task<ActionResult> Users(string sort)
        {
            ViewBag.FirstName = "FirstName_DSC";
            ViewBag.LastName = "LastName_DSC";
            ViewBag.Email = "Email_DSC";
            ViewBag.PhoneNumber = "PhoneNumber_DSC";

            return View(await GetUsersAsync());
        }

        public async Task<ActionResult> GetUsersAjax(string search, string sort)
        {
            ViewBag.FirstName = sort == "FirstName_DSC" ? "FirstName" : "FirstName_DSC";
            ViewBag.LastName = sort == "LastName_DSC" ? "LastName" : "LastName_DSC";
            ViewBag.Email = sort == "Email_DSC" ? "Email" : "Email_DSC";
            ViewBag.PhoneNumber = sort == "PhoneNumber_DSC" ? "PhoneNumber" : "PhoneNumber_DSC";         

            return PartialView("_UsersTable", await GetUsersAsync(search, sort));
        }

        private async Task<UsersModel> GetUsersAsync(string search = "", string sort = "")
        {
            var users = new List<DetailModel>();
            var usersFinded = await UserService.GetUsersShort(search, sort);          
            foreach (var user in usersFinded)
            {
                users.Add(new DetailModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    AboutMe = user.AboutMe
                });
            }

            return new UsersModel
            {
                Search = search,
                Users = users
            };
        }
    }
}