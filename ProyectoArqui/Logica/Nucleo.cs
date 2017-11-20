using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    class Nucleo
    {

        public Shared shared;
        public Instruccion IR { get; set; }
        public int PC { get; set; }
        public int[] Registros { get; set; }
        public int IdNucleo;
        public int IdProce;
        /// <summary>
        /// 5 ya que el ultimo es la etiqueta.
        /// </summary>
        //public Instruccion[,] CacheInstrucciones = new Instruccion[5, 4];

        public CacheInstrucciones cacheI = new CacheInstrucciones();


        /// <summary>
        /// 6 ya que el 5 es la
        /// etiqueta y el 6 es el estado.
        /// </summary>
        public int[,] CacheDatos = new int[6, 4];

        public Nucleo(int idn, int idp)
        {
            IdNucleo = idn;
            IdProce = idp;
            shared = new Shared();
            //se inicializan los registros en 0
            Registros = new int[32];
            for (int i = 0; i < 32; i++)
            {
                Registros[i] = 0;
            }
            //se inicializa la cache en 0, con todos los bloques inválidos
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    CacheDatos[i, j] = 0;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                CacheDatos[5, i] = -1;
            }
        }

        //Ejecuta instrucciones del ALU
        public bool ejecutarInstruccion(Instruccion instruccion)
        {
            /*
             sumar cuatro al pc , cada vez que tiene una nueva lectura de instruccion.             
             */
            bool result = false;
            switch (instruccion.CodigoOp)
            {
                case 8:
                    //DADDI: Rx <-- (Ry) + n
                    Registros[instruccion.RF2_RD] = Registros[instruccion.RF1] + instruccion.RD_IMM;
                    break;
                case 32:
                    //DADD: Rx <-- (Ry) + (Rz)
                    Registros[instruccion.RD_IMM] = Registros[instruccion.RF1] + Registros[instruccion.RF2_RD];
                    break;
                case 34:
                    //DSUB: Rx <-- (Ry) - (Rz)
                    Registros[instruccion.RD_IMM] = Registros[instruccion.RF1] - Registros[instruccion.RF2_RD];
                    break;
                case 12:
                    //DMUL: Rx <-- (Ry) * (Rz)
                    Registros[instruccion.RD_IMM] = Registros[instruccion.RF1] * Registros[instruccion.RF2_RD];
                    break;
                case 14:
                    //DDIV: Rx <-- (Ry) / (Rz)
                    Registros[instruccion.RD_IMM] = Registros[instruccion.RF1] / Registros[instruccion.RF2_RD];
                    break;
                case 4:
                    //BEQZ: Si Rx == 0 hace un salto de n*4 (tamaño de instruccion)
                    if (Registros[instruccion.RF1] == 0)
                    {
                        PC = PC + (instruccion.RD_IMM * 4); //cambio el PC a la etiqueta
                    }
                    break;
                case 5:
                    //BNEZ: Si Rx != 0 hace un salto de n*4 (tamaño de instruccion)
                    if (Registros[instruccion.RF1] != 0)
                    {
                        PC = PC + (instruccion.RD_IMM * 4); //cambio el PC a la etiqueta
                    }
                    break;
                case 3:
                    //JAL: R31 <-- PC, PC <-- PC + n
                    Registros[31] = PC;
                    PC = PC + instruccion.RD_IMM;
                    break;
                case 2:
                    //JR: PC <-- (RX)
                    PC = Registros[instruccion.RF1];
                    break;
                case 63:
                    //FIN
                    result = true;
                    break;
                default:
                    //si hay un mal codigo deberia de tirar error
                    break;
            }
            return result;
        }

        //Hay que definir lo de los estados, en este caso estoy proponiendo que: -1 = I, 0 = C, 1 = M para la cache
        //para el directorio sería -1 = U, 0 = C, 1 = M
        public bool EjecutarLW(Instruccion instruccion /*Referencias refMemoria,*/ )
        {
            bool result = false;

            //se obtiene direccion de memoria
            int direccion = instruccion.RF1 + instruccion.RD_IMM;

            //se convierte a bloque y palabra posicion en cache
            int bloque = direccion / 16;
            int palabra = (direccion % 16) / 4;
            int posCache = bloque % 4;

            //se trata de bloquear el caché
            var lockCache = new Lock(CacheDatos);

            //si se pudo bloquear sigue con la lectura
            if (lockCache.HasLock)
            {
                //tick de reloj
                //Controladora.barreraReloj.SignalAndWait();
                //si esta el bloque en mi cache y esta válido
                if (CacheDatos[4, posCache] == bloque && CacheDatos[5, posCache] != -1)
                {
                    //cargar el valor al registro correspondiente
                    Registros[instruccion.RF2_RD] = CacheDatos[palabra, posCache];

                    //restar quantum
                    //quantum.Valor--; LO COMENTE PARA QUE NO DIERA ERRORES
                    result = true;
                    //Controladora.barreraReloj.SignalAndWait();
                }
                else //fallo de cache (no se encuentra el bloque en cache o no esta valido)
                {
                    //tick de reloj
                    //Controladora.barreraReloj.SignalAndWait();

                    //se obtienen los datos del bloque victima
                    int bloqueVictima = CacheDatos[4, posCache];

                    //se debe de revisar el estado del bloque para saber si esta modificado
                    if (CacheDatos[5, posCache] == 1)
                    {
                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();
                        //bloquear el bus de data
                        var lockBusDatos = new Lock(Controladora.BusDatosP1);
                        //si obtuve el bus de data
                        if (lockBusDatos.HasLock)
                        {

                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();

                            //bloquear el directorio casa del bloque victima
                            if (bloqueVictima < 16)
                            {
                                //tick de reloj
                                //Controladora.barreraReloj.SignalAndWait();
                                //si el bloqueVictima es de 0 a 15 significa que hay que revisar el directorio del procesador0
                                var lockDirVictima = new Lock(shared.directorios.ElementAt(0));
                                //se debe de bloquear la memoria para escribir en ella
                                var lockMem = new Lock(shared.memoriasCompartida.ElementAt(0));

                                //se actualiza el valor en memoria del bloque victima
                                for (int i = 0; i < 4; i++)
                                {
                                    shared.memoriasCompartida.ElementAt(0)[((bloqueVictima * 16) / 4) + i] = CacheDatos[i, posCache];
                                }

                                //se actualiza el directorio casa con 0´s y una U en el estado del bloque victima
                                shared.directorios.ElementAt(0)[bloqueVictima, 0] = -1;
                                for (int i = 1; i < 4; i++)
                                {
                                    shared.directorios.ElementAt(0)[bloqueVictima, i] = 0;
                                }

                                //se libera el directorio casa del bloque victima
                                lockDirVictima.Dispose();
                                lockMem.Dispose();
                            }
                            else
                            {
                                //tick de reloj
                                //Controladora.barreraReloj.SignalAndWait();
                                //sino se bloquea el directorio casa del procesador1
                                var lockDirVictima = new Lock(shared.directorios.ElementAt(1));
                                //se debe de bloquear la memoria para escribir en ella
                                var lockMem = new Lock(shared.memoriasCompartida.ElementAt(1));

                                //se actualiza el valor en memoria del bloque victima
                                for (int i = 0; i < 4; i++)
                                {
                                    shared.memoriasCompartida.ElementAt(1)[((bloqueVictima * 16) / 4) + i - 64] = CacheDatos[i, posCache];
                                    //el -64 es porque hay que recordar que esa memoria empieza desde 0 en realidad, aunque en el papel empieza inmediatamente despues de la primera memoria compartida 
                                }
                                //se actualiza el directorio casa con 0´s y una U en el estado del bloque victima
                                shared.directorios.ElementAt(1)[bloqueVictima, 0] = -1;
                                for (int i = 1; i < 4; i++)
                                {
                                    shared.directorios.ElementAt(1)[bloqueVictima, i] = 0;
                                }
                                //se libera el directorio casa del bloque victima
                                lockDirVictima.Dispose();
                                lockMem.Dispose();
                            }
                            //se invalida el estado de la cache del bloque victima
                            CacheDatos[5, posCache] = -1;
                        }
                        //se libera el bus
                        lockBusDatos.Dispose();
                    }
                    else if (CacheDatos[5, posCache] == 0)
                    {
                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();
                        //pone un 0 en el directorio casa del bloque victima
                        //se pregunta cual es el directorio casa
                        if (bloqueVictima < 16)
                        {
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            var lockDirVictima = new Lock(shared.directorios.ElementAt(0));
                            shared.directorios.ElementAt(0)[bloqueVictima, IdNucleo] = 0;
                            lockDirVictima.Dispose();
                        }
                        else
                        {
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            var lockDirVictima = new Lock(shared.directorios.ElementAt(1));
                            shared.directorios.ElementAt(1)[bloqueVictima, IdNucleo] = 0;
                            lockDirVictima.Dispose();
                        }
                    }

                    //se bloquea el bus
                    var lockBusDatos1 = new Lock(Controladora.BusDatosP1);
                    //se bloquea el directorio casa del bloque fuente
                    if (bloque < 16) //significa que el directorio casa es el del procesador 0
                    {
                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();
                        var lockDirFuente = new Lock(shared.directorios.ElementAt(0));

                        //se debe de preguntar en el directorio si el bloque fuente está modificado en alguna otra caché
                        if (shared.directorios.ElementAt(0)[bloque, 0] == 1)
                        {
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            //se debe de preguntar en cual caché está modificado, además se debe de bloquear esa caché
                            if (shared.directorios.ElementAt(0)[bloque, 1] == 1) //Nucleo0
                            {
                                //tick de reloj
                                //Controladora.barreraReloj.SignalAndWait();
                                var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(0));

                                //se debe de mandar a escribir a memoria
                                //se debe de bloquear la memoria para escribir en ella
                                var lockMem = new Lock(shared.memoriasCompartida.ElementAt(0));

                                //se actualiza el valor en memoria del bloque
                                for (int i = 0; i < 4; i++)
                                {
                                    shared.memoriasCompartida.ElementAt(0)[((bloque * 16) / 4) + i] = shared.cachesDatos.ElementAt(0)[i, posCache];
                                }
                                //se actualiza el directorio casa con 0´s y una U en el estado del bloque
                                shared.directorios.ElementAt(0)[bloque, 0] = -1;
                                for (int i = 1; i < 4; i++)
                                {
                                    shared.directorios.ElementAt(0)[bloque, i] = 0;
                                }
                                //se libera la memoria
                                lockMem.Dispose();
                                lockCacheFuente.Dispose();
                            }
                            else if (shared.directorios.ElementAt(0)[bloque, 2] == 1) //Nucleo 1
                            {
                                //tick de reloj
                                //Controladora.barreraReloj.SignalAndWait();
                                var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(1));

                                //se debe de mandar a escribir a memoria
                                //se debe de bloquear la memoria para escribir en ella
                                var lockMem = new Lock(shared.memoriasCompartida.ElementAt(0));

                                //se actualiza el valor en memoria del bloque
                                for (int i = 0; i < 4; i++)
                                {
                                    shared.memoriasCompartida.ElementAt(0)[((bloque * 16) / 4) + i] = shared.cachesDatos.ElementAt(1)[i, posCache];
                                }
                                //se actualiza el directorio casa con 0´s y una U en el estado del bloque
                                shared.directorios.ElementAt(0)[bloque, 0] = -1;
                                for (int i = 1; i < 4; i++)
                                {
                                    shared.directorios.ElementAt(0)[bloque, i] = 0;
                                }
                                //se libera la memoria
                                lockMem.Dispose();
                                lockCacheFuente.Dispose();
                            }
                            else //Nucleo2
                            {
                                //tick de reloj
                                //Controladora.barreraReloj.SignalAndWait();
                                var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(2));

                                //se debe de mandar a escribir a memoria
                                //se debe de bloquear la memoria para escribir en ella
                                var lockMem = new Lock(shared.memoriasCompartida.ElementAt(0));

                                //se actualiza el valor en memoria del bloque
                                for (int i = 0; i < 4; i++)
                                {
                                    shared.memoriasCompartida.ElementAt(0)[((bloque * 16) / 4) + i] = shared.cachesDatos.ElementAt(2)[i, posCache];
                                }
                                //se actualiza el directorio casa con 0´s y una U en el estado del bloque
                                shared.directorios.ElementAt(0)[bloque, 0] = -1;
                                for (int i = 1; i < 4; i++)
                                {
                                    shared.directorios.ElementAt(0)[bloque, i] = 0;
                                }
                                //se libera la memoria
                                lockMem.Dispose();
                                lockCacheFuente.Dispose();

                            }
                        }
                        lockDirFuente.Dispose();
                        //si no es porque está compartido o invalido y se le puede caer sin problema
                    }
                    else //directorio casa es del procesador 1
                    {
                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();
                        var lockDirFuente = new Lock(shared.directorios.ElementAt(1));

                        //se debe de preguntar en el directorio si el bloque fuente está modificado en alguna otra caché
                        if (shared.directorios.ElementAt(1)[bloque, 0] == 1)
                        {
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            //se debe de preguntar en cual caché está modificado, además se debe de bloquear esa caché
                            if (shared.directorios.ElementAt(1)[bloque, 1] == 1) //Nucleo0
                            {
                                //tick de reloj
                                //Controladora.barreraReloj.SignalAndWait();
                                var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(0));

                                //se debe de mandar a escribir a memoria
                                //se debe de bloquear la memoria para escribir en ella
                                var lockMem = new Lock(shared.memoriasCompartida.ElementAt(1));

                                //se actualiza el valor en memoria del bloque
                                for (int i = 0; i < 4; i++)
                                {
                                    shared.memoriasCompartida.ElementAt(1)[((bloque * 16) / 4) / + i - 64] = shared.cachesDatos.ElementAt(0)[i, posCache];
                                    //el -64 es porque hay que recordar que esa memoria empieza desde 0 en realidad, aunque en el papel empieza inmediatamente despues de la primera memoria compartida 
                                }
                                //se actualiza el directorio casa con 0´s y una U en el estado del bloque
                                shared.directorios.ElementAt(1)[bloque, 0] = -1;
                                for (int i = 1; i < 4; i++)
                                {
                                    shared.directorios.ElementAt(1)[bloque, i] = 0;
                                }
                                //se libera la memoria
                                lockMem.Dispose();
                                lockCacheFuente.Dispose();
                            }
                            else if (shared.directorios.ElementAt(1)[bloque, 2] == 1) //Nucleo 1
                            {
                                //tick de reloj
                                //Controladora.barreraReloj.SignalAndWait();
                                var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(1));

                                //se debe de mandar a escribir a memoria
                                //se debe de bloquear la memoria para escribir en ella
                                var lockMem = new Lock(shared.memoriasCompartida.ElementAt(1));

                                //se actualiza el valor en memoria del bloque
                                for (int i = 0; i < 4; i++)
                                {
                                    shared.memoriasCompartida.ElementAt(1)[((bloque * 16) + i) / 4 - 64] = shared.cachesDatos.ElementAt(1)[i, posCache];
                                    //el -64 es porque hay que recordar que esa memoria empieza desde 0 en realidad, aunque en el papel empieza inmediatamente despues de la primera memoria compartida 
                                }
                                //se actualiza el directorio casa con 0´s y una U en el estado del bloque
                                shared.directorios.ElementAt(1)[bloque, 0] = -1;
                                for (int i = 1; i < 4; i++)
                                {
                                    shared.directorios.ElementAt(1)[bloque, i] = 0;
                                }
                                //se libera la memoria
                                lockMem.Dispose();
                                lockCacheFuente.Dispose();
                            }
                            else //Nucleo2
                            {
                                //tick de reloj
                                //Controladora.barreraReloj.SignalAndWait();
                                var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(2));

                                //se debe de mandar a escribir a memoria
                                //se debe de bloquear la memoria para escribir en ella
                                var lockMem = new Lock(shared.memoriasCompartida.ElementAt(1));

                                //se actualiza el valor en memoria del bloque
                                for (int i = 0; i < 4; i++)
                                {
                                    shared.memoriasCompartida.ElementAt(1)[((bloque * 16) / 4) / + i - 64] = shared.cachesDatos.ElementAt(2)[i, posCache];
                                    //el -64 es porque hay que recordar que esa memoria empieza desde 0 en realidad, aunque en el papel empieza inmediatamente despues de la primera memoria compartida 
                                }
                                //se actualiza el directorio casa con 0´s y una U en el estado del bloque 
                                shared.directorios.ElementAt(1)[bloque, 0] = -1;
                                for (int i = 1; i < 4; i++)
                                {
                                    shared.directorios.ElementAt(1)[bloque, i] = 0;
                                }
                                //se libera la memoria
                                lockMem.Dispose();
                                lockCacheFuente.Dispose();

                            }
                        }
                        lockDirFuente.Dispose();
                        //si no es porque está compartido o invalido y se le puede caer sin problema
                    }
                    //Ahora que no hay conflictos con los estados se puede finalmente escribir en la cache para la lectura
                    if (bloque < 16) //significa que el dato se encuentra en la memoria del procesador 0
                    {
                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();
                        var lockMem = new Lock(shared.memoriasCompartida.ElementAt(0));
                        var lockDirCasa = new Lock(shared.directorios.ElementAt(0));
                        //subo el bloque a cache
                        for (int i = 0; i < 4; i++)
                        {
                            //AQUI NO ESTÁ LEYENDO BIEN MEMORIA PORQUE DICE QUE HAY 0´S
                            CacheDatos[i, posCache] = shared.memoriasCompartida.ElementAt(0)[((bloque * 16) / 4) + i];
                            Console.Write("CacheDatos: {0}\n", CacheDatos[i, posCache]);
                        }
                        //actualizo el valor del bloque y el del estado
                        CacheDatos[4, posCache] = bloque;
                        CacheDatos[5, posCache] = 0; //0 por estar compartido

                        //actualizo el directorio casa
                        //pongo un 0 en estado, que significa que esta compartido
                        shared.directorios.ElementAt(0)[bloque, 0] = 0;
                        shared.directorios.ElementAt(0)[bloque, IdNucleo] = 1;

                        lockMem.Dispose();
                        lockDirCasa.Dispose();
                    }
                    else //esta en la memoria del procesador 1
                    {
                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();
                        var lockMem = new Lock(shared.memoriasCompartida.ElementAt(0));
                        var lockDirCasa = new Lock(shared.directorios.ElementAt(0));
                        //subo el bloque a cache
                        for (int i = 0; i < 4; i++)
                        {
                            CacheDatos[i, posCache] = shared.memoriasCompartida.ElementAt(0)[((bloque * 16) /4) / + i - 64];
                            //-64 porque esta es la memoria del procesador 0, la cual realmente comienza desde 0
                        }

                        //actualizo el directorio casa
                        //pongo un 0 en estado, que significa que esta compartido
                        shared.directorios.ElementAt(1)[bloque, 0] = 0;
                        shared.directorios.ElementAt(1)[bloque, IdNucleo] = 1;

                        lockMem.Dispose();
                        lockDirCasa.Dispose();
                    }
                    //finalmente puedo leer
                    //agrego al registro lo que hay en caché
                    Registros[instruccion.RF2_RD] = CacheDatos[palabra, posCache];
                    Console.Write("Registro: {0}\n", Registros[instruccion.RF2_RD]);
                    //restar quantum
                    //quantum.Valor--;
                    //ExitoAnterior = true
                    result = true;
                    //4(1 + 5 +1) cargando bloque de memoria a cache.
                    //IncrementarReloj(28);
                    //libero el bus
                    lockBusDatos1.Dispose();
                }
                //libero la cache
                lockCache.Dispose();
            }
            else
            {
                //sino se pudo obtener el cache hay tick de reloj
                //Controladora.barreraReloj.SignalAndWait();
            }
            return result;
        }

        private void IncrementarReloj(int limiteSuperior)
        {
            for (int i = 0; i < limiteSuperior; i++)
                Controladora.barreraReloj.SignalAndWait();
        }

        public void fetch()
        {
            //pregunta en la cache si esta en el bloque i, 
            //si si está entonces, busca y carga la instruccion al IR,
            //sino esta lo traigo de la memoria de instrucciones (el bloque).




        }

        //devuelve true si finalizo el Hilillo
        public void ejecutar(object o)
        {

            //carga los datos compartidos
            shared = (Shared)o;

            //hilillo que se va a ejecutar en el nucleo
            ContextoHilillo proximo;

            do
            {
                //bloquea la cola de contextos del Procesador donde esta el nucleo
                lock (shared.colasContextos.ElementAt(IdProce))
                {
                    //carga el hilo de la cola de hilos pendientes
                    proximo = shared.colasContextos.ElementAt(IdProce).Dequeue();
                }
                if (proximo.Finalizado)
                {
                    lock (shared.hilosFinalizados.ElementAt(IdProce))
                    {
                        //si el hilo esta finalizado, lo guarda en la lista de hilos finalizados
                        shared.hilosFinalizados.ElementAt(IdProce).Add(proximo);
                    }
                }

            } while (proximo.Finalizado);

            //REVISAR EL CASO EN QUE TODOS LOS HILOS ESTEN FINALIZADOS Y QUEDE VACIA LA COLA PORQUE SE PUEDE ENCLICLAR

            //si no es un hilo finalizado, inicializa registros y pc del contexto
            int numInst;

            lock (shared)
            {
                numInst = shared.quantum; // Trae el quantum definido para ejecutar
            }

            PC = proximo.PC;
            Registros = proximo.Registros;

            while (numInst > 0 && !proximo.Finalizado)
            {

                //saca palabra y bloque del PC

                int bloque = PC / 16;
                int palabra = (PC % 16) / 4;

                bool hit = false;

                //comienza fetch

                int i = 0;
                while (i < 4 && cacheI.etiquetas[i] != bloque)
                {
                    if (cacheI.etiquetas[i] == bloque)
                    {
                        hit = true;
                    }

                }
                int k = 0;
                while (k < 4 && cacheI.etiquetas[k] != bloque)
                {
                    if (cacheI.etiquetas[k] == bloque)
                    {
                        hit = true;
                    }
                    k++;
                }

                if (!hit)
                {

                    //no hubo hit y va a memoria a cargar bloque

                    for (int j = 0; j < 4; ++j)
                    {

                        Instruccion nueva = new Instruccion();

                        lock (shared.memoriaInstrucciones.ElementAt(IdProce))
                        {
                            nueva.CodigoOp = shared.memoriaInstrucciones.ElementAt(IdProce)[bloque + (j * 4)];
                            nueva.RF1 = shared.memoriaInstrucciones.ElementAt(IdProce)[bloque + (j * 4) + 1];
                            nueva.RF2_RD = shared.memoriaInstrucciones.ElementAt(IdProce)[bloque + (j * 4) + 2];
                            nueva.RD_IMM = shared.memoriaInstrucciones.ElementAt(IdProce)[bloque + (j * 4) + 3];
                        }

                        cacheI.bloqueInstruccion[bloque % 4, j] = nueva;


                    }

                    //Guarda la etiqueta del bloque subido
                    cacheI.etiquetas[bloque % 4] = bloque;

                }

                //Carga instruccion al IR de la cache

                IR = cacheI.bloqueInstruccion[bloque, palabra];

                PC += 4; //COMPROBAR SI EL INCREMENTO ESTA BIEN

                //ejecuta instruccion

                if (ejecutarInstruccion(IR))
                {
                    proximo.Finalizado = true;

                }

                numInst--;

            }

            if (proximo.Finalizado)
            {
                lock (shared.hilosFinalizados.ElementAt(IdProce))
                {
                    shared.hilosFinalizados.ElementAt(IdProce).Add(proximo);
                }
            }
            else
            {
                lock (shared.colasContextos.ElementAt(IdProce))
                {
                    shared.colasContextos.ElementAt(IdProce).Enqueue(proximo);
                }
            }

        }
    }
}
