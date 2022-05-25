using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application;
using App.Domain.Entities;
using App.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netcos;

namespace App.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AccountController(
            IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost]
        public Task<Result<Users>> Resgister([FromBody] AccountRequestModel model)
        {
            return _accountManager.RegisterAccountAsync(model);
        }

        [HttpPost("SignIn")]
        public Result SignIn([FromBody] AuthModel model) 
        {
            return _accountManager.Authenticate(model);
        }
    }
}
