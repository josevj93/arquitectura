using System;
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

        
        //24 bloques [256-636] (96 instrucciones) = 24 bloques x 16 entradas = 384 enteros
        public static int[] MIP1 = new int[384]; //Memoria de instrucciones del procesador 1
        
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

        public static int Quant;

        //funcion para simular el tick de reloj
        static void TickReloj()
        {
            Reloj++;
            //theForm.AppendReloj(Reloj.ToString());
        }


        public static void cargar(List<List<int>> hilillos)
        {
            //Carga las instrucciones de los hilillos en la memoria de instrucciones de cada procesador
            int cont = 0;
            int contadorP1 = 0, contadorP2 = 0;

            foreach (List<int> archivo in hilillos)
            {

                if (cont % 2 == 0)
                {
                    ColaContextosP1.Enqueue(new ContextoHilillo(contadorP1));

                    foreach (int dato in archivo)
                    {
                        MIP1[contadorP1++] = dato;
                    }

                }

                else
                {
                    ColaContextosP2.Enqueue(new ContextoHilillo(contadorP2));
                    foreach (int dato in archivo)
                    {
                        MIP2[contadorP2++] = dato;
                    }


                }
                cont++;
            }


        }


        public static void iniciar()
        {




        }




    }
}