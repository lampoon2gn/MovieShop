using MovieShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Response
{
    public class TopPurchasedResponseModel
    {
        public Movie movie { get; set; }
        public int numOfPurchases { get; set; }
    }
}
