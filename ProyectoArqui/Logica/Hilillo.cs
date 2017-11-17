using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    class Hilillo
    {
        //identificador del hilillo
        public int IdHilillo { get; set; }

        //siguiente instruccion a ejecutarse
        public int PC { get; set; }

        //direccion donde inician las instrucciones del hilillo
        public int DireccionInicial { get; set; }

        //booleano que indica si no se ha finalizado (al ejecutarse la ultima instruccion se pone en true)
        public bool Finalizado { get; set; }

        //identificador del nucleo donde esta corriendo
        public int IdNucleo { get; set; }
    }
}
