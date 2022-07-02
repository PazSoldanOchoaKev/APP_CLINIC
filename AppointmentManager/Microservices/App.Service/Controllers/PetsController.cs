using App.Application;
using App.Domain.Models;
using Microsoft.AspNetCore.Http;
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
    public class PetsController : ControllerBase
    {
        private readonly IPetsManager _petsManager;
        public PetsController(
            IPetsManager  petsManager)
        {
            _petsManager = petsManager;
        }

        [HttpPost]
        public Task<Result> CreatPets([FromBody] PetModel model)
        {
            return _petsManager.CreatePetAsync(model);
        }

        [HttpPost("edit")]
        public Task<Result> EditPets([FromBody] PetModel model)
        {
            return _petsManager.EditPetAsync(model);
        }

        [HttpGet("{userId}")]
        public Result GetPets(string userId)
        {
            return _petsManager.GetPetsByUser(userId);
        }

    }
}
