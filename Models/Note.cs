using System;

namespace Z01.Models
{
    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] CategoriesList { get; set; }
        public DateTime Date { get; set; }
        public string FileType { get; set; }
    }
}