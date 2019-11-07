using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace Z01.Models
{
    public class IndexView {
        // Data display
        public Note[] Notes { get; set; }
        // Paging
        public int Page { get; set; }
        public int MaxPages { get; set; }
        // Filtering
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string[] AllCategories { get; set; }
        public string Category { get; set; }
    }
}