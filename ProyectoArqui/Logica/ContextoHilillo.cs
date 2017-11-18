using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    public class ContextoHilillo
    {
        //arreglo de registros 0-31
        public int[] Registros = new int[32];

        //proxima instruccion a ejecutar
        public int PC { get; set; }

        //registro de instruccion
        public Utilidades.CodigosInst IR { get; set; }

        //identificador del nucleo donde fue ejecutado
        public int IdNucleo { get; set; }
        public int IdProcesador { get; set; }

        //direccion donde inician las instrucciones del hilillo
        public int DireccionInicial { get; set; }

        //booleano que indica si no se ha finalizado (al ejecutarse la ultima instruccion se pone en true)
        public bool Finalizado { get; set; }


        public ContextoHilillo(int DireccionInicial)
        {
            this.DireccionInicial = DireccionInicial;
            PC = DireccionInicial;
            Finalizado = false;
           for(int i = 0; i < Registros.Count(); ++i )
            {
                Registros[i] = 0;
            }
        }
    }
}
