using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProyectoArqui.Logica
{
    public class Procesador
    {

        int id;

        Shared shared;

        List<Nucleo> nucleos = new List<Nucleo>();

        public Procesador(int id, int nNucleos)
        {
            //asigna el id al Procesador
            this.id = id;

            //Crea todos los nucleos de este Procesador
            for (int i = 0; i < nNucleos; ++i)
            {
                nucleos.Add(new Nucleo(i, id));
            }

        }


        public void iniciar(Object o)
        {
            //carga los datos compartidos
            shared = (Shared)o;

            Thread[] hilos = new Thread[nucleos.Count()];


            for (int i = 0; i < nucleos.Count(); ++i)
            {
                Thread t = new Thread(nucleos[i].ejecutar);
                hilos[i] = t;
            }

            for (int i = 0; i < nucleos.Count(); ++i)
            {
                hilos[i].Start(shared);
            }

            foreach (var t in hilos)
            {
                t.Join();
            }



        /*

        int hilosPendientes;

        lock (Program.BusContextos)
        {
            hilosPendientes = shared.colasContextos.ElementAt(id).Count();
        }

        //mientras la cola de Hilos tenga hilos pendientes
        while (hilosPendientes > 0)
        {
            int a = 0;
            foreach(Nucleo nucleo in nucleos)
            {

                lock (Program.BusContextos)
                {
                    hilosPendientes = shared.colasContextos.ElementAt(id).Count();
                }

                if (hilosPendientes > 0)
                {

                    Thread t = new Thread(nucleo.ejecutar);
                    t.Start(shared);

                    t.Join();
                }


            }


        }
        */
    }

}
}
