using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui
{
    class Hilo
    {

        public Shared shared;
        Queue<int> queue;
        private int id;

        public Hilo(int n, Queue<int> q)
        {
            id = n;
            //shared = s;
            queue = q;
        }

        public void insertar(int n)
        {
            lock (shared.lista)
            {
                shared.lista.Add(n);
            }
            
        }

        /*
        public void imprimir()
        {
            Console.Write("Hilo > " + id + ": ");
            foreach (String s in lista)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine("");
        }
        */
        public void iniciar(object o)
        {
            shared =((Shared)o);
            while(queue.Count() > 0)
            {
                insertar(queue.Dequeue());
            }

            //imprimir();

        }
    }
}
