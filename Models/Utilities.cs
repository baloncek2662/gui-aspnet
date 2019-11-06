using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Z01.Models
{
    public static class Utilities {
        public static Note[] GetNotes() {
            DirectoryInfo dir = new DirectoryInfo("notes");
            FileInfo[] files = dir.GetFiles();
            Note[] notes = new Note[files.Length];
            for (int i = 0; i < files.Length; i++) {
                FileInfo file = files[i];
                Note note = new Note();

                if (Path.GetFileNameWithoutExtension(file.Name) == Constants.TEMP_CATEGORY_FILE) { // ignore the file used to temporarily store categories
                    continue;
                }
                
                string extension = file.Extension.Substring(1); // removes the initial dot
                note.IsMarkdown = extension == "md"; 
                note.Title = Path.GetFileNameWithoutExtension(file.Name); // removes extension from name
                note.Date = file.CreationTime;

                string[] tempCategories = new string[2];
                tempCategories[0]="fassa";
                tempCategories[1]="kjfjop";
                note.CategoriesList = tempCategories;

                notes[i] = note;
            }            
            return notes;
        }

        public static Note GetNote(string title) {
            return GetNotes()[0];
        }

        public static void DeleteNote(string title, bool isMarkdown) {            
            string convertedType = Utilities.ConvertToExtenison(isMarkdown);
            string path = Utilities.BuildFullFilePath(Constants.NOTES_FOLDER, title, convertedType);
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
            string line;   
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);  
            while((line = file.ReadLine()) != null) { 
                categories.Add(line);
            }  
            file.Close();  
            return categories;
        }

        public static string FormatCategories() {
            string path = Utilities.BuildFullFilePath(Constants.NOTES_FOLDER, Constants.TEMP_CATEGORY_FILE, Utilities.ConvertToExtenison(Constants.TEMP_CATEGORY_FILE_EXTENSION));
            List<string> allCategories = GetAllCategories(path);
            string formatted = "";
            for (int i = 0; i < allCategories.Count - 1; i++) {
                formatted += allCategories[i] + ", ";
            }
            formatted += allCategories[allCategories.Count-1];
            return formatted;
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