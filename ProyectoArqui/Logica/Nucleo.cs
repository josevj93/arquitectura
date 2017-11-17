using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Logica
{
    class Nucleo
    {
        public Utilidades.CodigosInst IR { get; set; }
        public int PC { get; set; }
        public int[] Registros { get; set; }
        public int IdNucleo = -1;

        /// <summary>
        /// 5 ya que el ultimo es la etiqueta.
        /// </summary>
        //public Instruccion[,] CacheInstrucciones = new Instruccion[5, 4];

        public CacheInstrucciones cacheI = new CacheInstrucciones();

        
        /// <summary>
        /// 6 ya que el 5 es la etiqueta y el 6 es el estado.
        /// </summary>
        public int[,] CacheDatos = new int[6, 4];

        public Instruccion instruccionActual; //la primera vez será null

        public Nucleo(int id)
        {
            IdNucleo = id;
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
                case Utilidades.CodigosInst.DADDI:
                    //Rx <-- (Ry) + n
                    Registros[instruccion.RF2_RD] = Registros[instruccion.RF1] + Registros[instruccion.RD_IMM];
                    result = true;
                    break;
                case Utilidades.CodigosInst.DADD:
                    //Rx <-- (Ry) + (Rz)
                    Registros[instruccion.RD_IMM] = Registros[instruccion.RF1] + Registros[instruccion.RF2_RD];
                    result = true;
                    break;
                case Utilidades.CodigosInst.DSUB:
                    //Rx <-- (Ry) - (Rz)
                    Registros[instruccion.RD_IMM] = Registros[instruccion.RF1] - Registros[instruccion.RF2_RD];
                    result = true;
                    break;
                case Utilidades.CodigosInst.DMUL:
                    //Rx <-- (Ry) * (Rz)
                    Registros[instruccion.RD_IMM] = Registros[instruccion.RF1] * Registros[instruccion.RF2_RD];
                    result = true;
                    break;
                case Utilidades.CodigosInst.DDIV:
                    //Rx <-- (Ry) / (Rz)
                    Registros[instruccion.RD_IMM] = Registros[instruccion.RF1] / Registros[instruccion.RF2_RD];
                    result = true;
                    break;
                case Utilidades.CodigosInst.BEQZ:
                    //Si Rx == 0 hace un salto de n*4 (tamaño de instruccion)
                    if(Registros[instruccion.RF1] == 0)
                    {
                        PC = PC + (instruccion.RD_IMM * 4); //cambio el PC a la etiqueta
                        result = true;
                    }
                    break;
                case Utilidades.CodigosInst.BNEZ:
                    //Si Rx != 0 hace un salto de n*4 (tamaño de instruccion)
                    if(Registros[instruccion.RF1] != 0)
                    {
                        PC = PC + (instruccion.RD_IMM * 4); //cambio el PC a la etiqueta
                    }
                    break;
                case Utilidades.CodigosInst.JAL:
                    //R31 <-- PC, PC <-- PC + n
                    Registros[31] = PC;
                    PC = PC + instruccion.RD_IMM;
                    result = true;
                    break;
                case Utilidades.CodigosInst.JR:
                    //PC <-- (RX)
                    PC = Registros[instruccion.RF1];
                    result = true;
                    break;
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
        public ContextoHilillo iniciar(ContextoHilillo contexto, int[] MI, int quantum)
        {
            //Inicializa registros y pc del contexto
            PC = contexto.PC;
            Registros = contexto.Registros;
            bool finalizado = contexto.Finalizado;

            while(quantum > 0 /*&& !finalizado*/)
            {

                //saca palabra y bloque del PC

                int bloque = PC / 16;
                int palabra = PC / 4; // revisar

                bool hit = false;

                //comienza fetch
                foreach (int i in cacheI.etiquetas)
                {
                    if(i == bloque)
                    {
                        hit = true;

                    }

                }

                if (hit)
                {
                    //hit en cache 
                    //cacheI.bloqueInstruccion;
                }
                else
                {

                    //no hubo hit y va a memoria a cargar bloque

                    
                    
                }

            }




            return contexto;

        }
    }
}
