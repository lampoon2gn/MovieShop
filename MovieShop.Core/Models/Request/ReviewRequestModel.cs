﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Request
{
    public class ReviewRequestModel
    {
        public int MovieId { get; set; }
       // public Movie Movie { get; set; }
        public int UserId { get; set; }
        //public User User { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; }
    }
}
