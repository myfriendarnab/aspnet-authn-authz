using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace UamAuthorizationHelper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomAuthorizationPolicy(this IServiceCollection services, string policyName)
        {
            var svcProvider = services.BuildServiceProvider();
            var config = svcProvider.GetRequiredService<IConfiguration>();
            var allowedUamRoles = config.GetSection($"UAMRolesConfiguration:{policyName}").Get<IEnumerable<string>>();
            //var allowedUamRoles = config.Get<IEnumerable<string>>($"UAMRolesConfiguration:{policyName}");

            services.AddSingleton<IAuthorizationHandler, ScmUserRequirementHandler>();
            
            services.AddAuthorization(option => 
                option.AddPolicy(policyName, policy=> 
                    policy.Requirements.Add(new ScmUserRequirement(allowedUamRoles))));
            
            return services;
        }
    }
}