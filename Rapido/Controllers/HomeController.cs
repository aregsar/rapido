using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rapido.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            //throw new Exception();
            return View();
        }

       
    }
}
