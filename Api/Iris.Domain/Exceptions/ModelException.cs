using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTemplate.Domain.Exceptions
{
    public class ModelException : Exception
    {
        public ModelException() { }        

        public ModelException(string message): base(message) { }

        public ModelException(string message, Exception inner): base(message, inner) { }
    }
}
