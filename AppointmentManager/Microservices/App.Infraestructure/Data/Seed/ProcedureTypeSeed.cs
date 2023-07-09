using System;
using System.Collections.Generic;
using App.Domain.Entities;
using Netcos.EntityFrameworkCore;

namespace App.Infraestructure.Data.Seed
{
    internal class ProcedureTypeSeed : IEntitySeed<ProcedureTypes>
    {
        public ICollection<ProcedureTypes> GetEntities()
        {
            return new ProcedureTypes[]
            {
                new ProcedureTypes { Id = "d8058bb1-11bd-4d35-8bbe-efd3b70d4c1a", Type= "CARDIOLOGIA" },
                new ProcedureTypes { Id = "338adee6-7049-4ea6-a1f2-1f4389de3595", Type= "NEUROLOGIA" },
                new ProcedureTypes { Id = "265fcbbd-5548-475c-92d1-e99caa6d54bf", Type= "DERMATOLOGIA" }
            };
        }
    }
}

