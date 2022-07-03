using App.Domain.Entities;
using App.Domain.Models;
using Netcos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public interface IPetsManager
    {
        Task<Result> CreatePetAsync(PetModel model);
        Result<IEnumerable<Pets>> GetPetsByUser(string userId);
        Task<Result> EditPetAsync(PetModel model);
        Task<Result> DeletePetAsync(PetModel model);
    }
}
