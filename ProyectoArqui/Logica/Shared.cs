using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    public class Shared
    {

        List<int[]> memoriasCompartida;

        List<int[,]> directorios;

        List<int[,]> cachesDatos;

        Shared()
        {
            // crea una lista de memorias compartidas (cada memoria es de un procesador)
            memoriasCompartida = new List<int[]>();
            //16 bloques [0-252] = 16 bloques x 4 entradas = 64 enteros
            memoriasCompartida.Add(new int[64]);     //Memoria compartida del procesador 1
            //8 bloques [256-380] = 8 bloques x 4 entradas = 32 enteros
            memoriasCompartida.Add(new int[32]);     //Memoria compartida del procesador 2

            // crea una lista de directorios (cada directorio es de un procesador)
            directorios = new List<int[,]>();
            //Directorio procesador 1, 16 bloques x 4 columnas (estado, N0, N1, N2)
            directorios.Add(new int[16, 4]);
            //Directorio procesador 2, 8 bloques x 4 columnas (estado, N0, N1, N2)
            directorios.Add(new int[8, 4]);

            // crea una lista de caches de datos (cada cache es de un nucleo)
            cachesDatos = new List<int[,]>();
            //Cache Nucleo 1, (16 bytes + etiqueta + estado) x 4 columnas (espacios de cache)
            cachesDatos.Add(new int[18,4]);
            //Cache Nucleo 2, (16 bytes + etiqueta + estado) x 4 columnas (espacios de cache)
            cachesDatos.Add(new int[18,4]);
            //Cache Nucleo 3, (16 bytes + etiqueta + estado) x 4 columnas (espacios de cache)
            cachesDatos.Add(new int[18,4]);


            //incializar memorias compartidas, directorios y caches de datos
        }

    }
}
