using Quiz.Contracts.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Helpers
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingHandler> _logger;
        private readonly IDatabaseManager _database;

        public LoggingHandler(ILogger<LoggingHandler> logger, IDatabaseManager database)
        {
            _logger = logger;
            _database = database;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Capture request headers and body
            var requestHeaders = request.Headers.ToString();

            string? requestBody = null;

            if (request.Content != null)
            {
                requestBody = await request.Content.ReadAsStringAsync();
            }


            // Send the HTTP request
            var response = await base.SendAsync(request, cancellationToken);

            stopwatch.Stop();

            // Capture response headers and body
            var responseHeaders = response.Headers.ToString();
            var responseBody = await response.Content.ReadAsStringAsync();


            //var apiLog = new APF_API_RESPONSE
            //{
            //    REQ_HEADER = requestHeaders,
            //    REQ_BODY = requestBody ?? "",
            //    RSP_HEADER = responseHeaders,
            //    RSP_BODY = responseBody,
            //    DURATION = stopwatch.ElapsedMilliseconds,
            //    STATUS = (int)response.StatusCode,
            //    TIME_STAMP = DateTime.Now,
            //    URL = request.RequestUri.AbsoluteUri
            //};


            string sql = @"INSERT INTO APF_API_RESPONSE (REQ_HEADER, REQ_BODY, RSP_HEADER, RSP_BODY, DURATION, STATUS, TIME_STAMP, URL) VALUES (@REQ_HEADER, @REQ_BODY, @RSP_HEADER, @RSP_BODY, @DURATION, @STATUS, @TIME_STAMP, @URL)";

            //List<SqlParameter> param = new List<SqlParameter>
            //{
            //    new SqlParameter("@REQ_HEADER", apiLog.REQ_HEADER),
            //    new SqlParameter("@REQ_BODY", apiLog.REQ_BODY),
            //    new SqlParameter("@RSP_HEADER", apiLog.RSP_HEADER),
            //    new SqlParameter("@RSP_BODY", apiLog.RSP_BODY),
            //    new SqlParameter("@DURATION", apiLog.DURATION),
            //    new SqlParameter("@STATUS", apiLog.STATUS),
            //    new SqlParameter("@TIME_STAMP", apiLog.TIME_STAMP),
            //    new SqlParameter("@URL", apiLog.URL)
            //};

            //_database.ExecuteCommand(sql, param);

            return response;
        }
    }
}
