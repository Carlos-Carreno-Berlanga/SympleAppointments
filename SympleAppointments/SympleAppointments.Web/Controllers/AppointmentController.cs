using Microsoft.AspNetCore.Mvc;
using SympleAppointments.Application.Appointments;
using System;
using System.Threading.Tasks;

namespace SympleAppointments.Web.Controllers
{
    public class AppointmentController : BaseController
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetOneAsync(Guid id)
        {

            return Ok(await Mediator.Send(new GetOne.Query { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAsync(Create.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
