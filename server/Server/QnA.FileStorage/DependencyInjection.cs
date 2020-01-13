using Microsoft.Extensions.DependencyInjection;
using QnA.Application.Interfaces;

namespace QnA.FileStorage
{
    public static class DependencyInjection
    {
        public static void AddStorage(this IServiceCollection services)
        {
            services.AddScoped<IFileStorageProvider, FileSystemStorageProvider>();
            services.AddScoped<IPlaceHolderImageProvider, PlaceHolderImageProvider>();
        }
    }
}
