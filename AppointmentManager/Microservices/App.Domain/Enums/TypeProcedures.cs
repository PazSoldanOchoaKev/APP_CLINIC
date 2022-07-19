using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace App.Domain.Enums
{
    public enum TypeProcedures
    {
        [EnumMember(Value = "Baño Medicado")]
        BAÑO_MEDICADO = 1,
        [EnumMember(Value = "Baño y Corte")]
        BAÑO_Y_CORTE = 2,
        [EnumMember(Value = "Corte de Uñas")]
        CORTE_DE_UÑAS = 3
    }

    public enum AppoinmentStatus
    {
        PENDING = 1,
        RUNNING = 2,
        DONE = 3
    }
}
