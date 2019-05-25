using Bookify.Auth.Infrastructure.Models;
using System;
using System.Net;

namespace Bookify.Auth.Infrastructure.Helpers.Http
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ResponseErrorModel Error { get; set; }

        public HttpResponseException(HttpStatusCode statusCode, ResponseErrorModel error)
        {
            StatusCode = statusCode;
            Error = error;
        }
    }
}
