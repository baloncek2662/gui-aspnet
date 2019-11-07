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
        public IActionResult Index(int page) {
            Utilities.DeleteNote("16wnvTJUtjn7vU0OAUYl", false); //deletes previously saved categories

            IndexView model = new IndexView();
            model.MaxPages = Utilities.GetMaxPages();
            if (page == 0) {
                model.Page = 1;
            } else if (page > model.MaxPages) {
                model.Page = model.MaxPages;
            } else {
                model.Page = page;
            }
            model.Notes = Utilities.GetNotes(model.Page);

            return View(model);
        }
        
        // GET: /Home/Delete/ 
        public IActionResult Delete(string title, bool isMarkdown) {
            Utilities.DeleteNote(title, isMarkdown);
            return RedirectToAction("Index");
        }
 
        // GET: /Home/New/ 
        public IActionResult New(string title) {
            Note note = null;
            if (title != null) {
                note = Utilities.GetNote(title, "txt");
                Utilities.CreateTemporaryCategoryFile(title, "txt");
            }
            return View(note);
        }

        // POST: /Home/New/ 
        [HttpPost]
        public IActionResult New(Note note, string actionType, string title) {
            if (actionType == "Save") {
                note.Save();
            } else if (actionType == "Add") {
                note.AddCategory(note.CategoryAction);
                return View(note);
            } else if (actionType == "Remove") {
                note.RemoveCategory(note.CategoryAction);
                return View(note);                
            } 

            return RedirectToAction("Index");
        } 
        
        // Display error message
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
