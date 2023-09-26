using System;
using System.Net;
namespace HotelBooking.Core.Exceptions {
    public class RestException : Exception{

        public RestException(HttpStatusCode statusCode, string message ) : base(message) {
            StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; set; }
    }
}
