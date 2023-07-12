using System;
using System.Collections.Generic;
using App.Domain.Entities;
using Netcos.EntityFrameworkCore;

namespace App.Infraestructure.Data.Seed
{
    public class DoctorsSeed : IEntitySeed<Doctors>
    {
        public ICollection<Doctors> GetEntities()
        {
            return new Doctors[]
            {
                new Doctors
                {
                    Id = "44454f0e-6dba-4e1d-8312-70f88295250a",
                    Name = "Dr. Ramirez 1",
                    ProcedureTypeId = "d8058bb1-11bd-4d35-8bbe-efd3b70d4c1a",
                    StartHour = 9,
                    CountHour = 4
                },
                new Doctors
                {
                    Id = "d4cf675e-159e-4138-b21f-f02f6846a399",
                    Name = "Dr. Ramirez 2",
                    ProcedureTypeId = "d8058bb1-11bd-4d35-8bbe-efd3b70d4c1a",
                    StartHour = 14,
                    CountHour = 4
                },
                new Doctors
                {
                    Id = "459cc2ed-a856-4bb5-8798-02c766ba4c7a",
                    Name = "Dr. Royer",
                    ProcedureTypeId = "338adee6-7049-4ea6-a1f2-1f4389de3595",
                    StartHour = 9,
                    CountHour = 6
                },
                new Doctors
                {
                    Id = "ae73a318-5610-40e7-8f75-6940fd8fd411",
                    Name = "Dr. Vasquez",
                    ProcedureTypeId = "265fcbbd-5548-475c-92d1-e99caa6d54bf",
                    StartHour = 14,
                    CountHour = 4
                },
                new Doctors
                {
                    Id = "efe5fe3c-22fc-45a4-9c05-0e526923abbd",
                    Name = "Dr. Vasquez 1",
                    ProcedureTypeId = "265fcbbd-5548-475c-92d1-e99caa6d54bf",
                    StartHour = 18,
                    CountHour = 4
                },
            };
        }
    }
}

