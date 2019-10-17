using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Z01.Models;
using System.Text.Encodings.Web;

namespace Z01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Details/ 
        public IActionResult Details(int id, string title)
        {
            return HtmlEncoder.Default.Encode($"This is id {id}, title is: {id}");
            //ViewData["Message"] = "Your application description page.";

            //return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
