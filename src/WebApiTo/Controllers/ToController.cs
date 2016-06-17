using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTo.Controllers
{
    [Route("api/[controller]")]
    public class ToController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "To API is alive!";
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                using (var stream = System.IO.File.Create(@"C:\temp\newfile.pdf", 1024 * 64))
                {
                    await Request.Body.CopyToAsync(stream);
                }
            }
            catch
            {
                return BadRequest("can't save file");
            }
            return Ok();
        }
    }
}
