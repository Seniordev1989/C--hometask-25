using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
   
    public class UserDetailsController : Controller
    {
        private UserDetailsService _service=new UserDetailsService();

        // GET: UserDetails
        public ActionResult Index()
        {
            return View("ViewUserDetails");
        }
        public ActionResult Upload()
        {
            return View("ViewUploadUserDetails");
        }
        public ActionResult Edit(int Id)
        {
            ViewBag.Id = Id;
            return View("EditUserDetails");
        }
        [HttpPost]
        public ActionResult UploadCsv(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
             var result=   _service.UploadCsv(file);
                return Json(result);
            }
            return Json(new {status=false,message="Upload unsuccessfull"});
        }

        //// GET: UserDetails/Details/5
        [HttpGet]
        public ActionResult GetAllUserDetails()
        {
            var result = _service.GetAllUserDetails();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetById(int Id)
        {
            var result = _service.GetById(Id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public ActionResult Update(UserDetails usrdata)
        {
            var result = _service.Update(usrdata);
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var result = _service.Delete(Id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
