using App.Application;
using App.Domain.Enums;
using App.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Netcos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentManager _appointmentManager;

        public AppointmentController(
            IAppointmentManager appointmentManager)
        {
            _appointmentManager = appointmentManager;
        }

        [HttpPost]
        public Task<Result> CreatAppintment([FromBody] AppointmentModel model)
        {
            return _appointmentManager.CreatAppointmentAsync(model);
        }
        [HttpGet("{userId}")]
        public Result GetAppointment(string userId, [FromQuery] AppoinmentStatus status)
        {
            return _appointmentManager.GetAppointmentByUser(userId, status);
        }

        [HttpPost("edit")]
        public Task<Result> EditAppointment([FromBody] AppointmentModel model)
        {
            return _appointmentManager.EditAppointmentAsync(model);
        }
        [HttpPost("delete")]
        public Task<Result> Deleteappointment([FromBody] AppointmentModel model)
        {
            return _appointmentManager.DeleteAppointmentAsync(model);
        }
        [HttpGet("avaibalehours")]
        public Result GetAvailableHours(DateTime date)
        {
            return _appointmentManager.GetAvailableHours(date);
        }

        [HttpGet("schedule")]
        public Result GetAppoinments(DateTime StartDate, DateTime EndDate)
        {
            return _appointmentManager.GetAppoinments(StartDate, EndDate);
        }

    }
}
