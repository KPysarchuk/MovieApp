using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Common;

namespace MoviesApp
{
    public static class ParserXML
    {
        public static List<MovieModel> Parse()
        {
            var xmlDoc = XElement.Load(Constants.XMLPath);

            var movies = xmlDoc.Descendants("movie").Select(movie => new MovieModel
            {
                Title = movie.Element("title")?.Value,
                Year = movie.Element("year")?.Value,
                Country = movie.Element("country")?.Value,
                Genre = movie.Element("genre")?.Value,
                Summary = movie.Element("summary")?.Value,
                
                Director = movie.Descendants("director").Select(director => new Director
                {
                    LastName = director.Element("last_name")?.Value,
                    FirstName = director.Element("first_name")?.Value,
                    BirthDate = director.Element("birth_date")?.Value,
                }).FirstOrDefault(),

                Actors = movie.Descendants("actor").Select(actor => new Actor
                {
                    LastName = actor.Element("last_name")?.Value,
                    FirstName = actor.Element("first_name")?.Value,
                    BirthDate = actor.Element("birth_date")?.Value,
                    Role = actor.Element("role")?.Value
                }).ToList()
            }).ToList();

            return movies;
        }
    }
}
