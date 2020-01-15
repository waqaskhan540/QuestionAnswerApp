using System;
using System.Collections.Generic;

namespace QnA.Api.ApiModels
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }

        public List<string> Errors { get; set; }
        public static BaseResponse Ok(Object data = null)
        {
            return new BaseResponse { Success = true, Data = data };
        }

        public static BaseResponse Ok(string message)
        {
            return new BaseResponse { Success = true, Message = message };
        }

        public static BaseResponse Error(string message = null)
        {
            return new BaseResponse { Message = message, Success = false };
        }

        public static BaseResponse Error(string message, List<string> errors)
        {
            return new BaseResponse { Message = message, Errors = errors };
        }


    }
}
