using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Infrastructure.Exceptions
{
    class MovieNotFoundException:Exception
    {
        public MovieNotFoundException()
        {

        }

        public MovieNotFoundException(int id)
            : base(String.Format("Can't find movie with id: {0}", id))
        {

        }
    }
}
