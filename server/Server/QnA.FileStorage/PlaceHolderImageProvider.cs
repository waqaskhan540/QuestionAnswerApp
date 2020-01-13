using Microsoft.AspNetCore.Http;
using QnA.Application.Interfaces;

namespace QnA.FileStorage
{
    public class PlaceHolderImageProvider : IPlaceHolderImageProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public PlaceHolderImageProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetProfileImagePlaceHolder()
        {
            var filename = "placeholder.jpg";

            var request = _contextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host.ToString()}//ProfileImages/{filename}";
        }
    }
}
