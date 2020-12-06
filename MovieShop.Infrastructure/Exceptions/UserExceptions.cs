using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Infrastructure.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {

        }

        public UserNotFoundException(string email)
            :base(String.Format("Something went wrong looking for user with email: {0}",email))
        {

        }
    }
}
