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
        // GET: / 
        // GET: /Home/ 
        public IActionResult Index()
        {
            Note[] notes = Utilities.GetNotes();
            return View(notes);
        }

        // GET: /Home/Details/ 
        public IActionResult Details(int id, string title)
        {
            return View();
        }

        // GET: /Home/New/ 
        public IActionResult New()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
