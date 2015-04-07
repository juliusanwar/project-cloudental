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

        public ActionResult ShowAllRole()
        {
            var roles = RoleManager.Roles;
            List<RoleViewModel> model = new List<RoleViewModel>();

            foreach(var role in roles)
            {
                model.Add(new RoleViewModel { RoleId = role.Id, RoleName = role.Name });
            }

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
                        msgbox.AlertClass = "alert alert-info";
                        msgbox.MessageBody = "Data Role " + role.RoleName + " berhasil ditambah !";
                    }
                    else
                    {
                        msgbox.AlertClass = "alert alert-warning";
                        msgbox.MessageBody = "Gagal menambahkan role !";
                    } 
                }
                else
                {
                    msgbox.AlertClass = "alert alert-warning";
                    msgbox.MessageBody = "Role yang anda inputkan sudah ada, masukan yang lain !";
                }
            }
            return PartialView("_MessageBox", msgbox);
        }


    }
}