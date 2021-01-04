using System;
using System.Net;

namespace QnA.Drafts.Api.Http
{
    public interface ICustomHttpResponse<T>
    {
        T Data { get; }
        string[] Errors { get; }

        string Path { get; }
        HttpStatusCode StatusCode { get; }       
    }

    public interface ICustomHttpResponse
    {       
        string[] Errors { get; }
        string Path { get; }
        HttpStatusCode StatusCode { get; }        
    }
}