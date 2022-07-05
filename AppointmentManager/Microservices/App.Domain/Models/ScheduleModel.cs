using System;
namespace App.Domain.Models
{
    public class ScheduleModel
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public byte[] Image { get; set; }
        public string CategoryColor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime => StartTime.AddHours(1).ToUniversalTime();
        public bool IsAllDay { get; set; }
        public bool IsBlock { get; set; }
        public bool IsReadonly { get; set; }
    }
}
