using App.Domain.Entities;
using App.Domain.Models;
using Netcos;
using Netcos.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Netcos.Result;

namespace App.Application.Services
{
    internal class PaymentManager : IPaymentManager
    {

        //private readonly IRepository<Payment> _payment;
        public readonly IRepository<CardData> _cardDatas;
        public PaymentManager(
            //IRepository<Payment> payments)
            IRepository<CardData> cardDatas)
        {
            //_payment = payments;
            _cardDatas = cardDatas;
        }

        public async Task<Result> CreateCardAsync(CardDataModel model)
        {
            var result = await _cardDatas.AddAsync(model);
            if (!result)
            {
                return Fail("Error al realizar el pago");
            }
            return result;
        }
    }
}
