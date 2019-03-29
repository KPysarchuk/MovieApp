using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace YearFilter
{
    public class YearFilter : IFilterable
    {
        public List<MovieModel> Filter(List<MovieModel> films, string selectedFilter)
        {
            return films.Where(x => x.Year == selectedFilter).ToList();
        }

        public List<string> GetSelectionValues(List<MovieModel> films)
        {
            return films.Select(x => x.Year).ToList();
        }

        public string GetFilterName()
        {
            return "Year";
        }
    }
}
