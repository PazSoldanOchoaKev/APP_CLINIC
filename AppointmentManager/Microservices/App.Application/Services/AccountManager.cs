using System;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Entities;
using App.Domain.Models;
using Netcos;
using Netcos.EntityFrameworkCore;
using Netcos.Security.Cryptography;
using static Netcos.Result;

namespace App.Application.Services
{
    internal class AccountManager : IAccountManager
    {
        private readonly IRepository<Access> _accesses;
        private readonly IRepository<Users> _users;

        private const string passwordKey = "a4cQp8mqfG69LbcqmEizkxac09IdAIWv";

        public AccountManager(
            IRepository<Users> users,
            IRepository<Access> accesses)
        {
            _accesses = accesses;
            _users = users;
        }

        public async Task<Result<Users>> RegisterAccountAsync(AccountRequestModel model)
        {
            var passwordEncrypt = EncryptProvider.AESEncrypt(model.Password, passwordKey);
            model.Access = new Access { User = model.Email, Password = passwordEncrypt };
            var result = await _users.AddAsync(model);
            if (!result)
            {
                return Fail("Error al crear el usuario");
            }
            return model;
        }

        public async Task<Result<Users>> EditAccountAsync(AccountRequestModel model)
        {
            var result = await _users.UpdateAsync(model);
            if (!result)
            {
                return Fail("Error al editar los datos del usuario");
            }
            return model;
        }

        public Result<Users> Authenticate(AuthModel model)
        {
            var access = _accesses.FirstOrDefault(a => a.User.ToUpper() == model.User.ToUpper());
            if (access != null)
            {
                var passwordDecrypt = EncryptProvider.AESDecrypt(access.Password, passwordKey);
                if (passwordDecrypt == model.Password)
                {
                    var user = _users.FirstOrDefault(u => u.AccessId == access.Id);
                    return Ok(user);
                }
                else
                {
                    return Fail("Contraseña incorrecta");
                }
            }
            else
            {
                return Fail("Usuario no encontrado");
            }
        }
    }
}
