using System;
using App.Domain.Enums;

namespace App.Domain.Models
{
    public class ScheduleModel
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public byte[] Image { get; set; }
        public string Color => EvaluateColor();
        public DateTime StartTime { get; set; }
        public DateTime EndTime => StartTime.AddHours(1).ToUniversalTime();
        public bool IsAllDay { get; set; }
        public bool IsBlock { get; set; }
        public bool IsReadonly { get; set; }
        public AppoinmentStatus Status { get; set; }
        public string Procedure { get; set; }

        private string EvaluateColor()
        {
            switch (Status)
            {
                case AppoinmentStatus.PENDING:
                    if (EndTime < DateTime.Now.ToUniversalTime())
                        return "#ff0040";
                    break;
                case AppoinmentStatus.RUNNING:
                    return "#87b300";
                case AppoinmentStatus.DONE:
                    return "#7b7b7b";
            }
            return "";
        }
    }
}
