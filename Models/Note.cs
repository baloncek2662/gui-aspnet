using System;
using System.ComponentModel;

namespace Z01.Models
{
    public class Note
    {
        [DisplayName("Title:")]
        public string Title { get; set; }
        [DisplayName("Content:")]
        public string Content { get; set; }
        [DisplayName("Categories:")]
        public string[] CategoriesList { 
            get {
                if (this.CategoriesList == null) {
                    return new string[0];
                } 
                return this.CategoriesList;
            }
            set {} }
        public DateTime Date { get; set; }
        [DisplayName("Markdown:")]
        public bool IsMarkdown { get; set; }
        [DisplayName("Category name:")]
        public string CategoryAction { get; set; }

        public void Save() {            
            if (!System.IO.Directory.Exists(Constants.NOTES_FOLDER)) {
                System.IO.Directory.CreateDirectory(Constants.NOTES_FOLDER);
            }
            // save categories from tempfile to real file and delete the file

            string[] formattedContent = FormatContent();

            string path = Utilities.BuildFullFilePath(Constants.NOTES_FOLDER, this.Title, Utilities.ConvertToExtenison(this.IsMarkdown));
            System.IO.File.WriteAllLines(path, formattedContent); // change to alllines
        }

        public string[] FormatContent() {
            string[] formattedContent = new string[3];
            formattedContent[0] = "date: " + this.Date;
            formattedContent[1] = "categories: " + Utilities.FormatCategories();
            formattedContent[2] = this.Content;
            return formattedContent;
        }

        public void AddCategory(string categoryName) {
            string path = Utilities.BuildFullFilePath(Constants.NOTES_FOLDER, Constants.TEMP_CATEGORY_FILE, Utilities.ConvertToExtenison(Constants.TEMP_CATEGORY_FILE_EXTENSION));
            System.IO.File.AppendAllText(path, ""); //creates file if it doesnt exist
            if (Utilities.CategoryExists(categoryName, path)) {
                return;
            }
            System.IO.File.AppendAllText(path, categoryName + Environment.NewLine); 
            string[] newList = new string[this.CategoriesList.Length+1];
            for (int i = 0; i < this.CategoriesList.Length; i++) {
                newList[i] = this.CategoriesList[i];
            }
            newList[newList.Length - 1] = categoryName;
            this.CategoriesList = newList;
        }
    }
}