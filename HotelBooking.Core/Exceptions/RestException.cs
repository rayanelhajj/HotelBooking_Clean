using System;
using System.Net;
namespace HotelBooking.Core.Exceptions {
    public class RestException : Exception{
        public HttpStatusCode Status { get; }
        public string Error { get; }
        public string Code { get; set; }

        public RestException(HttpStatusCode status, string error = null):base(error) {
            Status = status;
            Error = error;
        }

    }
}
