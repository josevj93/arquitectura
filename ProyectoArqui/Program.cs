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


        //buses
        public static readonly object[] BusDatos = new object[2];
        public static readonly object[] BusInstrucciones = new object[2];
        public static readonly object BusContextos = new object();

        public static Form1 Vista;
        public static Shared shared = new Shared();
        //public static Controladora controladora = new Controladora();

        /*
        public static List<Nucleo> Procesador1 = new List<Nucleo>();
        public static List<Nucleo> Procesador2 = new List<Nucleo>();

        //contador de ciclos de reloj para la ejecucion
        public static int Reloj = 0;

        //public static List<Barrier> barrerasDeReloj = new List<Barrier>();//(3, barrier => Reloj++);
        public static Barrier barreraReloj = new Barrier(3, barrier => TickReloj());

        
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
            Queue<int> q1 = new Queue<int>();
            Queue<int> q2 = new Queue<int>();


            for(int i = 0; i < 1000000; ++i)
            {
                BusDatos[i] = new object();
            }

            for (int i = 0; i < 2; ++i)
            {
                BusInstrucciones[i] = new object();
            }
  
            
            */

            Instruccion instr = new Instruccion
            {
                //35 4 11 4
                CodigoOp = 35,
                RF1 = 4,
                RF2_RD = 11,
                RD_IMM = 4
            };
            Nucleo nucleo = new Nucleo(1, 1);
            shared.memoriasCompartida.ElementAt(0)[2] = 10;
            shared.memoriasCompartida.ElementAt(0)[1] = 15;
            shared.memoriasCompartida.ElementAt(0)[0] = 2;
            //imprime bloque de memoria
            for (int i = 0; i < 4; i++)
            {
                Console.Write("memoria: {0}\n", shared.memoriasCompartida.ElementAt(0)[i]);
            }
            Console.Write("antes del load: {0}\n", nucleo.Registros[11]);
            nucleo.EjecutarLW(instr);
            Console.Write("despues del load: {0}\n", nucleo.Registros[11]);
            Instruccion instr2 = new Instruccion
            {
                //35 4 11 0
                CodigoOp = 35,
                RF1 = 4,
                RF2_RD = 11,
                RD_IMM = 0
            };
            nucleo.EjecutarLW(instr2);
            Console.Write("load con hit: {0}\n", nucleo.Registros[11]);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Vista = new Form1();
            Application.Run(new Form1());
            

        }
    }
}
