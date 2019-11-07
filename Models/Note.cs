using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace Z01.Models
{
    public class Note
    {
        [DisplayName("Title:")]
        public string Title { get; set; }
        [DisplayName("Content:")]
        public string Content { get; set; }
        [DisplayName("Categories:")]
        public string[] CategoriesList { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Markdown:")]
        public bool IsMarkdown { get; set; }
        [DisplayName("Category name:")]
        public string CategoryAction { get; set; }
        public int CurrentPage { get; set; }

        public void Save() {            
            if (!System.IO.Directory.Exists(Constants.NOTES_FOLDER)) {
                System.IO.Directory.CreateDirectory(Constants.NOTES_FOLDER);
            }
            string[] formattedContent = FormatContent();

            string path = Utilities.BuildFullFilePath(Constants.NOTES_FOLDER, this.Title, Utilities.ConvertToExtenison(this.IsMarkdown));
            string alternatePath = Utilities.BuildFullFilePath(Constants.NOTES_FOLDER, this.Title, Utilities.ConvertToExtenison(!this.IsMarkdown));
            if (File.Exists(alternatePath)) {
                Utilities.DeleteNote(this.Title, !this.IsMarkdown);
            }
            System.IO.File.WriteAllLines(path, formattedContent); 
        }

        public string[] FormatContent() {
            string[] formattedContent = new string[3];
            formattedContent[0] = "categories: " + Utilities.FormatCategories();
            formattedContent[1] = "date: " + DateTime.Now;
            formattedContent[2] = this.Content;
            return formattedContent;
        }

        public void AddCategory(string categoryName) {
            string path = Constants.TEMP_CATEGORY_FILE;
            System.IO.File.AppendAllText(path, ""); //creates file if it doesnt exist
            if (!Utilities.CategoryExists(categoryName, path)) {
                System.IO.File.AppendAllText(path, categoryName + Environment.NewLine);
            }
            this.CategoriesList = Utilities.GetAllCategories(path).ToArray();
        }

        public void RemoveCategory(string categoryName) {
            string path = Constants.TEMP_CATEGORY_FILE;
            if (!File.Exists(path)) {
                return;
            }
            string text = File.ReadAllText(path);
            text = text.Replace(categoryName+Environment.NewLine, "");
            File.WriteAllText(path, text);
            this.CategoriesList = Utilities.GetAllCategories(path).ToArray();
        }
    }
}