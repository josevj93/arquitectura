using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
   public class Referencias
    {
        //direccion en memoria
        public int Direccion { get; set; }

        //numero de bloque
        public int Bloque { get; set; }

        //numero de palabra
        public int Palabra { get; set; }

        //posicion en cache
        public int PosCache { get; set; }
    }
}
