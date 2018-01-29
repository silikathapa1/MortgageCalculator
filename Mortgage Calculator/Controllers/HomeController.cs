using Mortgage_Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Mortgage_Calculator.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.AmortCharts = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(CalculationView cv)
        {
            Utility.amortChart ac = new Utility.amortChart();

            List<AmortTable> listView;
           
            listView = ac.calcAmortTable(cv);
            
                       ViewBag.AmortCharts = listView;
                // If something failed, redisplay form
            return View();
        }

        
    }
}