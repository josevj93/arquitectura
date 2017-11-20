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

        public int quantum { get; set; }

        public int[] hilosTotales { get; set; }
        
        /// <summary>
        /// se va a inicializar todo con 0´s
        /// </summary>
        public Shared()
        {

            hilosTotales = new int[2];

            // crea una lista de memorias compartidas (cada memoria es de un procesador)
            memoriasCompartida = new List<int[]>();
            //16 bloques [0-252] = 16 bloques x 4 entradas = 64 enteros
            memoriasCompartida.Add(new int[64]);     //Memoria compartida del procesador 1
            for (int i = 0; i < 64; i++)
            {
                memoriasCompartida.ElementAt(0)[i] = 0;
            }
            //8 bloques [256-380] = 8 bloques x 4 entradas = 32 enteros
            memoriasCompartida.Add(new int[32]);     //Memoria compartida del procesador 2
            for (int i = 0; i < 32; i++)
            {
                memoriasCompartida.ElementAt(1)[i] = 0;
            }

            // crea una lista de memorias de instrucciones (cada memoria es de un procesador)
            memoriaInstrucciones = new List<int[]>();
            //24 bloques [256-636] (96 instrucciones) = 24 bloques x 16 entradas = 384 enteros
            memoriaInstrucciones.Add(new int[384]); //Memoria de instrucciones del procesador 1
            for(int i = 0; i < 384; i++)
            {
                memoriaInstrucciones.ElementAt(0)[i] = 0;
            }
            //16 bloques [128-380] (64 instrucciones) = 16 bloques x 16 enteros = 256 enteros
            memoriaInstrucciones.Add(new int[256]); //Memoria de instrucciones del procesador 2
            for (int i = 0; i < 256; i++)
            {
                memoriaInstrucciones.ElementAt(1)[i] = 0;
            }

            // crea una lista de directorios (cada directorio es de un procesador)
            //recordemos que para inicializar hay que poner el estado en Uncached = -1
            directorios = new List<int[,]>();
            //Directorio procesador 1, 16 bloques x 4 columnas (estado, N0, N1, N2)
            directorios.Add(new int[16, 4]);
            for(int i = 0; i < 16; i++)
            {
                for(int j = 1; j < 4; j++)
                {
                    directorios.ElementAt(0)[i, j] = 0;
                }
            }
            for (int i = 0; i < 16; i++)
            {
                directorios.ElementAt(0)[i, 0] = -1;
            }
            //Directorio procesador 2, 8 bloques x 4 columnas (estado, N0, N1, N2)
            directorios.Add(new int[8, 4]);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    directorios.ElementAt(1)[i, j] = 0;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                directorios.ElementAt(1)[i, 0] = -1;
            }

            // crea una lista de caches de datos (cada cache es de un nucleo)
            //se inicializa la cache en 0, con todos los bloques inválidos = -1
            cachesDatos = new List<int[,]>();
            //queda la duda sobre si son 18 o 6
            //Cache Nucleo 1, (4 enteros + etiqueta + estado) x 4 columnas (espacios de cache)
            cachesDatos.Add(new int[6,4]);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cachesDatos.ElementAt(0)[i, j] = 0;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                cachesDatos.ElementAt(0)[5, i] = -1;
            }
            //Cache Nucleo 2, (4 enteros + etiqueta + estado) x 4 columnas (espacios de cache)
            cachesDatos.Add(new int[6,4]);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cachesDatos.ElementAt(1)[i, j] = 0;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                cachesDatos.ElementAt(1)[5, i] = -1;
            }
            //Cache Nucleo 3, (4 enteros + etiqueta + estado) x 4 columnas (espacios de cache)
            cachesDatos.Add(new int[6,4]);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cachesDatos.ElementAt(2)[i, j] = 0;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                cachesDatos.ElementAt(2)[5, i] = -1;
            }

            // crea una lista de colas de contextos (cada cola de contextos es de un procesador)
            colasContextos = new List<Queue<ContextoHilillo>>();
            //cola que tendra los contextos de los hilillos del Procesador 1
            colasContextos.Add(new Queue<ContextoHilillo>());
            //cola que tendra los contextos de los hilillos del Procesador 2
            colasContextos.Add(new Queue<ContextoHilillo>());

            // crea una lista de listas de contextos que finalizaron (cada lista de contextos es de un procesador)
            hilosFinalizados = new List<List<ContextoHilillo>>();
            //lista de hilos que han finalizado en el Procesador 1
            hilosFinalizados.Add(new List<ContextoHilillo>());
            //lista de hilos que han finalizado en el Procesador 2
            hilosFinalizados.Add(new List<ContextoHilillo>());
            

            //incializar memorias compartidas, directorios y caches de datos
        }

    }
}
