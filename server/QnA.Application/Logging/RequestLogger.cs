using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Logging
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILoggerFactory _loggerFactory;
        public RequestLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var logger = _loggerFactory.CreateLogger<TRequest>();
            logger.LogInformation("QnA Request: {Name}  {@Request}",
                typeof(TRequest).Name, request);
            return Task.CompletedTask;
        }
    }
}
