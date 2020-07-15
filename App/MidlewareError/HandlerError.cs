using System.Net;
using System;
namespace App.MidlewareError
{
    public class HandlerError : Exception
    {
        public HttpStatusCode Code{ get; }
        public object Errors { get; }
        public HandlerError(HttpStatusCode code,object errors=null){
            Code = code;
            Errors = errors;
        }
    }
}