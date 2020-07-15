using System.Net;
using System;
using System.Threading.Tasks;
using App.MidlewareError;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebAPI.Middleware
{
    public class HandleErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandleErrorMiddleware> _logger;
        public HandleErrorMiddleware(RequestDelegate next, ILogger<HandleErrorMiddleware>logger){
            _next = next;
            _logger = logger;
        }

        private async Task HandlerExeptionAsync(HttpContext context,Exception exception,ILogger<HandleErrorMiddleware> logger ){
            object errors = null;
            switch(exception){
               case HandlerError se :
                    logger.LogError(exception, "Manejador error");
                    errors = se.Errors;
                    context.Response.StatusCode = (int)se.Code;
                    break;
                case Exception e:
                    logger.LogError(e, "Error de Servidor");
                    errors = string.IsNullOrWhiteSpace(e.Message)? "Error":e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
                context.Response.ContentType = "application/json";
                if(errors!=null){
                    var resultados = JsonConvert.SerializeObject(new { errors });
                    await context.Response.WriteAsync(resultados);

            }
        }
        public async Task Invoke(HttpContext context){
            try
            {
                await _next(context);
            }catch(Exception ex){
                await HandlerExeptionAsync(context, ex, _logger);
            }
        }
        
    }
    
}