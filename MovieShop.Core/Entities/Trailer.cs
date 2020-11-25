using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Entities
{
    //one movie can have multiple trailers
    public class Trailer
    {
        public int Id { get; set; }
        //foreign key from movie table
        public int MovieId { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }

        //navigation properties, navigate to related entities
        //suppose you get a trailer id and need the movie title, you can get it from the movie object here
        public Movie Movie { get; set; }

        
    }
}
