using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace firehose.Controllers
{
    [Route("api/[controller]")]
    public class QueueController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Hell";
        }

        [HttpPost]
        public void Post(firehose.Models.Event value)
        {
            
        }
    }
}
