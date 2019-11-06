using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Z01.Models;
using System.Text.Encodings.Web;

namespace Z01.Controllers
{
    public class HomeController : Controller {
        // GET: /  
        // GET: /Home/
        public IActionResult Index() {
            Utilities.DeleteNote(Constants.TEMP_CATEGORY_FILE, Constants.TEMP_CATEGORY_FILE_EXTENSION); //deletes previously saved categories
            Note[] notes = Utilities.GetNotes();
            return View(notes);
        }

        // GET: /Home/Details/ 
        public IActionResult Details(string title) {
            Note note = Utilities.GetNote(title);
            return View("New", note);
        }
        
        // GET: /Home/Delete/ 
        public IActionResult Delete(string title, bool isMarkdown) {
            Utilities.DeleteNote(title, isMarkdown);
            return RedirectToAction("Index");
        }

        // "New" view methods    
        // GET: /Home/New/ 
        public IActionResult New() {
            return View();
        }

        // POST: /Home/New/ ...on save button click, redirects to index
        [HttpPost]
        public IActionResult New(Note note, string actionType) {
            if (actionType == "Save") {
                note.Save();
            } else if (actionType == "Add") {
                note.AddCategory(note.CategoryAction);
                return View();
            } else if (actionType == "Remove") {
                return View();                
            } 

            return RedirectToAction("Index");
        } 
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
