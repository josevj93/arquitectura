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
                    Registros[instruccion.RF2_RD] = Registros[instruccion.RF1] + Registros[instruccion.RD_IMM];
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
                    if(Registros[instruccion.RF1] == 0)
                    {
                        PC = PC + (instruccion.RD_IMM * 4); //cambio el PC a la etiqueta
                    }
                    break;
                case 5:
                    //BNEZ: Si Rx != 0 hace un salto de n*4 (tamaño de instruccion)
                    if(Registros[instruccion.RF1] != 0)
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

        private bool EjecutarLW(Instruccion instruccion, Referencias refMemoria, Quantum quantum)
        {
            bool result = false;

            var lockMiCache = new Lock(CacheDatos); //
            if (lockMiCache.HasLock)
            {
                //tick de reloj
                Controladora.barreraReloj.SignalAndWait();
                //si esta el bloque en mi cache y esta válido
                if (CacheDatos[4, refMemoria.PosCache] == refMemoria.Bloque && CacheDatos[4, refMemoria.PosCache] != -1)
                {
                    //cargar el valor al registro correspondiente
                    Registros[instruccion.RF2_RD] = CacheDatos[refMemoria.Palabra, refMemoria.PosCache];

                    //restar quantum
                    quantum.Valor--;
                    //ExitoAnterior = true
                    result = true;
                    Controladora.barreraReloj.SignalAndWait();
                }
                else //fallo de cache
                {
                    //bloquear el bus de data
                    var lockBusData = new Lock(Controladora.BusDatosP1);
                    //si obtuve el bus de data
                    if (lockBusData.HasLock)
                    {
                        //tick de reloj
                        Controladora.barreraReloj.SignalAndWait();
                        //subo el bloque a cache
                        for (int i = 0; i < 4; i++)
                        {
                            //CacheDatos[/*refMemoria.Palabra +*/ i, refMemoria.PosCache] =
                                //Controladora.MCP1[((refMemoria.Bloque * 16) + i) / 4];
                        }
                        //agrego al registro lo que hay en caché
                        Registros[instruccion.RF2_RD] = CacheDatos[refMemoria.Palabra, refMemoria.PosCache];
                        //restar quantum
                        quantum.Valor--;
                        //ExitoAnterior = true
                        result = true;
                        //4(1 + 5 +1) cargando bloque de memoria a cache.
                        IncrementarReloj(28);
                        //libero el bus
                        lockBusData.Dispose();
                    }
                    else
                    {
                        //tick de reloj si no tuve el bus
                        Controladora.barreraReloj.SignalAndWait();
                    }
                }
                lockMiCache.Dispose();
            }
            else
            {
                //tick de reloj si no obtuve el cacheData
                Controladora.barreraReloj.SignalAndWait();
            }
            return result;
        }

        private void IncrementarReloj(int limiteSuperior)
        {
            for (int i = 0; i < limiteSuperior; i++)
                Controladora.barreraReloj.SignalAndWait();
        }

        public void fetch() {
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

            do {
                //bloquea la cola de contextos del Procesador donde esta el nucleo
                lock (shared.colasContextos.ElementAt(IdProce))
                {
                    //carga el hilo de la cola de hilos pendientes
                    proximo = shared.colasContextos.ElementAt(IdProce).Dequeue();
                }
                if (proximo.Finalizado)
                {
                    lock (shared.hilosFinalizados)
                    {
                        //si el hilo esta finalizado, lo guarda en la lista de hilos finalizados
                        shared.hilosFinalizados.ElementAt(IdProce).Add(proximo);
                    }
                }

            } while (proximo.Finalizado);

            //REVISAR EL CASO EN QUE TODOS LOS HILOS ESTEN FINALIZADOS Y QUEDE VACIA LA COLA PORQUE SE PUEDE ENCLICLAR

            //si no es un hilo finalizado, inicializa registros y pc del contexto
            int numInst = Controladora.Quant; // GUARDAR QUANTUM EN SHARED
            PC = proximo.PC;
            Registros = proximo.Registros;
            
            while(numInst > 0 && !proximo.Finalizado)
            {

                //saca palabra y bloque del PC

                int bloque = PC / 16;
                int palabra = (PC %16)/4;

                bool hit = false;

                //comienza fetch

                int i = 0;
                while (i<4 && cacheI.etiquetas[i] != bloque)
                {
                    if(cacheI.etiquetas[i] == bloque)
                    {
                        hit = true;
                    }

                }

                if (!hit)
                {

                    //no hubo hit y va a memoria a cargar bloque

                    for (int j = 0; j < 4; ++j)
                    {

                        Instruccion nueva = new Instruccion();

                        if (contexto.IdProcesador == 0)
                        {
                            nueva.CodigoOp = Controladora.MIP1[bloque + (j * 4)];
                            nueva.RF1 = Controladora.MIP1[bloque + (j * 4) + 1];
                            nueva.RF2_RD = Controladora.MIP1[bloque + (j * 4)  + 2];
                            nueva.RD_IMM = Controladora.MIP1[bloque + (j * 4) + 3];

                        }
                        else
                        {
                            nueva.CodigoOp = Controladora.MIP2[bloque + (j * 4)];
                            nueva.RF1 = Controladora.MIP2[bloque + (j * 4) + 1];
                            nueva.RF2_RD = Controladora.MIP2[bloque + (j * 4) + 2];
                            nueva.RD_IMM = Controladora.MIP2[bloque + (j * 4) + 3];


                        }
                        
                        cacheI.bloqueInstruccion[bloque%4, j] = nueva;


                    }

                    //Guarda la etiqueta del bloque subido
                    cacheI.etiquetas[bloque % 4] = bloque;
                    
                }

                //Carga instruccion al IR de la cache

                IR = cacheI.bloqueInstruccion[bloque, palabra];

                //ejecuta instruccion

                if (ejecutarInstruccion(IR)){
                    contexto.Finalizado = true;

                }


            }

            return contexto;

        }
    }
}
