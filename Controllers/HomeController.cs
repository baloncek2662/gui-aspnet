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
        public IActionResult Index(IndexView input, int page, string actionType) {
            if (!Directory.Exists(Constants.NOTES_FOLDER)) {
                Directory.CreateDirectory(Constants.NOTES_FOLDER);                
            }
            Utilities.DeleteNote("16wnvTJUtjn7vU0OAUYl", false); //deletes previously saved categories

            IndexView model = new IndexView();

            model.AllCategories = Utilities.GetAllFilesCategories();

            model.MaxPages = Utilities.GetMaxPages();
            if (page == 0) {
                model.Page = 1;
            } else if (page > model.MaxPages) {
                model.Page = model.MaxPages;
            } else {
                model.Page = page;
            }
            if (actionType == "Filter" && input != null) {
                model.Notes = Utilities.GetFilteredNotes(model.Page, input.From, input.To, input.Category);
            } else {
                model.Notes = Utilities.GetNotes(model.Page);
            }

            return View(model);
        }
        
        // GET: /Home/Delete/ 
        public IActionResult Delete(string title, bool isMarkdown) {
            Utilities.DeleteNote(title, isMarkdown);
            return RedirectToAction("Index");
        }
 
        // GET: /Home/New/ 
        public IActionResult New(string title) {
            System.IO.File.AppendAllText(Constants.TEMP_CATEGORY_FILE, ""); //creates file if it doesnt exist
            Note note = null;
            if (title != null) {
                string txtPath = Utilities.BuildFullFilePath(Constants.NOTES_FOLDER, title, "txt");
                bool isTextFile = System.IO.File.Exists(txtPath);
                if (isTextFile) {
                    note = Utilities.GetNote(title, "txt");
                } else {
                    note = Utilities.GetNote(title, "md");
                }
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
