using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    public class Shared
    {

        public List<int[]> memoriasCompartida;

        public List<int[,]> directorios;

        public List<int[,]> cachesDatos;

        public List<Queue<ContextoHilillo>> colasContextos;

        public List<List<ContextoHilillo>> hilosFinalizados;

        public List<int[]> memoriaInstrucciones;


        public Shared()
        {
            // crea una lista de memorias compartidas (cada memoria es de un procesador)
            memoriasCompartida = new List<int[]>();
            //16 bloques [0-252] = 16 bloques x 4 entradas = 64 enteros
            memoriasCompartida.Add(new int[64]);     //Memoria compartida del procesador 1
            //8 bloques [256-380] = 8 bloques x 4 entradas = 32 enteros
            memoriasCompartida.Add(new int[32]);     //Memoria compartida del procesador 2

            //24 bloques [256-636] (96 instrucciones) = 24 bloques x 16 entradas = 384 enteros
            memoriaInstrucciones.Add(new int[384]); //Memoria de instrucciones del procesador 1

            //16 bloques [128-380] (64 instrucciones) = 16 bloques x 16 enteros = 256 enteros
            memoriaInstrucciones.Add(new int[256]); //Memoria de instrucciones del procesador 2

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

            //cola que tendra los contextos de los hilillos del Procesador 1
            colasContextos.Add(new Queue<ContextoHilillo>());

            //cola que tendra los contextos de los hilillos del Procesador 2
            colasContextos.Add(new Queue<ContextoHilillo>());

            //lista de hilos que han finalizado en el Procesador 1
            hilosFinalizados.Add(new List<ContextoHilillo>());

            //lista de hilos que han finalizado en el Procesador 2
            hilosFinalizados.Add(new List<ContextoHilillo>());

            //incializar memorias compartidas, directorios y caches de datos
        }

    }
}
