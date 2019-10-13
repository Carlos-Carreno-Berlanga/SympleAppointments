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


        [HttpPost]
        public async Task<ActionResult<ReviewDto>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
