using App.Application;
using App.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Netcos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentManager _paymentManager;
        public PaymentController(
            IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        [HttpPost]
        public Task<Result> CreateCard([FromBody] CardDataModel model)
        {
            return _paymentManager.CreateCardAsync(model);
        }
    }
}
