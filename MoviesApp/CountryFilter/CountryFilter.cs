using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace CountryFilter
{
    public class CountryFilter : IFilterable
    {
        public List<MovieModel> Filter(List<MovieModel> films, string selectedFilter)
        {
            return films.Where(x => x.Country == selectedFilter).ToList();
        }

        public List<string> GetSelectionValues(List<MovieModel> films)
        {
            return films.Select(x => x.Country).ToList();
        }

        public string GetFilterName()
        {
            return "Country";
        }
    }
}
