using MediatR;
using Microsoft.AspNetCore.Mvc;
using SympleAppointments.Application.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SympleAppointments.Web.Controllers
{
    public class ReviewsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetOneAsync(Guid id)
        {

            return Ok(await Mediator.Send(new GetOne.Query { Id = id }));
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewDto>>> GetAllAsync()
        {

            return Ok(await Mediator.Send(new GetAll.Query()));
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateAsync(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteAsync(Guid id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }
    }
}
