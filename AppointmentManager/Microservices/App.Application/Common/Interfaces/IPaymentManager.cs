using App.Domain.Entities;
using App.Domain.Models;
using Netcos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public interface IPaymentManager
    {
        Task<Result> CreateCardAsync(CardDataModel model);
    }
}
