using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTemplate.Domain.Exceptions
{
    public class BussinessException:Exception
    {
        public BussinessException() { }        

        public BussinessException(string message): base(message) { }

        public BussinessException (string message, Exception inner): base(message, inner) { }
    }
}
