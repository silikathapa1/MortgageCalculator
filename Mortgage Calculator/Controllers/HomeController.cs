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
            return View();
        }

        [HttpPost]
        public ActionResult calculateAmortTable(CalculationView cv)
        {
            Utility.amortChart ac = new Utility.amortChart();
            List<AmortTable> listView;
           
            listView = ac.calcAmortTable(cv);
            
            foreach(AmortTable x in listView)
            {
                System.Diagnostics.Debug.WriteLine("new data " + x.interest);
            }
            // If we got this far, something failed, redisplay form
            return PartialView("AmortChart", listView);
        }

        
    }
}