using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace Z01.Models
{
    public class IndexView {
        public Note[] Notes { get; set; }
        public int Page { get; set; }
        public int MaxPages { get; set; }
    }
}