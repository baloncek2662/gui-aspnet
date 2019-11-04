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
    public class HomeController : Controller
    {
        string NOTES_FOLDER = "notes";
        // GET: /  
        // GET: /Home/
        public IActionResult Index()
        {
            Note[] notes = Utilities.GetNotes();
            return View(notes);
        }

        // GET: /Home/Details/ 
        public IActionResult Details(string title)
        {
            Note note = Utilities.GetNote("title");
            return View(note);
        }

        // GET: /Home/New/ 
        public IActionResult New()
        {
            return View();
        }

        // GET: /Home/Delete/ 
        public IActionResult Delete(string title, string type)
        {
            string path = Utilities.BuildFullFilePath(NOTES_FOLDER, title, type);
            if (System.IO.File.Exists(path)) {
                try {
                    System.IO.File.Delete(path);
                } catch (System.IO.IOException e) {
                    Console.WriteLine(e.Message);
                    Note[] notesz = Utilities.GetNotes();
                    return View("Index", notesz);
                } 
            }            
            Note[] notes = Utilities.GetNotes();
            return RedirectToAction("Index");
        }

        // 2nd view methods     ??mogoce premakni v drug controller?
        public void AddNote(string title, string text, string type, string[] categories) {            
            if (!System.IO.Directory.Exists(NOTES_FOLDER)) {
                System.IO.Directory.CreateDirectory(NOTES_FOLDER);
            }
            string path = Utilities.BuildFullFilePath(NOTES_FOLDER, title, type);
            System.IO.File.WriteAllText(path, text);
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
