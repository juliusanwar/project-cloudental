using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;

using CloudClinic.Models.ViewModel;
using CloudClinic.Models;
using System.Text;

namespace CloudClinic.Controllers
{
    [Authorize(Roles = "Admin,Dokter")]
    public class ManageUserController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        


        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: ManageUser
        public ActionResult Index()
        {
            return View();
        }

        private IEnumerable<UserViewModel> GetAllUsers()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (var user in UserManager.Users)
            {
                users.Add(new UserViewModel
                {
                    UserName = user.Email,
                    Email = user.Email,
                    UserId = user.Id
                });
            }
            return users;
        }

        private IEnumerable<RoleViewModel> GetAllRoles()
        {
            var roles = RoleManager.Roles;
            List<RoleViewModel> model = new List<RoleViewModel>();

            foreach (var role in roles)
            {
                model.Add(new RoleViewModel { RoleId = role.Id, RoleName = role.Name });
            }

            return model;
        }

        private IEnumerable<UserWithRoleViewModel> GetAllUserInRole()
        {
            List<UserWithRoleViewModel> lstUser = new List<UserWithRoleViewModel>();


            foreach (var user in UserManager.Users.ToList())
            {
                StringBuilder sb = new StringBuilder();
                foreach (var role in user.Roles.ToList())
                {
                    sb.Append(RoleManager.FindById(role.RoleId).Name + " ");
                }
                var newUser = new UserWithRoleViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = sb.ToString()
                };
                lstUser.Add(newUser);
            }

            return lstUser;
        }

        public ActionResult ShowUserWithRole()
        {
            var model = GetAllUserInRole();

            return View(model);
        }


        public ActionResult ShowAllRole()
        {
            var model = GetAllRoles();

            return View(model);   
        }

        public ActionResult ShowAllUser()
        {
            var model = GetAllUsers();

            return View(model);

        }

         

        //public IdentityResult userInRole(UserInRoleViewModel userInRole)
        //{
        //    var roleForUser = UserManager.GetRoles(userInRole.UserId);
        //    IdentityResult result = null;
        //    if (!roleForUser.Contains(userInRole.RoleId))
        //    {
        //        result = UserManager.AddToRole(userInRole.UserId,
        //            RoleManager.FindById(userInRole.RoleId).Name);
        //    }

        //    return result;
        //}

        public ActionResult AddUserToRole()
        {
            var users = GetAllUsers();
            var roles = GetAllRoles();
            ViewBag.UserId = new SelectList(users, "UserId", "Email");
            ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult AddUserToRole(UserInRoleViewModel userInRole)
        {
            using (UserInRoleService userinRoleS = new UserInRoleService(UserManager, RoleManager))
            {
                var users = GetAllUsers();
                var roles = GetAllRoles();

                var result = userinRoleS.AddUserToRole(userInRole);
                if (result.Succeeded)
                {
                    ViewBag.Pesan = "Berhasil menambahkan " + UserManager.FindById(userInRole.UserId).Email +
                        " kedalam role " + RoleManager.FindById(userInRole.RoleId).Name;
                }
                else
                {
                    ViewBag.Pesan = "Gagal menambah role !";
                }

                ViewBag.UserId = new SelectList(users, "UserId", "UserName");
                ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName");
            }

            return View(userInRole);

        }

        

        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(RoleViewModel role)
        {
            MessageBoxViewModel msgbox = new MessageBoxViewModel();
           
            if(ModelState.IsValid)
            {
                var newRole = RoleManager.FindByName(role.RoleName);
                if(newRole==null)
                {
                    newRole = new IdentityRole(role.RoleName);
                    var result = RoleManager.Create(newRole);
                    if (result.Succeeded)
                    {
                        msgbox.TitleMessage = "Info";
                        msgbox.AlertClass = "alert alert-info";
                        msgbox.MessageBody = "Data Role " + role.RoleName + " berhasil ditambah !";
                    }
                    else
                    {
                        msgbox.TitleMessage = "Error!";
                        msgbox.AlertClass = "alert alert-error";
                        msgbox.MessageBody = "Gagal menambahkan role !";
                    } 
                }
                else
                {
                    msgbox.TitleMessage = "Warning!";
                    msgbox.AlertClass = "alert alert-warning";
                    msgbox.MessageBody = "Role "+ role.RoleName +" sudah ada, masukan yang lain !";
                }
            }
            return PartialView("_MessageBox", msgbox);

            
        }

        public ActionResult UpdateRole(string id)
        {
            RoleViewModel model = new RoleViewModel();
            var role = RoleManager.FindById(id);
            if(role!=null)
            {
                model.RoleId = role.Id;
                model.RoleName = role.Name;
                return View(model);
            }
            else
            {
                return RedirectToAction("ShowAllRole");
            }
            
        }

        [HttpPost]
        public ActionResult UpdateRole(RoleViewModel model)
        {
            MessageBoxViewModel msgbox = new MessageBoxViewModel();

            if (ModelState.IsValid)
            {
                var role = RoleManager.FindById(model.RoleId);
                if (role != null)
                {
                    role.Name = model.RoleName;
                    var result = RoleManager.Update(role);
                    if (result.Succeeded)
                    {
                        msgbox.TitleMessage = "info";
                        msgbox.MessageBody = "Role " + model.RoleName + " sudah berhasil diupdate !";
                        msgbox.AlertClass = "alert alert-info";
                    }
                    else
                    {
                        msgbox.TitleMessage = "Error!";
                        msgbox.MessageBody = "Gagal mengupdate role " + model.RoleName;
                        msgbox.AlertClass = "alert alert-error";
                    }
                } 
            }
            return PartialView("_MessageBox", msgbox);
        }

        public ActionResult DeleteRole(string id)
        {
            var role = RoleManager.FindById(id);
            if(role!=null)
            {
                var result = RoleManager.Delete(role);
               
                ViewBag.TitleMessage = "Konfirmasi";
                ViewBag.MessageBody = "Data role " + role.Name + " berhasil di delete";
                ViewBag.AlertClass = "alert alert-info";
            }

            IEnumerable<RoleViewModel> model = GetAllRoles();
            return PartialView("_PartialListRole", model);
        }


    }
}