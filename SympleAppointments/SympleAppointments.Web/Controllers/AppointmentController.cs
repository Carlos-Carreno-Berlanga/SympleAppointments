using Microsoft.AspNetCore.Mvc;
using SympleAppointments.Application.Appointments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SympleAppointments.Web.Controllers
{
    public class AppointmentController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult<List<AppointmentDto>>> GetAllAsync()
        {

            return Ok(await Mediator.Send(new GetAll.Query()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetOneAsync(Guid id)
        {

            return Ok(await Mediator.Send(new GetOne.Query { Id = id }));
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<List<AppointmentDto>>> GetAllAppointmentsByClient(Guid clientId)
        {

            return Ok(await Mediator.Send(new GetAllByClient.Query { ClientId = clientId }));
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAsync(Create.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
