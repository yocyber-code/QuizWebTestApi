using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Quiz.Contracts.Interfaces;

using System.Data.SqlTypes;
using System.Diagnostics;
using System.Net;
using System.Text;
using Quiz.Core.Exceptions;

namespace Wattana.Web.Middleware
{
    public class RequestHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestHandleMiddleware> _logger;
        public RequestHandleMiddleware(RequestDelegate next, ILogger<RequestHandleMiddleware> log)
        {
            _next = next;
            _logger = log;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            try
            {
                // Log request parameters here
                _logger.LogInformation($"[{threadId}] Request: {context.Request.Method} {context.Request.Path}{context.Request.QueryString}");

                // Log request headers
                _logger.LogInformation("Request Headers:");

                foreach (var header in context.Request.Headers)
                {
                    if (header.Key == "Authorization")
                        _logger.LogInformation($"[{threadId}] {header.Key}: {header.Value}");
                }

                // Log form data (POST request)
                if (context.Request.HasFormContentType)
                {
                    foreach (var formParameter in context.Request.Form)
                    {
                        _logger.LogInformation($"[{threadId}] Form Parameter: {formParameter.Key} = {formParameter.Value}");
                    }
                }

                if (context.Request.Method == "POST" || context.Request.Method == "PUT")
                {
                    context.Request.EnableBuffering(); // Enable request body buffering

                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true))
                    {
                        var requestBody = await reader.ReadToEndAsync();
                        // Log the request body as JSON
                        if (!string.IsNullOrEmpty(requestBody))
                        {
                            try
                            {
                                var jsonBody = JToken.Parse(requestBody).ToString(Formatting.None);
                                _logger.LogInformation($"[{threadId}] Request Body: {jsonBody}");
                            }
                            catch (Exception)
                            {
                                _logger.LogWarning("Unable to parse request body as JSON.");
                            }

                            // Reset the request body stream position for further processing
                            context.Request.Body.Seek(0, SeekOrigin.Begin);
                        }
                    }
                }

                await _next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception exceptionObj)
            {
                _logger.LogError($"[{threadId}] {context.Request.Method} {context.Request.Path}{context.Request.QueryString}");
                _logger.LogError($"[{threadId}] {exceptionObj.Message}");
                _logger.LogError($"[{threadId}] {exceptionObj.StackTrace}");
                Console.Clear();
                Console.WriteLine(exceptionObj);
                await HandleExceptionAsync(context, exceptionObj);
            }
        }


        private Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            string result = null;
            context.Response.ContentType = "application/json";
            if (exception is HttpStatusCodeException)
            {
                result = new ErrorDetails()
                {
                    Message = exception.Message,
                    Code = (int)exception.StatusCode
                }.ToString();
                context.Response.StatusCode = (int)exception.StatusCode;
            }
            else
            {
                result = new ErrorDetails()
                {
                    Message = "Runtime Error",
                    Code = (int)HttpStatusCode.BadRequest
                }.ToString();
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return context.Response.WriteAsync(result);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string err = exception.ToString();
            string result = new ErrorDetails()
            {
                Message = err,
                Code = (int)HttpStatusCode.InternalServerError
            }.ToString();
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}
