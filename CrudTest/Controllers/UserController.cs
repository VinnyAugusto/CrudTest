using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudTest.BLL;
using CrudTest.TO;

namespace CrudTest.UI.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList()
        {
            UserBLL userBLL = new UserBLL();

            var ret = userBLL.GetAll();

            if (ret.Key)
                return View(ret.Value.Value);
            else
                throw new Exception(ret.Value.Key);

        }

        [HttpGet]
        public ActionResult GetById(long pId)
        {
            UserBLL userBLL = new UserBLL();

            var ret = userBLL.GetById(pId);

            if (ret.Key)
                return Json(ret.Value.Value, JsonRequestBehavior.AllowGet);
            else
                throw new Exception(ret.Value.Key);

        }

        [HttpPost]
        public ActionResult Save(UserTO userTO)
        {
            UserBLL userBLL = new UserBLL();

            var ret = userBLL.Save(userTO);

            if (ret.Key)
                return Json(new { Message = ret.Value });
            else
                throw new Exception(ret.Value);

        }

        [HttpPost]
        public ActionResult Update(UserTO userTO)
        {
            UserBLL userBLL = new UserBLL();

            var ret = userBLL.Update(userTO);

            if (ret.Key)
                return Json(new { Message = ret.Value });
            else
                throw new Exception(ret.Value);

        }

        [HttpPost]
        public ActionResult DeleteById(long pId)
        {
            UserBLL userBLL = new UserBLL();

            var ret = userBLL.DeleteById(pId);

            if (ret.Key)
                return Json(new { Message = ret.Value });
            else
                throw new Exception(ret.Value);

        }
    }
}