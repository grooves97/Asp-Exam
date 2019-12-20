using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.DataAccess;
using WebApplication.DTOs;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;
        private readonly EarthquakeContext context;

        public AuthController(AuthService authService, EarthquakeContext context)
        {
            this.authService = authService;
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthDTO authDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var token = await authService.Authenticate(authDTO.Login, authDTO.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }
    }
}