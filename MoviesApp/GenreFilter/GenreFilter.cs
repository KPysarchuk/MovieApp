using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace GenreFilter
{
    public class GenreFilter : IFilterable
    {
        public List<MovieModel> Filter(List<MovieModel> films, string selectedFilter)
        {
            return films.Where(x => x.Genre == selectedFilter).ToList();
        }

        public List<string> GetSelectionValues(List<MovieModel> films)
        {
            return films.Select(x => x.Genre).ToList();
        }

        public string GetFilterName()
        {
            return "Genre";
        }
    }
}
