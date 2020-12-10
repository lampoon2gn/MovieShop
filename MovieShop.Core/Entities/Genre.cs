
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
/*using Newtonsoft.Json;*/
using System.Text.Json.Serialization;

namespace MovieShop.Core.Entities
{
    [Table("Genre")]
    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(24)]
        public string Name { get; set; }

        //the navigation property of movie+genre will automatically create a junction table for genre and movie
        [JsonIgnore]
        public ICollection<Movie> Movies { get; set; }
    }
}
