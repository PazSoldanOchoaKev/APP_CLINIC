using App.Domain.Entities;
using App.Domain.Models;
using Netcos;
using Netcos.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Netcos.Result;

namespace App.Application.Services
{
    internal class PetsManager : IPetsManager
    {
        public readonly IRepository<Pets> _pets;
        public PetsManager(
            IRepository<Pets> pets)
        {
            _pets = pets;
        }

        public async Task<Result> CreatePetAsync(PetModel model)
        {
            var result = await _pets.AddAsync(model);
            if (!result)
            {
                return Fail("Error al crear mascota");
            }
            return result;
        }

        public async Task<Result> EditPetAsync(PetModel model)
        {
            var result = await _pets.UpdateAsync(model);
            if (!result)
            {
                return Fail("Error al editar la mascota");
            }
            return result;
        }

        public async Task<Result> DeletePetAsync(PetModel model)
        {
            var result = await _pets.DeleteAsync(model);
            if (!result)
            {
                return Fail("Error al eliminar la mascota");
            }
            return result;
        }

        public Result<IEnumerable<Pets>> GetPetsByUser(string userId)
        {
            return _pets.Where(p => p.UserId == userId).ToList();
        }
    }
}
