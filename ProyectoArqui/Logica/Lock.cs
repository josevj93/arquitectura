using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    public class Lock : IDisposable
    {
        private object locked;

        public bool HasLock { get; private set; }

        public Lock(object obj)
        {
            if (!Monitor.TryEnter(obj)) return;
            HasLock = true;
            locked = obj;
        }

        public void Dispose()
        {
            if (!HasLock) return;

            Monitor.Exit(locked);
            locked = null;
            HasLock = false;
        }
    }
}
