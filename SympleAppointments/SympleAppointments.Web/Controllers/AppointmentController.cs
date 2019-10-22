using Microsoft.AspNetCore.Mvc;
using SympleAppointments.Application.Appointments;
using System.Threading.Tasks;

namespace SympleAppointments.Web.Controllers
{
    public class AppointmentController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAsync(Create.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
