﻿using System;
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
        public int[,] cacheDatos = new int[6, 4];

        public Nucleo(int idn, int idp)
        {
            IdNucleo = idn;
            IdProce = idp;
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
                    cacheDatos[i, j] = 0;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                cacheDatos[5, i] = -1;
            }
        }

        public Nucleo(int idn, int idp, Controladora controladora)
        {
            IdNucleo = idn;
            IdProce = idp;
            shared = controladora.shared;
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
                    cacheDatos[i, j] = 0;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                cacheDatos[5, i] = -1;
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
                case 35:
                    EjecutarLW(instruccion, shared.cachesDatos.ElementAt(IdNucleo));
                    break;
                case 43:
                    EjecutarSW(instruccion, shared.cachesDatos.ElementAt(IdNucleo));
                    break;
                default:
                    //si hay un mal codigo deberia de tirar error
                    break;
            }
            return result;
        }

        //Hay que definir lo de los estados, en este caso estoy proponiendo que: -1 = I, 0 = C, 1 = M para la cache
        //para el directorio sería -1 = U, 0 = C, 1 = M
        public bool EjecutarLW(Instruccion instruccion, int[,] CacheDatos)
        {
            bool result = false;

            //se obtiene direccion de memoria
            int direccion = instruccion.RF1 + instruccion.RD_IMM;

            //se convierte a bloque y palabra posicion en cache
            int bloque = direccion / 16;
            int palabra = (direccion % 16) / 4;
            int posCache = bloque % 4;

            //Asigna un valor Para el directorio y la memoria
            int procesador;
            if (bloque < 16) //significa que son del directorio 0 y la memoria 0
            {
                procesador = 0;
            }
            else //memoria y directorio 1
            {
                procesador = 1;
            }

            //se trata de bloquear el caché
            var lockCache = new Lock(CacheDatos);

            //si se pudo bloquear sigue con la lectura
            if (lockCache.HasLock)
            {
                //tick de reloj
                //Controladora.barreraReloj.SignalAndWait();
                //si esta el bloque en mi cache y esta válido (hit)
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

                    //Asigna un valor Para el directorio y la memoria del bloque victima
                    int procesadorVictima;
                    if (bloqueVictima < 16) //significa que son del directorio 0 y la memoria 0
                    {
                        procesadorVictima = 0;
                    }
                    else //memoria y directorio 1
                    {
                        procesadorVictima = 1;
                    }

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
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            //si el bloqueVictima es de 0 a 15 significa que hay que revisar el directorio del procesador0
                            var lockDirVictima = new Lock(shared.directorios.ElementAt(procesadorVictima));
                            //se debe de bloquear la memoria para escribir en ella
                            var lockMemVictima = new Lock(shared.memoriasCompartida.ElementAt(procesadorVictima));

                            //se actualiza el valor en memoria del bloque victima
                            for (int i = 0; i < 4; i++)
                            {
                                shared.memoriasCompartida.ElementAt(procesadorVictima)[((bloqueVictima * 16) / 4) + i] = CacheDatos[i, posCache];
                            }

                            //se actualiza el directorio casa con 0´s y una U en el estado del bloque victima
                            shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, 0] = -1;
                            for (int i = 1; i < 4; i++)
                            {
                                shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, i] = 0;
                            }

                            //se libera el directorio casa del bloque victima
                            lockDirVictima.Dispose();
                            lockMemVictima.Dispose();

                            //se invalida el estado de la cache del bloque victima
                            CacheDatos[5, posCache] = -1;
                        }
                        //se libera el bus
                        lockBusDatos.Dispose();
                    }
                    else if (CacheDatos[5, posCache] == 0) //Compartido
                    {
                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();

                        //pone un 0 en el directorio casa del bloque victima
                        //se pregunta cual es el directorio casa

                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();
                        var lockDirVictima = new Lock(shared.directorios.ElementAt(procesadorVictima));
                        shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, IdNucleo] = 0;
                        CacheDatos[5, posCache] = -1;
                        //se debe de actualizar el valor del directorio por si todo esta en 0
                        if (shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, 1] == 0 &&
                           shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, 2] == 0 &&
                           shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, 3] == 0)
                        {
                            //Si entra aqui es porque el bloque esta uncached y se debe actualizar el estado con un -1
                            shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, 0] = -1;
                        }
                        lockDirVictima.Dispose();
                    }

                    //se bloquea el bus
                    var lockBusDatos1 = new Lock(Controladora.BusDatosP1);
                    //se bloquea el directorio casa del bloque fuente

                    //tick de reloj
                    //Controladora.barreraReloj.SignalAndWait();
                    var lockDirFuente = new Lock(shared.directorios.ElementAt(procesador));
                    //se debe de bloquear la memoria para escribir en ella
                    var lockMem = new Lock(shared.memoriasCompartida.ElementAt(procesador));
                    //se debe de mandar a escribir a memoria

                    //se debe de preguntar en el directorio si el bloque fuente está modificado en alguna otra caché
                    if (shared.directorios.ElementAt(procesador)[bloque, 0] == 1)
                    {
                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();
                        //se debe de preguntar en cual caché está modificado, además se debe de bloquear esa caché
                        if (shared.directorios.ElementAt(procesador)[bloque, 1] == 1) //Nucleo0
                        {
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(0));
                            
                            //se actualiza el valor en memoria del bloque
                            for (int i = 0; i < 4; i++)
                            {
                                shared.memoriasCompartida.ElementAt(procesador)[((bloque * 16) / 4) + i] = shared.cachesDatos.ElementAt(0)[i, posCache];
                            }
                            //Se actualiza el estado del bloque a compartido
                            shared.cachesDatos.ElementAt(0)[5, posCache] = 0;
                            //Se actualiza el estado del directorio como compartido
                            shared.directorios.ElementAt(procesador)[bloque, 0] = 0;
                            //se deja el 1 que ya estaba en el directorio

                            //se libera la memoria
                            lockMem.Dispose();
                            lockCacheFuente.Dispose();
                        }
                        else if (shared.directorios.ElementAt(procesador)[bloque, 2] == 1) //Nucleo 1
                        {
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(1));

                            //se actualiza el valor en memoria del bloque
                            for (int i = 0; i < 4; i++)
                            {
                                shared.memoriasCompartida.ElementAt(procesador)[((bloque * 16) / 4) + i] = shared.cachesDatos.ElementAt(1)[i, posCache];
                            }
                            //Se actualiza el estado del bloque a compartido
                            shared.cachesDatos.ElementAt(1)[5, posCache] = 0;
                            //Se actualiza el estado del directorio como compartido
                            shared.directorios.ElementAt(procesador)[bloque, 0] = 0;
                            //se deja el 1 que ya estaba en el directorio

                            //se libera la memoria
                            lockMem.Dispose();
                            lockCacheFuente.Dispose();
                        }
                        else //Nucleo2
                        {
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(2));

                            //se actualiza el valor en memoria del bloque
                            for (int i = 0; i < 4; i++)
                            {
                                shared.memoriasCompartida.ElementAt(procesador)[((bloque * 16) / 4) + i] = shared.cachesDatos.ElementAt(2)[i, posCache];
                            }
                            //Se actualiza el estado del bloque a compartido
                            shared.cachesDatos.ElementAt(2)[5, posCache] = 0;
                            //Se actualiza el estado del directorio como compartido
                            shared.directorios.ElementAt(procesador)[bloque, 0] = 0;
                            //se deja el 1 que ya estaba en el directorio

                            //se libera la memoria
                            lockMem.Dispose();
                            lockCacheFuente.Dispose();

                        }
                    }
                    //si no es porque está compartido o invalido y se le puede caer sin problema

                    //Ahora que no hay conflictos con los estados se puede finalmente escribir en la cache para la lectura

                    //tick de reloj
                    //Controladora.barreraReloj.SignalAndWait();
                    //subo el bloque a cache
                    for (int i = 0; i < 4; i++)
                    {
                        CacheDatos[i, posCache] = shared.memoriasCompartida.ElementAt(procesador)[((bloque * 16) / 4) + i];
                        Console.Write("CacheDatos: {0}\n", CacheDatos[i, posCache]);
                    }
                    //actualizo el valor del bloque y el del estado
                    CacheDatos[4, posCache] = bloque;
                    CacheDatos[5, posCache] = 0; //0 por estar compartido

                    //actualizo el directorio casa
                    //pongo un 0 en estado, que significa que esta compartido
                    shared.directorios.ElementAt(procesador)[bloque, 0] = 0;
                    shared.directorios.ElementAt(procesador)[bloque, IdNucleo] = 1;

                    lockMem.Dispose();
                    lockDirFuente.Dispose();

                    //finalmente puedo leer
                    //agrego al registro lo que hay en caché
                    Registros[instruccion.RF2_RD] = CacheDatos[palabra, posCache];
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

        //Hay que definir lo de los estados, en este caso estoy proponiendo que: -1 = I, 0 = C, 1 = M para la cache
        //para el directorio sería -1 = U, 0 = C, 1 = M
        public bool EjecutarSW(Instruccion instruccion, int[,] CacheDatos)
        {
            bool result = false;

            //se obtiene direccion de memoria
            int direccion = instruccion.RF1 + instruccion.RD_IMM;

            //se convierte a bloque y palabra posicion en cache
            int bloque = direccion / 16;
            int palabra = (direccion % 16) / 4;
            int posCache = bloque % 4;

            //Asigna un valor Para el directorio y la memoria
            int procesador;
            if (bloque < 16) //significa que son del directorio 0 y la memoria 0
            {
                procesador = 0;
            }
            else //memoria y directorio 1
            {
                procesador = 1;
            }

            //se trata de bloquear el caché
            var lockCache = new Lock(CacheDatos);

            //si se pudo bloquear sigue con la lectura
            if (lockCache.HasLock)
            {
                //tick de reloj
                //Controladora.barreraReloj.SignalAndWait();
                //si esta el bloque en mi cache y esta válido (hit)
                if (CacheDatos[4, posCache] == bloque && CacheDatos[5, posCache] != -1)
                {
                    //Se debe de preguntar si el estado del bloque es modificado
                    if (CacheDatos[5, posCache] == 1)
                    {
                        //se modifica el dato en la cache actual
                        CacheDatos[palabra, posCache] = Registros[instruccion.RF2_RD];
                        result = true;
                        //no se actualiza directorio ni estado de la cache pues ya estaba modificado
                        //se desbloquea todo
                        lockCache.Dispose();
                    }
                    else //el bloque es compartido
                    {
                        //se debe de bloquear el directorio casa del bloque
                        var lockDir = new Lock(shared.directorios.ElementAt(0));
                        //se debe de preguntar si se encuentra compartido en otra cache (como es compartido puede estar en las 3, pero minimo en una, la actual)
                        if (shared.directorios.ElementAt(procesador)[bloque, 1] == 1)//Nucleo0
                        {
                            //se bloquea la cache donde esta el bloque
                            var lockCache2 = new Lock(shared.cachesDatos.ElementAt(0));
                            //invalida el bloque de la cache y el directorio
                            shared.cachesDatos.ElementAt(0)[5, posCache] = -1;
                            shared.directorios.ElementAt(procesador)[bloque, 1] = 0;
                            lockCache2.Dispose();
                        }
                        if (shared.directorios.ElementAt(procesador)[bloque, 2] == 1)//Nucleo1
                        {
                            //se bloquea la cache donde esta el bloque
                            var lockCache2 = new Lock(shared.cachesDatos.ElementAt(1));
                            //invalida el bloque de la cache y el directorio
                            shared.cachesDatos.ElementAt(1)[5, posCache] = -1;
                            shared.directorios.ElementAt(procesador)[bloque, 1] = 0;
                            lockCache2.Dispose();
                        }
                        if (shared.directorios.ElementAt(procesador)[bloque, 3] == 1)//Nucleo2
                        {
                            //se bloquea la cache donde esta el bloque
                            var lockCache2 = new Lock(shared.cachesDatos.ElementAt(2));
                            //invalida el bloque de la cache y el directorio
                            shared.cachesDatos.ElementAt(2)[5, posCache] = -1;
                            shared.directorios.ElementAt(procesador)[bloque, 1] = 0;
                            lockCache2.Dispose();
                        }
                        //Cuando llega aquí ya invalidó todos los posibles bloques compartidos y ya puede se modificar
                        //se modifica el dato en la cache actual
                        CacheDatos[palabra, posCache] = Registros[instruccion.RF2_RD];
                        //actualiza su estado en cache
                        CacheDatos[5, posCache] = 1;
                        result = true;
                        //se actualiza el directorio
                        shared.directorios.ElementAt(procesador)[bloque, 0] = 1;
                        shared.directorios.ElementAt(procesador)[bloque, IdNucleo] = 1;
                        //se desbloquea todo
                        lockCache.Dispose();
                        lockDir.Dispose();
                    }
                }
                else //fallo de cache (no se encuentra el bloque en cache o no esta valido) FALTA! REVISAR!
                {
                    //tick de reloj
                    //Controladora.barreraReloj.SignalAndWait();

                    //se obtienen los datos del bloque victima
                    int bloqueVictima = CacheDatos[4, posCache];

                    //Asigna un valor Para el directorio y la memoria del bloque victima
                    int procesadorVictima;
                    if (bloqueVictima < 16) //significa que son del directorio 0 y la memoria 0
                    {
                        procesadorVictima = 0;
                    }
                    else //memoria y directorio 1
                    {
                        procesadorVictima = 1;
                    }

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
                            //bloquear el directorio casa del bloque victima
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            //si el bloqueVictima es de 0 a 15 significa que hay que revisar el directorio del procesador0
                            var lockDirVictima = new Lock(shared.directorios.ElementAt(procesadorVictima));
                            //se debe de bloquear la memoria para escribir en ella
                            var lockMemVictima = new Lock(shared.memoriasCompartida.ElementAt(procesadorVictima));

                            //se actualiza el valor en memoria del bloque victima
                            for (int i = 0; i < 4; i++)
                            {
                                shared.memoriasCompartida.ElementAt(procesadorVictima)[((bloqueVictima * 16) / 4) + i] = CacheDatos[i, posCache];
                            }

                            //se actualiza el directorio casa con 0´s y una U en el estado del bloque victima
                            shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, 0] = -1;
                            for (int i = 1; i < 4; i++)
                            {
                                shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, i] = 0;
                            }

                            //se libera el directorio casa del bloque victima
                            lockDirVictima.Dispose();
                            lockMemVictima.Dispose();
                            //se invalida el estado de la cache del bloque victima
                            CacheDatos[5, posCache] = -1;
                        }
                        //se libera el bus
                        lockBusDatos.Dispose();
                    }
                    else if (CacheDatos[5, posCache] == 0) //Compartido
                    {
                        //pone un 0 en el directorio casa del bloque victima
                        //se pregunta cual es el directorio casa
                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();
                        var lockDirVictima = new Lock(shared.directorios.ElementAt(procesadorVictima));
                        shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, IdNucleo] = 0;
                        CacheDatos[5, posCache] = -1;
                        //se debe de actualizar el valor del directorio por si todo esta en 0
                        if (shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, 1] == 0 &&
                           shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, 2] == 0 &&
                           shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, 3] == 0)
                        {
                            //Si entra aqui es porque el bloque esta uncached y se debe actualizar el estado con un -1
                            shared.directorios.ElementAt(procesadorVictima)[bloqueVictima, 0] = -1;
                        }
                        lockDirVictima.Dispose();
                    }
                    //Ya termino de analizar el bloque victima, si es invalido se le puede caer encima sin problema

                    //se bloquea el bus
                    var lockBusDatos1 = new Lock(Controladora.BusDatosP1);

                    //se bloquea el directorio casa del bloque fuente
                    //tick de reloj
                    //Controladora.barreraReloj.SignalAndWait();
                    var lockDirFuente = new Lock(shared.directorios.ElementAt(procesador));
                    //se debe de mandar a escribir a memoria
                    //se debe de bloquear la memoria para escribir en ella
                    var lockMem = new Lock(shared.memoriasCompartida.ElementAt(procesador));

                    //se debe de preguntar en el directorio si el bloque fuente está modificado en alguna otra caché
                    if (shared.directorios.ElementAt(procesador)[bloque, 0] == 1)
                    {
                        //tick de reloj
                        //Controladora.barreraReloj.SignalAndWait();
                        //se debe de preguntar en cual caché está modificado, además se debe de bloquear esa caché
                        if (shared.directorios.ElementAt(procesador)[bloque, 1] == 1) //Nucleo0
                        {
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(0));

                            //se actualiza el valor en memoria del bloque
                            for (int i = 0; i < 4; i++)
                            {
                                shared.memoriasCompartida.ElementAt(procesador)[((bloque * 16) / 4) + i] = shared.cachesDatos.ElementAt(0)[i, posCache];
                            }
                            //Se invalida el bloque
                            shared.cachesDatos.ElementAt(0)[5, posCache] = -1;
                            //Se pone 0 en el nucleo invalidado
                            shared.directorios.ElementAt(procesador)[bloque, 1] = 0;
                            //Pongo como uncached
                            shared.directorios.ElementAt(procesador)[bloque, 0] = -1;

                            //se libera la memoria
                            lockMem.Dispose();
                            lockCacheFuente.Dispose();
                        }
                        else if (shared.directorios.ElementAt(procesador)[bloque, 2] == 1) //Nucleo 1
                        {
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(1));

                            //se actualiza el valor en memoria del bloque
                            for (int i = 0; i < 4; i++)
                            {
                                shared.memoriasCompartida.ElementAt(procesador)[((bloque * 16) / 4) + i] = shared.cachesDatos.ElementAt(1)[i, posCache];
                            }
                            //Se invalida el bloque
                            shared.cachesDatos.ElementAt(1)[5, posCache] = -1;
                            //Se pone 0 en el nucleo invalidado
                            shared.directorios.ElementAt(procesador)[bloque, 2] = 0;
                            //Pongo como uncached
                            shared.directorios.ElementAt(procesador)[bloque, 0] = -1;

                            //se libera la memoria
                            lockMem.Dispose();
                            lockCacheFuente.Dispose();
                        }
                        else //Nucleo2
                        {
                            //tick de reloj
                            //Controladora.barreraReloj.SignalAndWait();
                            var lockCacheFuente = new Lock(shared.cachesDatos.ElementAt(2));

                            //se actualiza el valor en memoria del bloque
                            for (int i = 0; i < 4; i++)
                            {
                                shared.memoriasCompartida.ElementAt(procesador)[((bloque * 16) / 4) + i] = shared.cachesDatos.ElementAt(2)[i, posCache];
                            }
                            //Se invalida el bloque
                            shared.cachesDatos.ElementAt(2)[5, posCache] = -1;
                            //Se pone 0 en el nucleo invalidado
                            shared.directorios.ElementAt(procesador)[bloque, 3] = 0;
                            //Pongo como uncached
                            shared.directorios.ElementAt(procesador)[bloque, 0] = -1;

                            //se libera la memoria
                            lockMem.Dispose();
                            lockCacheFuente.Dispose();

                        }
                    }

                    //si no es porque está compartido o invalido y se le puede caer sin problema

                    //Ahora que no hay conflictos con los estados se puede finalmente escribir en la cache para la lectura
                    //tick de reloj
                    //Controladora.barreraReloj.SignalAndWait();

                    //subo el bloque a cache
                    for (int i = 0; i < 4; i++)
                    {
                        CacheDatos[i, posCache] = shared.memoriasCompartida.ElementAt(procesador)[((bloque * 16) / 4) + i];
                        Console.Write("CacheDatos: {0}\n", CacheDatos[i, posCache]);
                    }
                    //actualizo el valor del bloque y el del estado
                    CacheDatos[4, posCache] = bloque;
                    //finalmente puedo escribir
                    //agrego al caché lo que hay en registro
                    CacheDatos[palabra, posCache] = Registros[instruccion.RF2_RD];
                    //restar quantum
                    //quantum.Valor--;
                    //ExitoAnterior = true
                    result = true;
                    //4(1 + 5 +1) cargando bloque de memoria a cache.
                    //IncrementarReloj(28);
                    //libero el bus
                    lockBusDatos1.Dispose();
                    CacheDatos[5, posCache] = 1; //1 por estar modificado

                    //actualizo el directorio casa
                    //pongo un 1 en estado, que significa que esta modificado
                    shared.directorios.ElementAt(procesador)[bloque, 0] = 1;
                    shared.directorios.ElementAt(procesador)[bloque, IdNucleo] = 1;

                    lockMem.Dispose();
                    lockDirFuente.Dispose();

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
            //Probar bloqueo de hilos finalizados

            int hfin;

            lock (Program.BusFinalizados)
            {
                hfin = shared.hilosFinalizados.ElementAt(IdProce).Count();
            }


            while (shared.hilosTotales[IdProce] != hfin)
            {

                proximo = null;

                //bloquea la cola de contextos del Procesador donde esta el nucleo
                lock (Program.BusContextos)
                {
                    //carga el hilo de la cola de hilos pendientes
                    if (shared.colasContextos.ElementAt(IdProce).Count() > 0)
                        proximo = shared.colasContextos.ElementAt(IdProce).Dequeue();
                }

                if (proximo != null)
                {
                    /*
                    do
                    {
                        //bloquea la cola de contextos del Procesador donde esta el nucleo
                        lock (Program.BusContextos)
                        {
                            //carga el hilo de la cola de hilos pendientes
                            proximo = shared.colasContextos.ElementAt(IdProce).Dequeue();
                        }
                        if (proximo.Finalizado)
                        {
                            //lock (shared.hilosFinalizados.ElementAt(IdProce))
                            {
                                //si el hilo esta finalizado, lo guarda en la lista de hilos finalizados
                                shared.hilosFinalizados.ElementAt(IdProce).Add(proximo);
                            }
                        }

                    } while (proximo.Finalizado);
                    */
                    //REVISAR EL CASO EN QUE TODOS LOS HILOS ESTEN FINALIZADOS Y QUEDE VACIA LA COLA PORQUE SE PUEDE ENCLICLAR

                    //inicializa registros y pc del contexto
                    int numInst;

                    //lock (shared)
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

                        if (cacheI.etiquetas[bloque % 4] == bloque)
                        {
                            hit = true;
                        }

                        if (!hit)
                        {
                            //no hubo hit y va a memoria a cargar bloque

                            for (int j = 0; j < 4; ++j)
                            {

                                Instruccion nueva = new Instruccion();

                                lock (Program.BusInstrucciones[IdProce])
                                {
                                    nueva.CodigoOp = shared.memoriaInstrucciones.ElementAt(IdProce)[(bloque * 16) + (j * 4)];
                                    nueva.RF1 = shared.memoriaInstrucciones.ElementAt(IdProce)[(bloque * 16) + (j * 4) + 1];
                                    nueva.RF2_RD = shared.memoriaInstrucciones.ElementAt(IdProce)[(bloque * 16) + (j * 4) + 2];
                                    nueva.RD_IMM = shared.memoriaInstrucciones.ElementAt(IdProce)[(bloque * 16) + (j * 4) + 3];
                                }

                                cacheI.bloqueInstruccion[bloque % 4, j] = nueva;


                            }

                            //Guarda la etiqueta del bloque subido
                            cacheI.etiquetas[bloque % 4] = bloque;

                        }

                        //Carga instruccion al IR de la cache

                        IR = cacheI.bloqueInstruccion[bloque % 4, palabra];

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
                        lock (Program.BusFinalizados)
                        {
                            shared.hilosFinalizados.ElementAt(IdProce).Add(proximo);
                        }
                    }
                    else
                    {
                        lock (Program.BusContextos)
                        {
                            proximo.IR = IR;
                            proximo.PC = PC;
                            shared.colasContextos.ElementAt(IdProce).Enqueue(proximo);
                        }
                    }

                }

                lock (Program.BusFinalizados)
                {
                    hfin = shared.hilosFinalizados.ElementAt(IdProce).Count();
                }

            }
        }
    }
}
