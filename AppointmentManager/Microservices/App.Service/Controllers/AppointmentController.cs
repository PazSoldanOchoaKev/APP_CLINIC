﻿using App.Application;
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
        public Result GetAppointment(string userId)
        {
            return _appointmentManager.GetAppointmentByUser(userId);
        }
           
    }
}