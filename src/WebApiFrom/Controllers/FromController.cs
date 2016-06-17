using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Net.Http;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApiFrom.Controllers
{
    [Route("api/[controller]")]
    public class FromController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "From API is alive!";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string path)
        {
            if (path == null)
            {
                return BadRequest("path cannot be null");
            }
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var content = new StreamContent(stream);
                    content.Headers.Clear();
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    content.Headers.ContentLength = stream.Length;

                    var client = new HttpClient();
                    var response = await client.PostAsync("http://localhost:58449/api/to", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        return BadRequest(response.ReasonPhrase);
                    }
                }
            }
            catch
            {
                return BadRequest("error opening file");
            }
            return Ok();
        }
    }
}
