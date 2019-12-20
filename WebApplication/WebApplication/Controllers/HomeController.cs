using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebApplication.DTOs;
using WebApplication.Options;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public IActionResult Info()
        {
            string json;
            using (WebClient client = new WebClient())
            {
                var count = HttpContext.Request.Headers["Count"];

                if (string.IsNullOrEmpty(count))
                {
                    json = client.DownloadString("https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson");
                }
                else json = client.DownloadString("https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&limit=" + count);
            }
            var data = JsonConvert.DeserializeObject<FeatureCollection>(json);

            return Ok(data.Features);
        }
    }
}