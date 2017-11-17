using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    public class Utilidades
    {

        public enum CodigosInst {DADDI = 8, DADD = 32, DSUB = 34, DMUL = 12, DDIV = 14, BEQZ = 4, BNEZ = 5, JAL = 3, JR = 2, LW = 35, SW = 43, FIN = 63 }


        //convierte una direccion fisica (simulada) en una referencia de memoria dada en bloque, palabra y posicion en cache
        public static Referencias getReferencias(int direccion)
        {
            Referencias result = new Referencias
            {
                Direccion = direccion,
                Bloque = direccion / 16,
                Palabra = (direccion % 16) / 4
            };

            //posicion en cache
            result.PosCache = result.Bloque % 4;

            return result;
        }


    }
}
