using System.Net;

namespace PoS.Core.Exceptions
{
    public class PoSException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public PoSException(string message, HttpStatusCode statusCode) : base(message) 
        {
            StatusCode = statusCode;
        }
    }
}
