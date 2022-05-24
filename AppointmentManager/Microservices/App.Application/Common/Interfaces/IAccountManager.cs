using System;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Models;
using Netcos;

namespace App.Application
{
    public interface IAccountManager
    {
        Task<Result> RegisterAccountAsync(AccountRequestModel model);
        Result<Users> Authenticate(AuthModel model);
    }
}
