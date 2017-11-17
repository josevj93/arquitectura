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
        Queue<String> queue;
        private int id;

        public Hilo(int n, Shared s, Queue<String> q)
        {
            id = n;
            shared = s;
            queue = q;
        }

        public void insertar(String s)
        {
            lock (shared)
            {
                shared.lista.Add(s);
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
        public void iniciar()
        {
            if(queue.Count() > 0)
            {
                insertar(queue.Dequeue());
            }

            //imprimir();

        }
    }
}
