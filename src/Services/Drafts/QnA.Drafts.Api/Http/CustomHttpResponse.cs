using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Http
{
    public class CustomHttpResponse<T> : ICustomHttpResponse<T>
    {
        public CustomHttpResponse(HttpStatusCode statusCode, string[] errors, T data)
        {
            StatusCode = statusCode;           
            Errors = errors;
            Data = data;
        }
        public HttpStatusCode StatusCode { get; private set; }       
        public string[] Errors { get; private set; }
        public T Data { get; private set; }
        public string Path { get; private set; }
    }

    public class CustomHttpResponse : ICustomHttpResponse
    {
        private CustomHttpResponse(HttpStatusCode statusCode, string[] errors = null, string path = null)
        {
            StatusCode = statusCode;           
            Errors = errors;
            Path = path;
        }
        public string[] Errors { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }
        public string Path { get; private set; }

        public static CustomHttpResponse Create(HttpStatusCode statusCode,string[] errors = null, string path = null)
        {
            return new CustomHttpResponse(statusCode, errors, path);
        }

       
    }
}
