using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Z01.Models
{
    public static class Utilities {
        public static Note[] GetNotes(int page) {
            DirectoryInfo dir = new DirectoryInfo(Constants.NOTES_FOLDER);
            FileInfo[] files = dir.GetFiles();
            List<Note> notes = new List<Note>();
            for (int i = (page - 1) * Constants.NOTES_PER_PAGE; i < page * Constants.NOTES_PER_PAGE && i < files.Length; i++) {
                FileInfo file = files[i];
                string extension = file.Extension.Substring(1); // removes the initial dot
                Note note = GetNote(Path.GetFileNameWithoutExtension(file.Name), extension);
                note.Date = file.CreationTime;

                notes.Add(note);
            }            
            return notes.ToArray();
        }

        public static Note[] GetFilteredNotes(int page, DateTime from, DateTime to, string category) {
            DirectoryInfo dir = new DirectoryInfo("notes");
            FileInfo[] files = dir.GetFiles();
            List<Note> notes = new List<Note>();
            int extra = 0;
            for (int i = (page - 1) * Constants.NOTES_PER_PAGE; i < extra + page * Constants.NOTES_PER_PAGE && i < files.Length; i++) {
                FileInfo file = files[i];
                string extension = file.Extension.Substring(1); // removes the initial dot
                Note note = GetNote(Path.GetFileNameWithoutExtension(file.Name), extension);
                note.Date = file.CreationTime;

                bool hasCategory = Array.Exists(note.CategoriesList, element => element == category) || category == null;
                bool isFrom = DateTime.Compare(note.Date, from) > 0;
                bool isTo = DateTime.Compare(note.Date, to) < 0;
                if (hasCategory && isFrom && isTo) {            
                    notes.Add(note);
                } else {
                    extra++;
                }
            }            
            return notes.ToArray();
        }

        public static Note GetNote(string title, string extension) {
            Note note = new Note();
            string path = BuildFullFilePath(Constants.NOTES_FOLDER, title, extension);

            note.Title = title;
            note.IsMarkdown = extension == "md";
            note.CategoriesList = ParseCategories(path);

            return note;
        }

        public static void DeleteNote(string title, bool isMarkdown) {            
            string convertedType = ConvertToExtenison(isMarkdown);
            string path = BuildFullFilePath(Constants.NOTES_FOLDER, title, convertedType);
            if (System.IO.File.Exists(path)) {
                try {
                    System.IO.File.Delete(path);
                } catch (System.IO.IOException e) {
                    Console.WriteLine(e.Message);
                } 
            }            
        }

        public static bool CategoryExists(string categoryName, string filePath) {
            List<string> categories = GetAllCategories(filePath);
            for (int i = 0; i < categories.Count; i++) {
                if (categories[i] == categoryName) {
                    return true;
                }
            }
            return false;
        }

        public static List<string> GetAllCategories(string filePath) {
            List<string> categories = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);  
            string line;   
            while((line = file.ReadLine()) != null) { 
                categories.Add(line);
            }  
            file.Close();  
            return categories;
        }

        public static string FormatCategories() {
            string path = Constants.TEMP_CATEGORY_FILE;
            List<string> allCategories = GetAllCategories(path);
            string formatted = "";
            for (int i = 0; i < allCategories.Count - 1; i++) {
                formatted += allCategories[i] + ", ";
            }
            if (allCategories.Count > 0) {
                formatted += allCategories[allCategories.Count-1];
            }

            return formatted;
        }

        public static string[] ParseCategories(string path) {
            System.IO.StreamReader file = new System.IO.StreamReader(path);  
            string categoriesString = file.ReadLine(); 
            categoriesString = categoriesString.Remove(0, "categories: ".Length); 
            return categoriesString.Split(", ");
        }

        public static int GetMaxPages() {            
            int filesCount = GetNumberOfFiles();
            if (filesCount % Constants.NOTES_PER_PAGE == 0) {
                int count = filesCount / Constants.NOTES_PER_PAGE;
                return count > 0 ? count : 1;
            } else {
                return filesCount / Constants.NOTES_PER_PAGE + 1;
            }
        }

        public static void CreateTemporaryCategoryFile(string title, string extension) {
            string readPath = BuildFullFilePath(Constants.NOTES_FOLDER, title, extension);
            string[] categories = ParseCategories(readPath);
            string writePath = Constants.TEMP_CATEGORY_FILE;

            System.IO.File.AppendAllLines(writePath, categories); 

        }

        public static string[] GetAllFilesCategories() {
            DirectoryInfo dir = new DirectoryInfo("notes");
            FileInfo[] files = dir.GetFiles();
            List<string> categories = new List<string>();
            for (int i = 0; i < files.Length; i++) {
                FileInfo file = files[i];
                string extension = file.Extension.Substring(1); // removes the initial dot
                Note note = GetNote(Path.GetFileNameWithoutExtension(file.Name), extension);
                for (int j = 0; j < note.CategoriesList.Length; j++) {
                    bool alreadyExist = categories.Contains(note.CategoriesList[j]);
                    if (!alreadyExist) {
                        categories.Add(note.CategoriesList[j]);
                    }
                }
            }        
            return categories.ToArray();    
        }

        public static int GetNumberOfFiles() {
            DirectoryInfo dir = new DirectoryInfo("notes");
            FileInfo[] files = dir.GetFiles();
            return files.Length;
        }

        public static string BuildFullFilePath(string folder, string title, string type) {
            string name = title + "." + type;
            return Path.Combine(folder, name);
        }

        public static string ConvertToExtenison(bool isMarkdown) {
            return isMarkdown ? "md" : "txt";
        }
    }
}