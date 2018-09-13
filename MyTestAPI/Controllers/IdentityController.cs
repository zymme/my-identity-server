using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyTestAPI.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("Inside Get() for IdentityController");

            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
