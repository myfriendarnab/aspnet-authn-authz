using Microsoft.AspNetCore.Builder;

namespace UamAuthorizationHelper
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder RegisterUserPermissions(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<UamRoleBuilderMiddleware>();
            return applicationBuilder;
        }
    }
}