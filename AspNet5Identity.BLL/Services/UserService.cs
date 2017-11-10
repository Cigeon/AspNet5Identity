using AspNet5Identity.BLL.DTO;
using AspNet5Identity.BLL.Inftastructure;
using AspNet5Identity.BLL.Interfaces;
using AspNet5Identity.DAL.Entities;
using AspNet5Identity.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace AspNet5Identity.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = userDto.Email,
                    UserName = userDto.Email,
                    PhoneNumber = userDto.PhoneNumber
                };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);

                ClientProfile clientProfile = new ClientProfile
                {
                    Id = user.Id,
                    AboutMe = userDto.AboutMe,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName
                };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration succedeed", "");
            }
            else
            {
                return new OperationDetails(false, "User with the same login is have been already created", "Email");
            }
        }

        public async Task<UserShortDTO> GetUserShortByEmail(string email)
        {
            ApplicationUser appUser = await Database.UserManager.FindByEmailAsync(email);
            var user = new UserShortDTO
            {

                Id = appUser.Id,
                Email = appUser.Email,
                FirstName = appUser.ClientProfile.FirstName,
                LastName = appUser.ClientProfile.LastName,
                PhoneNumber = appUser.PhoneNumber,
                AboutMe = appUser.ClientProfile.AboutMe
            };
            return user;

        }

        public async Task<List<UserShortDTO>> GetUsersShort(string search = "", string sort = "" )
        {
            return await Task.Run(() =>
            {
                var appUsers = Database.UserManager.Users
                    .Where(u => string.IsNullOrEmpty(search) 
                        || (u.ClientProfile.FirstName.Contains(search)) 
                        || (u.ClientProfile.LastName.Contains(search)) 
                        || (u.Email.Contains(search)) 
                        || (u.PhoneNumber.Contains(search)))
                    .ToList();

                var users = new List<UserShortDTO>();
                foreach (var user in appUsers)
                {
                    users.Add(new UserShortDTO
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.ClientProfile.FirstName,
                        LastName = user.ClientProfile.LastName,
                        PhoneNumber = user.PhoneNumber,
                        AboutMe = user.ClientProfile.AboutMe
                    });
                }

                var sortUsers = new List<UserShortDTO>();
                switch (sort)
                {
                    case "FirstName":
                        sortUsers = users.OrderBy(o => o.FirstName).ToList();
                        break;
                    case "FirstName_DSC":
                        sortUsers = users.OrderByDescending(o => o.FirstName).ToList();
                        break;
                    case "LastName":
                        sortUsers = users.OrderBy(o => o.LastName).ToList();
                        break;
                    case "LastName_DSC":
                        sortUsers = users.OrderByDescending(o => o.LastName).ToList();
                        break;
                    case "Email":
                        sortUsers = users.OrderBy(o => o.Email).ToList();
                        break;
                    case "Email_DSC":
                        sortUsers = users.OrderByDescending(o => o.Email).ToList();
                        break;
                    case "PhoneNumber":
                        sortUsers = users.OrderBy(o => o.PhoneNumber).ToList();
                        break;
                    case "PhoneNumber_DSC":
                        sortUsers = users.OrderByDescending(o => o.PhoneNumber).ToList();
                        break;
                    default:
                        sortUsers = users.OrderBy(o => o.FirstName).ToList();
                        break;
                }
                return sortUsers;
            });
        }
        
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
