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

        public Shared shared = new Shared();


        //contador de ciclos de reloj para la ejecucion
        public static int Reloj = 0;

        //public static List<Barrier> barrerasDeReloj = new List<Barrier>();//(3, barrier => Reloj++);
        public static Barrier barreraReloj = new Barrier(3, barrier => TickReloj());

        //buses
        public static readonly object BusDatosP1 = new object();
        public static readonly object BusInstruccionesP1 = new object();
        public static readonly object BusDatosP2 = new object();
        public static readonly object BusInstruccionesP2 = new object();
        
        
        //funcion para simular el tick de reloj
        static void TickReloj()
        {
            Reloj++;
            //theForm.AppendReloj(Reloj.ToString());
        }

        public void setQuantum(int q)
        {
            lock (shared)
            {
                shared.quantum = q;
            }
        }


        public void cargar(List<List<int>> hilillos)
        {
            //Carga las instrucciones de los hilillos en la memoria de instrucciones de cada procesador
            int cont = 0;
            int contadorP1 = 0, contadorP2 = 0;

            foreach (List<int> archivo in hilillos)
            {

                if (cont % 2 == 0)
                {
                    shared.colasContextos.ElementAt(0).Enqueue(new ContextoHilillo(contadorP1));
                    shared.hilosTotales[0]++;
                    foreach (int dato in archivo)
                    {
                        shared.memoriaInstrucciones.ElementAt(0)[contadorP1++] = dato;
                    }

                }

                else
                {
                    shared.colasContextos.ElementAt(1).Enqueue(new ContextoHilillo(contadorP2));
                    shared.hilosTotales[1]++;
                    foreach (int dato in archivo)
                    {
                        shared.memoriaInstrucciones.ElementAt(1)[contadorP2++] = dato;
                    }


                }
                cont++;
            }

        }


        public void iniciar()
        {
            //Inicia el procesador 0 con 2 nucleos
            Procesador p0 = new Procesador(0,2);
            Procesador p1 = new Procesador(1,1);

            Thread tp0 = new Thread(p0.iniciar);
            tp0.Start(shared);

            Thread tp1 = new Thread(p1.iniciar);
            tp1.Start(shared);

            tp0.Join();
            tp1.Join();

        }




    }
}