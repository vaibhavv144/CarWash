
using System.Net;
 

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        
 
       public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
           
        }
 
        public async Task InvokeAsync(HttpContext httpContext){
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                
              
                
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";    
 
                var error = new{
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We will try to resolve it",
 
                };
 
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }

 