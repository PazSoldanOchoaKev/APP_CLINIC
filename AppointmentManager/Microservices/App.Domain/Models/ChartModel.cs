using System;
using System.Collections;

namespace App.Domain.Models
{
    public class ChartPendingModel
    {
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }

    public class ChartModel
    {
        public IEnumerable result { get; set; }
        public int count { get; set; }
    }
}
