﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoArqui.Logica;
using System.Threading;

namespace ProyectoArqui
{
    class Controladora
    {

        public static List<Nucleo> Procesador1 = new List<Nucleo>();
        public static List<Nucleo> Procesador2 = new List<Nucleo>();

        //contador de ciclos de reloj para la ejecucion
        public static int Reloj = 0;

        //public static List<Barrier> barrerasDeReloj = new List<Barrier>();//(3, barrier => Reloj++);
        public static Barrier barreraReloj = new Barrier(3, barrier => TickReloj());

        //buses
        public static readonly object BusDatosP1 = new object();
        public static readonly object BusInstruccionesP1 = new object();
        public static readonly object BusDatosP2 = new object();
        public static readonly object BusInstruccionesP2 = new object();

        //cola que tendra los hilillos listos para ser ejecutados en los nucleos
        public static Queue<Hilillo> ColaHilillos = new Queue<Hilillo>();

        //cola que tendra los contextos de los hilillos del Procesador 1
        public static Queue<ContextoHilillo> ColaContextosP1 = new Queue<ContextoHilillo>();

        //cola que tendra los contextos de los hilillos del Procesador 2
        public static Queue<ContextoHilillo> ColaContextosP2 = new Queue<ContextoHilillo>();

        //16 bloques [0-252] = 16 bloques x 4 entradas = 64 enteros
        public static int[] MCP1 = new int[64]; //Memoria compartida del procesador 1
        //24 bloques [256-636] (96 instrucciones) = 24 bloques x 16 entradas = 384 enteros
        public static int[] MIP1 = new int[384]; //Memoria de instrucciones del procesador 1
        //8 bloques [256-380] = 8 bloques x 4 entradas = 32 enteros
        public static int[] MCP2 = new int[32]; //Memoria compartida del procesador 2
        //16 bloques [128-380] (64 instrucciones) = 16 bloques x 16 enteros = 256 enteros
        public static int[] MIP2 = new int[256]; //Memoria de instrucciones del procesador 2

        //Nucleo 1
        //caché de datos, matriz de 6 filas (4 palabras + 1 para etiqueta de bloque + 1 para estado) x 4 columnas de enteros
        public static int[,] CacheN1 = new int[6, 4];
        //cache de instrucciones, cada celda tiene una instruccion
        public static Instruccion[,] CacheInstN1 = new Instruccion[5, 4];
        //contador de las instrucciones completas permitidas por hilo o simplemente, el quantum
        public static Quantum QuantumN1 = new Quantum();

        //Nucleo 2
        //caché de datos, matriz de 6 filas (4 palabras + 1 para etiqueta de bloque + 1 para estado) x 4 columnas de enteros
        public static int[,] CacheN2 = new int[6, 4];
        //cache de instrucciones, cada celda tiene una instruccion
        public static Instruccion[,] CacheInstN2 = new Instruccion[5, 4];
        //contador de las instrucciones completas permitidas por hilo o simplemente, el quantum
        public static Quantum QuantumN2 = new Quantum();

        //Nucleo 3
        //caché de datos, matriz de 6 filas (4 palabras + 1 para etiqueta de bloque + 1 para estado) x 4 columnas de enteros
        public static int[,] CacheN3 = new int[6, 4];
        //cache de instrucciones, cada celda tiene una instruccion
        public static Instruccion[,] CacheInstN3 = new Instruccion[5, 4];
        //contador de las instrucciones completas permitidas por hilo o simplemente, el quantum
        public static Quantum QuantumN3 = new Quantum();

        //Directorio procesador 1, 16 bloques x 4 columnas (estado, N0, N1, N2)
        public static int[,] DirectorioP1 = new int[16, 4];
        //Directorio procesador 2, 8 bloques x 4 columnas (estado, N0, N1, N2)
        public static int[,] DirectorioP2 = new int[8, 4];

        //funcion para simular el tick de reloj
        static void TickReloj()
        {
            Reloj++;
            //theForm.AppendReloj(Reloj.ToString());
        }

        public static void cargar(List<List<int>> hilillosP1, List<List<int>> hilillosP2)
        {
            //Carga las instrucciones de los hilillos en la memoria de instrucciones de cada procesador
            int c = 0;

            for (int i = 0; i < hilillosP1.Count(); ++i)
            {

                ColaContextosP1.Enqueue(new ContextoHilillo(i, c));
                foreach (int integer in hilillosP1.ElementAt(i))
                {
                    MIP1[c++] = integer;
                }
            }

            c = 0;

            for (int i = 0; i < hilillosP2.Count(); ++i)
            {
                ColaContextosP2.Enqueue(new ContextoHilillo(i, c));
                foreach (int integer in hilillosP2.ElementAt(i))
                {
                    MIP2[c++] = integer;
                }
            }

        }

        public static void cargar2 (List<string> hilillo, int numero)
        {
            //Carga las instrucciones de los hilillos en la memoria de instrucciones de cada procesador
            int c = 0;

            if (numero % 2 == 0)
            {
                for (int i = 0; i < hilillo.Count()-1; ++i)
                {
                    ColaContextosP1.Enqueue(new ContextoHilillo(i, ColaContextosP1.Count));
                    foreach (char integer in hilillo.ElementAt(i))
                    {
                        MIP1[c++] = Convert.ToInt32(integer);
                    }
                }




            }
            else
            {
                c = 0;

                for (int i = 0; i < hilillo.Count()-1; ++i)
                {
                    ColaContextosP2.Enqueue(new ContextoHilillo(i, c));
                    foreach (char integer in hilillo.ElementAt(i))
                    {
                        MIP2[c++] = Convert.ToInt32(integer);
                    }
                }


            }




        }


    }
}