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
;
            var usersFinded = await UserService.GetUsersShort();
            var users = MapUsers(usersFinded);

            return View(users.OrderBy(o => o.FirstName).ToList());
        }

        public async Task<ActionResult> FindUsersAjax(string search)
        {
            var usersFinded = await UserService.GetUsersShort(search);
            var users = MapUsers(usersFinded);           

            return PartialView("_UsersTable", users);
        }

        public async Task<ActionResult> SortUsersAjax(string sort)
        {
            ViewBag.FirstName = sort == "FirstName" ? "FirstName_DSC" : "FirstName";
            ViewBag.LastName = sort == "LastName" ? "LastName_DSC" : "LastName";
            ViewBag.Email = sort == "Email" ? "Email_DSC" : "Email";
            ViewBag.PhoneNumber = sort == "PhoneNumber" ? "PhoneNumber_DSC" : "PhoneNumber";         
         
            var usersFinded = await UserService.GetUsersShort("", sort);
            var users = MapUsers(usersFinded);

            return PartialView("_UsersTable", users);
        }

        private List<DetailModel> MapUsers(List<UserShortDTO> users)
        {
            var sortUsers = new List<DetailModel>();
            foreach (var user in users)
            {
                sortUsers.Add(new DetailModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    AboutMe = user.AboutMe
                });
            }
            return sortUsers;
        }

    }
}