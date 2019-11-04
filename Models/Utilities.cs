using System;
using System.IO;

namespace Z01.Models
{
    public static class Utilities {
        public static Note[] GetNotes() {
            Note[] allNotes = new Note[2]; // change 1 to n, which you get from counting files in root/notes
            
            Note test = new Note();
            test.Title="Title";
            test.Content="Test content 123 abcd";
            test.CategoriesList=new string[2] {"sport","random"};
            test.Date=DateTime.Now;
            test.FileType = "txt";
            allNotes[0] = test;
            allNotes[1] = test;
            
            return allNotes;
        }

        public static Note GetNote(string title) {
            return GetNotes()[0];
        }

        public static string BuildFullFilePath(string folder, string title, string type) {
            string name = title + "." + type;
            return Path.Combine(folder, name);
        }
    }

    public enum FileType {
        Txt = 0,
        Md = 1
    }
}