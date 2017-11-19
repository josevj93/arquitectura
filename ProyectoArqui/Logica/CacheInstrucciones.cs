using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    /// <summary>
    /// Esta clase la creamos para manejar de una manera mas entendible la logica de 
    /// la cache de instrucciones y las etiquetas, usando la clase previamente creada 
    /// llamada Instruccion.
    /// </summary>
    public class CacheInstrucciones
    {

        public Instruccion[,] bloqueInstruccion = new Instruccion[4, 4];
        public int[] etiquetas = new int[4];

        public CacheInstrucciones()
        {
            //inicializamos las etiquetas en -1
            for (int i = 0; i < 4; i++)
            {
                etiquetas[i] = -1;
            }
        }

    }
}