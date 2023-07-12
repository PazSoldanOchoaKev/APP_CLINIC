using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Netcos;
using Netcos.EntityFrameworkCore;
using static Netcos.Result;

namespace App.Application.Services
{
    internal class AppointmentManager : IAppointmentManager
    {
        private readonly IRepository<Appointment> _appointments;
        private readonly IRepository<Pets> _pets;
        private readonly IRepository<ProcedureTypes> _procedureTypes;
        private readonly IRepository<Doctors> _doctors;

        public AppointmentManager(
            IRepository<Pets> pets,
            IRepository<ProcedureTypes> procedureTypes,
            IRepository<Appointment> appointment,
            IRepository<Doctors> doctors
            )
        {
            _appointments = appointment;
            _pets = pets;
            _procedureTypes = procedureTypes;
            _doctors = doctors;
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

        public Result<IEnumerable<AppointmentModel>> GetAppointmentByUser(string userId, AppoinmentStatus status)
        {
            return _appointments
                .Include(a => a.Pets)
                .Include(a => a.Doctor)
                .Include(a => a.Doctor.ProcedureTypes)
                .Where(a => a.Pets.UserId == userId && a.Status == status)
                .OrderByDescending(a => a.DateAppointment)
                .Select(a => new AppointmentModel
                {
                    DateAppointment = a.DateAppointment,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name,
                    Hour = a.Hour,
                    Id = a.Id,
                    PetId = a.PetId,
                    ClientName = a.Pets.PetName,
                    Status = a.Status,
                    Pets = a.Pets,
                    TypeProcedureId = a.TypeProcedureId,
                    TypeProcedureName = a.Doctor.ProcedureTypes.Type,
                })
                .ToList();
        }

        public async Task<Result> EditAppointmentAsync(AppointmentModel model)
        {
            var result = await _appointments.UpdateAsync(model);
            if (!result)
            {
                return Fail("Error al editar la mascota");
            }
            return result;
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

        public Result<IEnumerable<ScheduleModel>> GetAppoinments(DateTime StartDate, DateTime EndDate)
        {
            return _appointments
                .Include(a => a.Pets)
                //.Include(a => a.ProcedureType)
                .Where(a => a.DateAppointment.Date >= StartDate && a.DateAppointment.Date <= EndDate)
                .Select(a => new ScheduleModel
                {
                    Id = a.Id,
                    //Image = a.Pets.Photo,
                    StartTime = a.DateAppointment.Date.Add(a.Hour).ToUniversalTime(),
                    Subject = a.Pets.PetName,
                    Status = a.Status
                })
                .ToList();
        }

        public Result<ChartModel> GetChart()
        {
            var pendings = _appointments
                .Where(a => a.Status == AppoinmentStatus.PENDING && a.DateAppointment.Date >= DateTime.Now.AddMonths(-1))
                .GroupBy(a => a.DateAppointment.Date)
                .Select(a => new ChartPendingModel
                {
                    Count = a.Count(),
                    Date = a.Key.ToUniversalTime()
                })
                .ToList();
            return new ChartModel { result = pendings, count = pendings.Count };
        }

        public Result<IEnumerable<ProcedureTypes>> GetProcedureTypes()
        {
            return _procedureTypes.ToList();
        }

        public Result<IEnumerable<Doctors>> GetDoctors(string procedureId)
        {
            return _doctors
                .Where(item => item.ProcedureTypeId == procedureId)
                .ToList();
        }

        public Result<IEnumerable<int>> GetAvailableHours(string doctorId, DateTime date, string zone)
        {
            var doctor = _doctors.FirstOrDefault(item => item.Id == doctorId);
            if (doctor == null)
                return Fail("No se encontro horario disponible", 404);
            var hours = Enumerable.Range(doctor.StartHour, doctor.CountHour).Select(item => TimeSpan.FromHours(item));
            var usedHours = _appointments.Where(a => a.DateAppointment.Date == date && a.DoctorId == doctorId).Select(a => a.Hour).ToList();
            var nowLocalDate = DateTime.Now.ToUniversalTime().ConvertDateByTimeZoneId(zone);
            if (date.Date == nowLocalDate.Date)
                usedHours.AddRange(Enumerable.Range(1, nowLocalDate.Hour).Select(item => TimeSpan.FromHours(item)));
            else if (date.Date < nowLocalDate.Date)
                usedHours.AddRange(Enumerable.Range(1, 23).Select(item => TimeSpan.FromHours(item)));
            var availableHours = hours
                .Where(h => !usedHours.Contains(h))
                .Select(item => item.Hours);
            return Ok(availableHours);
        }
    }
}
