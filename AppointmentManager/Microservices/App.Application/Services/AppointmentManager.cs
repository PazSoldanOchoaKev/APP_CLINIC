using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Netcos;
using Netcos.Drawing;
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

        public async Task<Result> CreatAppointmentAsync(AppointmentModel model)
        {
            var result = await _appointments.AddAsync(model);
            if (!result)
            {
                return Fail("Error al reservar");
            }
            return result;
        }

        public Result<IEnumerable<Appointment>> GetAppointmentByUser(string userId, AppoinmentStatus status)
        {
            return _appointments.Include(a => a.Pets)
                .Where(a => a.Pets.UserId == userId && a.Status == status)
                .OrderByDescending(a => a.DateAppointment)
                .ToList();
        }
        public async Task<Result> DeleteAppointmentAsync(AppointmentModel model)
        {
            var appoinment = _appointments.FirstOrDefault(item => item.Id == model.Id);
            var result = await _appointments.DeleteAsync(appoinment);
            if (!result)
            {
                return Fail("Error al eliminar la mascota");
            }
            return result;
        }

        public Result<IEnumerable<string>> GetAvailableHours(DateTime date)
        {
            var hours = Enumerable.Range(9, 9).Select(item => TimeSpan.FromHours(item));
            var usedHours = _appointments.Where(a => a.DateAppointment.Date == date).Select(a => a.Hour).ToList();
            var availableHours = hours.Where(h => !usedHours.Contains(h)).Select(item => DateTime.MinValue.AddHours(item.Hours).ToString("hh:mm tt"));
            return Ok(availableHours);
        }

        public Result<IEnumerable<ScheduleModel>> GetAppoinments(DateTime StartDate, DateTime EndDate)
        {
            return _appointments
                .Include(a => a.Pets)
                .Where(a => a.DateAppointment.Date >= StartDate && a.DateAppointment.Date <= EndDate)
                .Select(a => new ScheduleModel
                {
                    Id = a.Id,
                    //Image = a.Pets.Photo,
                    StartTime = a.DateAppointment.Date.Add(a.Hour).ToUniversalTime(),
                    Subject = a.Pets.PetName,
                    Status = a.Status
                })
                .ToList()
                .ToList();
        }
    }
}
