using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Maersk.Warehouse.MIMEntities;
using Maersk.Warehouse.MIMEntities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Services.Utility;

namespace UamAuthorizationHelper
{
    public class ScmUserRequirement : IAuthorizationRequirement
    {
        internal readonly IEnumerable<string> _allowedRoles;

        public ScmUserRequirement(IEnumerable<string> allowedRoles)
        {
            _allowedRoles = allowedRoles.Select(r=>r.ToUpperInvariant());
        }
    }

    public class ScmUserRequirementHandler: AuthorizationHandler<ScmUserRequirement, CargoStuffing>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context
            , ScmUserRequirement requirement
            , CargoStuffing resource)
        {
            if (resource.CargoStuffingStatus != CargoStuffingStatus.CONFIRMED || resource.CreateSource != Source.NSCP_ORIGIN)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userRoles = 
                context.User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(r => r.Value);

            if (userRoles.Any(r=>requirement._allowedRoles.Contains(r.ToUpperInvariant())))
                context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}