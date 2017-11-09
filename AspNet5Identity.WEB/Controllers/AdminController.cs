using AspNet5Identity.BLL.Interfaces;
using AspNet5Identity.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

            var users = new List<DetailModel>();
            var usersFinded = await UserService.GetUsersShort();
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

            return View(users.OrderBy(o => o.FirstName).ToList());
        }

        public async Task<ActionResult> UsersAjax(string sort)
        {
            ViewBag.FirstName = sort == "FirstName" ? "FirstName_DSC" : "FirstName";
            ViewBag.LastName = sort == "LastName" ? "LastName_DSC" : "LastName";
            ViewBag.Email = sort == "Email" ? "Email_DSC" : "Email";
            ViewBag.PhoneNumber = sort == "PhoneNumber" ? "PhoneNumber_DSC" : "PhoneNumber";

            var users = new List<DetailModel>();
            var usersFinded = await UserService.GetUsersShort();
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

            var usersSorted = new List<DetailModel>();
            switch (sort)
            {
                case "FirstName":
                    usersSorted = users.OrderBy(o => o.FirstName).ToList();
                    break;
                case "FirstName_DSC":
                    usersSorted = users.OrderByDescending(o => o.FirstName).ToList();
                    break;
                case "LastName":
                    usersSorted = users.OrderBy(o => o.LastName).ToList();
                    break;
                case "LastName_DSC":
                    usersSorted = users.OrderByDescending(o => o.LastName).ToList();
                    break;
                case "Email":
                    usersSorted = users.OrderBy(o => o.Email).ToList();
                    break;
                case "Email_DSC":
                    usersSorted = users.OrderByDescending(o => o.Email).ToList();
                    break;
                case "PhoneNumber":
                    usersSorted = users.OrderBy(o => o.PhoneNumber).ToList();
                    break;
                case "PhoneNumber_DSC":
                    usersSorted = users.OrderByDescending(o => o.PhoneNumber).ToList();
                    break;
                default:
                    usersSorted = users.OrderBy(o => o.FirstName).ToList();
                    break;
            }

            return PartialView("_UsersTable", usersSorted);
        }

    }
}