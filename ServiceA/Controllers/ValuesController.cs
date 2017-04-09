using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var user = HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var email = user.FindFirst(ClaimTypes.Name).Value;
            var displayName = user.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            return new[] { $"Service A has recognized you as {displayName} with email {email} and identity {userId}." };
        }
    }
}

