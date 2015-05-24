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

namespace CloudClinic.Controllers
{
    [Authorize(Users = "jul@jul.com")]
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

        public ActionResult ShowAllRole()
        {
            var model = GetAllRoles();

            return View(model);

            
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