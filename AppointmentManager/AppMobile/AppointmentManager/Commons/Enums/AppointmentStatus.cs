using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager
{
    public enum AppointmentStatus
    {
        PENDING = 1,
        RUNNING = 2,
        DONE = 3
    }
    public enum TypeProcedures
    {
        BAÑO_MEDICADO = 1,
        BAÑO_Y_CORTE = 2,
        CORTE_DE_UÑAS = 3
    }
    public enum ListSizes
    {
        ANIMAL_PEQUEÑO_30 = 1,
        ANIMAL_MEDIANOS_40 = 2,
        ANIMAL_GRANDES_45 = 3
    }
}
