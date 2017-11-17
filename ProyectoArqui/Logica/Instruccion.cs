using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    public class Instruccion
    {
        //Codigo de Operacion ya codificada en Utilidades
        public Utilidades.CodigosInst CodigoOp { get; set; }
        
        //Registro fuente 1
        public int RF1 = 0;

        //Registro fuente 2 o Registro Destino
        public int RF2_RD = 0;

        //Registro Destino o Inmediato
        public int RD_IMM = 0;

        ////Valor de la etiqueta
        //public int Etiq = -1;
    }
}
