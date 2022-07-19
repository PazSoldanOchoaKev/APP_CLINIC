using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Models;
using Netcos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public interface IAppointmentManager
    {
        Task<Result> CreatAppointmentAsync(AppointmentModel model);
        Result<IEnumerable<Appointment>> GetAppointmentByUser(string userId, AppoinmentStatus status);
        Result<IEnumerable<string>> GetAvailableHours(DateTime date);
        Task<Result> EditAppointmentAsync(AppointmentModel model);
        Result<IEnumerable<ScheduleModel>> GetAppoinments(DateTime StartDate, DateTime EndDate);
        Task<Result> DeleteAppointmentAsync(AppointmentModel model);
        Result<ChartModel> GetChart();
    }
}
