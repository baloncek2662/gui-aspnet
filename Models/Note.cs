using System;

namespace Z01.Models
{
    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] CategoriesList { get; set; }
        public DateTime Date { get; set; }
        public FileType FileType { get; set; }

        public void TestMethod() {

        }
    }

    public static class Utilities {
        public static Note[] GetNotes() {
            Note[] allNotes = new Note[1]; // change 1 to n, which you get from counting files in root/notes
            
            Note test = new Note();
            test.Title="Tast Title as";
            test.Content="Test content 123 abcd";
            test.CategoriesList=new string[2] {"sport","random"};
            test.Date=DateTime.Now;
            allNotes[0] = test;
            
            return allNotes;
        }
    }

    public enum FileType {
        Txt = 0,
        Md = 1
    }
}