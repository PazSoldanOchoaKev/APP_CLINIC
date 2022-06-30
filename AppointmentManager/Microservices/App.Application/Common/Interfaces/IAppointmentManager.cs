using App.Domain.Entities;
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
        Result<IEnumerable<Appointment>> GetAppointmentByUser(string userId);
        Result<IEnumerable<string>> GetAvailableHours(DateTime date);
    }
}
