using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    public class Procesador
    {

        int id;

        Shared shared;

        List<Nucleo> nucleos;

        Queue<ContextoHilillo> colaHilos;

        List<ContextoHilillo> hilosFinalizados;

        Procesador(int id, int nNucleos, Queue<ContextoHilillo> cch)
        {
            //asigna el id al Procesador
            this.id = id;

            //Crea todos los nucleos de este Procesador
            for(int i=0; i < nNucleos; ++i)
            {
                nucleos.Add(new Nucleo(i,id));
            }

            colaHilos = cch;
        }

        void iniciar(Object o)
        {
            //carga los datos compartidos
            shared = (Shared)o;

            //mientras la cola de Hilos tenga hilos pendientes
            while (colaHilos.Count() < 0)
            {

                foreach(Nucleo nucleo in nucleos)
                {
                    if(colaHilos.Count() < 0)
                    {

                        ContextoHilillo proximo = colaHilos.Dequeue();

                        if (proximo.Finalizado)
                        {
                            //si el hilo termino lo carga en la lista de finalizados
                            hilosFinalizados.Add(proximo);
                        }
                        else
                        {
                            //sino, lo carga en el nucleo

                            colaHilos.Enqueue(nucleo.ejecutar(proximo));

                        }



                    }


                }
                

            }

        }

    }
}
