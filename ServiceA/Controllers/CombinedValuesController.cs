using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ServiceA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CombinedValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var user = HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var displayName = user.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var serviceA = new[] { $"Service A has recognized you as {displayName} with identity {userId}." };

            // Extract the token
            var token = HttpContext.Request.Headers["Authorization"][0].Substring("Bearer ".Length);

            // Calling service B with the token
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44371/api/values");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.SendAsync(request);
            var serviceB = JToken.Parse(await response.Content.ReadAsStringAsync()).ToObject<IEnumerable<string>>();

            return serviceA.Concat(serviceB);
        }
    }
}

