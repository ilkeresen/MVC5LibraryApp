using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5Library.Models;

namespace MVC5Library.Models.ClassIEnumerable
{
    public class Showcase
    {
        public IEnumerable<TBLBook> ShowCaseBook { get; set; }
        public IEnumerable<TBLAbout> ShowCaseAbout { get; set; }
        public IEnumerable<TBLComment> ShowCaseComment { get; set; }
    }
}