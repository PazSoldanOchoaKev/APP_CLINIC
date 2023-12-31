﻿using App.Domain.Entities;
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
        Result<IEnumerable<AppointmentModel>> GetAppointmentByUser(string userId, AppoinmentStatus status);
        Task<Result> EditAppointmentAsync(AppointmentModel model);
        Result<IEnumerable<ScheduleModel>> GetAppoinments(DateTime StartDate, DateTime EndDate);
        Task<Result> DeleteAppointmentAsync(AppointmentModel model);
        Result<ChartModel> GetChart();
        Result<IEnumerable<ProcedureTypes>> GetProcedureTypes();
        Result<IEnumerable<Doctors>> GetDoctors(string procedureId);
        Result<IEnumerable<int>> GetAvailableHours(string doctorId, DateTime date, string zone);
    }
}
