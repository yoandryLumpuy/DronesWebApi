using System;

namespace DronesWebApi.Commons.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(): base()
        { }

        public BadRequestException(string message): base(message)
        { }

        public BadRequestException(string message, Exception innerException): base(message, innerException)
        { }
    }
}
