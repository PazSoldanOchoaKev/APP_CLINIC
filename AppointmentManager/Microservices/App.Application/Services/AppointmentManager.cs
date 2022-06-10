using App.Domain.Entities;
using App.Domain.Models;
using Microsoft.Exchange.WebServices.Data;
using Netcos;
using Netcos.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Netcos.Result;

namespace App.Application.Services
{
    internal class AppointmentManager : IAppointmentManager
    {
        private readonly IRepository<Appointment> _appointments;
        private readonly IRepository<Pets> _pets;

        public AppointmentManager(
            IRepository<Pets> pets,
            IRepository<Appointment> appointment)
        {
            _appointments = appointment;
            _pets = pets;
        }
        public Task CreatApointmentAsync(AppointmentModel model)
        {
            var appointment = _appointments.Include(a => a.Pet).Where(a => a.Pet.UserId == UserId).List();
            var data = _appointments
                .Join(_pets, a => a.PetId, p => p.Id, (a, p) => new { a, p })
                .Where(i => i.p.UserId == userId)
                .Select(i => new Appointment
                {
                    DateAppointment = i.a.DateAppointment,
                    Hour = i.a.Hour,
                    Pets = i.p,
                    Status = i.a.Status,
                    Specificaction = i.a.Specificaction,
                })
                .ToList();
            return Task.CompletedTask;
        }

        /*public async Task<Result> CreatAppointmentAsync(AppointmentModel model)
        {
            var result = await _appointments.AddAsync(model);
            if (!result)
            {
                return Fail("Error al reservar");
            }
            return result;
        }

        public Result<IEnumerable<Appointment>> GetAppointmentByUser(string userId)
        {
            return _appointments
                .Join(_pets.AsEnumerable(), a => a.PetId, p => p.Id, (a, p) => new { a, p })
                .Where(i => i.p.UserId == userId)
                .Select(i => new Appointment
                {
                    DateAppointment = i.a.DateAppointment,
                    Hour = i.a.Hour,
                    Pets = i.p,
                    Status = i.a.Status,
                    Specificaction = i.a.Specificaction,
                })
                .ToList();
        }*/
    }
}
