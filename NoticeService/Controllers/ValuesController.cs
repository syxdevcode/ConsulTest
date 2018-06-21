using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace NoticeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public ValuesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost("/notice")]
        //[HttpGet("/notice")]
        public IActionResult Notice()
        {
            var bytes = new byte[10240];
            var i = Request.Body.ReadAsync(bytes, 0, bytes.Length);
            var content = System.Text.Encoding.UTF8.GetString(bytes).Trim('\0');

            EmailSettings settings = new EmailSettings()
            {
                SmtpServer = Configuration["Email:SmtpServer"],
                SmtpPort = Convert.ToInt32(Configuration["Email:SmtpPort"]),
                AuthAccount = Configuration["Email:AuthAccount"],
                AuthPassword = Configuration["Email:AuthPassword"],
                ToWho = Configuration["Email:ToWho"],
                ToAccount = Configuration["Email:ToAccount"],
                FromWho = Configuration["Email:FromWho"],
                FromAccount = Configuration["Email:FromAccount"],
                Subject = Configuration["Email:Subject"]
            };

            EmailHelper.SendHealthEmail(settings, content);

            return Ok();
        }
    }
}
