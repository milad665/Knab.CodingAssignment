using System;

namespace Knab.CodingAssignment.Framework.Exceptions
{
    public class GeneralApplicationException : ApplicationException, IApplicationException
    {
        public GeneralApplicationException(string message) : base(message)
        {
            
        }
    }
}