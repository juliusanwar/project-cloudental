using CloudClinic.Models;
using CloudClinic.Models.DataModel;
using CloudClinic.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudClinic.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult UserDetails()
        {
            Pasien model = new Pasien();
            return View(model);
        }

        public JsonResult PopulateDetails(Pasien model)
        {
            UserResultModel userResultModel = new UserResultModel();
            if (String.IsNullOrEmpty(model.UserName))
            {
                userResultModel.Message = "UserId can not be blank";
                return Json(userResultModel);
            }

            Pasien user = _userRepository.GetUser(model.UserName);

            if (user == null)
            {
                userResultModel.Message = String.Format("No UserId found for {0}", model.UserName);
                return Json(userResultModel);
            }
            userResultModel.Nama = user.Nama;
            userResultModel.Gender = user.Gender;
            userResultModel.Message = String.Empty; //success message is empty in this case

            return Json(userResultModel);
        }
    }

    public class UserResultModel
    {
        public string Nama { get; set; }
        public string Gender { get; set; }
        public string Message { get; set; }
    }
}