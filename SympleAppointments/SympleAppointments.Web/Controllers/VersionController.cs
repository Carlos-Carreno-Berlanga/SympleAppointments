using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SympleAppointments.Web.Controllers
{
    public class VersionController : Controller
    {
        [HttpGet]
        public  ActionResult<string> Get()
        {
            return Ok("HOLA");
        }
    }
}
