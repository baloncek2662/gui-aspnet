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
        [DisplayName("In categories:")]
        public string[] CategoriesList { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Markdown:")]
        public bool IsMarkdown { get; set; }
    }
}