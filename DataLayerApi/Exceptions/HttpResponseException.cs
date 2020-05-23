using System;
using System.Net;

namespace DataLayerApi.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException()
        {

        }

        public HttpResponseException(string message) : base(message)
        {

        }

        public HttpResponseException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpResponseException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public HttpResponseException(string message, HttpStatusCode statusCode, Exception innerException) : base(message, innerException)
        {
            this.StatusCode = statusCode;
        }

        public int Status { get; set; } = 500;

        public HttpStatusCode StatusCode
        {
            get { return Enum.Parse<HttpStatusCode>(this.Status.ToString()); }
            set { this.Status = (int)value; }
        }

        public int ErrorCode { get; set; }

        public object Value { get; set; }
    }
}
