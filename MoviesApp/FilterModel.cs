using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp
{
    public class FilterModel
    {
        public string Title { get; set; }
        public List<string> Values { get; set; }
        public string SelectedItem { get; set; }
    }
}
