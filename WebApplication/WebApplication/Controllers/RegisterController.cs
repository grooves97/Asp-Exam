using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.DataAccess;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly EarthquakeContext context;

        public RegisterController(EarthquakeContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Username == user.Username);

            if (existingUser != null)
            {
                return BadRequest();
            }

            context.Add(user);
            await context.SaveChangesAsync();

            return Ok(Response);
        }
    }
}