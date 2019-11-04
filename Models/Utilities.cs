using System;
using System.IO;

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
                
                note.FileType = file.Extension.Substring(1); // removes the initial dot
                note.Title = Path.GetFileNameWithoutExtension(file.Name); // removes extension from name
                note.Date = file.CreationTime;
                notes[i] = note;
            }            
            return notes;
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