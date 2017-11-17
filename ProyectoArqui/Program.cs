using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ProyectoArqui.Logica;

namespace ProyectoArqui
{
    static class Program
    {
        //asdasdadsasdadasd
        //comentario tuanis
        public static Form1 Vista;
        public static Controladora controladora = new Controladora();

        /*
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

        //cola que tendra los contextos de los hilillos
        public static Queue<ContextoHilillo> ColaContextos = new Queue<ContextoHilillo>();

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
        
        */

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            Queue<String> q1 = new Queue<String>();
            Queue<String> q2 = new Queue<String>();

            q1.Enqueue("Hola");
            q1.Enqueue("mundo");
            q1.Enqueue("1");

            q2.Enqueue("Soy");
            q2.Enqueue("Hilo");
            q2.Enqueue("2");

            Shared s = new Shared();
            s.lista = new List<String>();

            Hilo h1 = new Hilo(0, s,q1);
            Hilo h2 = new Hilo(1, s,q2);

            Thread t1 = new Thread(new ThreadStart(h1.iniciar));
            t1.Start();
            Thread t2 = new Thread(new ThreadStart(h2.iniciar));
            t2.Start();
            */

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Vista = new Form1();
            Application.Run(new Form1());
        }
    }
}
