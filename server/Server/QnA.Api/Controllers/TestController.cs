﻿using Microsoft.AspNetCore.Mvc;

namespace QnA.Api.Controllers
{
    public class TestController : Controller
    {


        [HttpPost("authorize")]
        public IActionResult Authorize()
        {
            var request = HttpContext.Request;
            return Ok();
        }

        [HttpGet("apirunning")]
        public IActionResult Test()
        {
            return Content("Api is running.");
        }
    }
}
