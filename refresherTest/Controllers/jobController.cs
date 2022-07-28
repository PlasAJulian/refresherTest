using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace refresherTest.Controllers
{
    public class jobController : Controller
    {
        public IActionResult Index()
        //public string Index()
        {
            return View();
            //return "this is the default.";
        }

        public string pull()
        {
            return "i am pulling all the data from that datebase.";
        }
        public string add()
        {
            return "i am adding a single entry to the datebase.";
        }

    }
}
