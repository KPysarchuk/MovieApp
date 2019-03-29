using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace DirectorFilter
{
    public class DirectorFilter : IFilterable
    {
        public List<MovieModel> Filter(List<MovieModel> films, string selectedFilter)
        {
            return films.Where(x => x.Director.FirstName + " " + x.Director.LastName == selectedFilter).ToList();
        }

        public List<string> GetSelectionValues(List<MovieModel> films)
        {
            return films.Select(x => x.Director.FirstName + " " + x.Director.LastName).ToList();
        }

        public string GetFilterName()
        {
            return "Director";
        }
    }
}
