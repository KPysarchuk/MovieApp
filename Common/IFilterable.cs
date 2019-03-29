using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IFilterable
    {
        List<MovieModel> Filter(List<MovieModel> films, string selectedFilter);
        List<string> GetSelectionValues(List<MovieModel> films);
        string GetFilterName();
    }
}
