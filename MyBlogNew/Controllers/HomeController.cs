using BlogBusinessLogic;
using BlogModel;
using MyBlogNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewClassLib;

namespace MyBlogNew.Controllers
{
    public class HomeController : Controller
    {
        private IDBmanager manager;
        public HomeController(IDBmanager blogManager)//传入有参构造函数
        {
            manager = blogManager;
        }
        public ActionResult Index()
        {
            ViewForIndex viewForIndex = manager.GetIndexModel();
            return View(viewForIndex);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}