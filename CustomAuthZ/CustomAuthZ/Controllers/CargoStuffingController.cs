using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Maersk.Warehouse.MIMEntities;
using Microsoft.AspNetCore.Authorization;
using UamAuthorizationHelper;

namespace CustomAuthZ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CargoStuffingController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuthorizationService _authorizationService;

        public CargoStuffingController(ILogger<WeatherForecastController> logger/*, IAuthorizationService authorizationService*/)
        {
            _logger = logger;
            //_authorizationService = authorizationService;
        }

        [HttpGet]
        //[Authorize(Policy = "ScmUserOnlyUsersForClr")]
        public async Task<IActionResult> Get()
        {

            ////custom authz
            if (Request.HttpContext.User.IsInRole("XYZ"))
            {
            }

            //var authzResult = await _authorizationService.AuthorizeAsync(Request.HttpContext.User, this,
            //    new[] {new ScmUserRequirement(new List<string> {""})});

            //if (authzResult.Succeeded)
            //{
            //    return Ok(new List<CargoStuffing>{new CargoStuffing()});
            //}

            return BadRequest("");
        }
    }
}
