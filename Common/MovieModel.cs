using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common
{
    public class MovieModel
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }
        public string Summary { get; set; }
        public Director Director { get; set; }
        public List<Actor> Actors { get; set; }
    }

    public class Director
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string BirthDate { get; set; }
    }

    public class Actor
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string BirthDate { get; set; }
        public string Role { get; set; }
    }
}
