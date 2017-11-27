using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ProyectoArqui.Logica;

namespace ProyectoArqui
{
    public partial class Form1 : Form
    {

        //Controladora controladora;

        public Form1()
        {
            InitializeComponent();
            //controladora = new Controladora();

        }

        //este es un push de prueba hecho por jose valverde

        private void Form1_Load(object sender, EventArgs e)
        {
            txtQuantum.Text = "10";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Controladora controladora = new Controladora();

            if (txtQuantum.Text != "" || txtArchivo.Text != "")
            {
                try
                {
                    controladora.setQuantum(Convert.ToInt32(txtQuantum.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("El valor ingresado no fue un numero entero.");
                    txtQuantum.Text = String.Empty;
                }
            }
            else
            {    
                MessageBox.Show("Debe ingresar un valor para el Quantum y cargar los archivos.");
            }

            /*
             
             Llamar controladora.cargar(lista1,lista2) y pasarle las 2 listas de listas de ints 
             Cada lista externa representa un archivo (o hilillo) y cada lista de ints es nada mas sacar todos los numeros
             y meterlos.
             8 0 6 11
             8 0 4 5
            Deberia quedar en una lista asi: 8>0>6>11>8>0>4>5
             
             
             */


            controladora.cargar(listaHilillos);

            
            controladora.iniciar();

            for (int i = 0; i <= controladora.shared.memoriasCompartida.ElementAt(0).Count() - 1; i++)
            {
                if ((i + 1) % 16 == 0)
                {
                    txtMemInstrucciones1.AppendText(controladora.shared.memoriasCompartida.ElementAt(0)[i] + "\n");

                }
                //if ((i + 1) % 64 == 0) {
                //    txtMemInstrucciones1.AppendText(controladora.shared.memoriaInstrucciones.ElementAt(0)[i] + "\n");
                //}
                else {

                    txtMemInstrucciones1.AppendText(controladora.shared.memoriasCompartida.ElementAt(0)[i] + " | ");
                }
                

            }

            for (int i = 0; i <= controladora.shared.memoriasCompartida.ElementAt(1).Count() - 1; i++)
            {
                if ((i + 1) % 16 == 0)
                {
                    txtMemInstrucciones2.AppendText(controladora.shared.memoriasCompartida.ElementAt(1)[i] + "\n");

                }
                //if ((i + 1) % 64 == 0) {
                //    txtMemInstrucciones1.AppendText(controladora.shared.memoriaInstrucciones.ElementAt(0)[i] + "\n");
                //}
                else
                {

                    txtMemInstrucciones2.AppendText(controladora.shared.memoriasCompartida.ElementAt(1)[i] + " | ");
                }

            }
           
            //AQUII COMIENZA HACIENDO LOS PROCES


            controladora.shared.listaNombreArchivos = nombreArchivos;
            //controladora.iniciarLoad();
            //controladora.iniciarStore();

            //Cargar procesador 0


            //cargar directorios
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j != 3)
                    {
                        txtdir0.AppendText(controladora.shared.directorios.ElementAt(0)[i, j].ToString() + "/");
                    }
                    else{
                        txtdir0.AppendText(controladora.shared.directorios.ElementAt(0)[i, j].ToString());

                    }
                }
                txtdir0.AppendText("\n");
            }

            //cargar cache 0
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j != 3)
                    {
                        txtCache00.AppendText(controladora.shared.cachesDatos.ElementAt(0)[i, j].ToString() + "/");
                    }
                    else
                    {

                        txtCache00.AppendText(controladora.shared.cachesDatos.ElementAt(0)[i, j].ToString());
                    }
                }
                txtCache00.AppendText("\n");
            }

            //carga cache 1
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j != 3)
                    {
                        txtCache01.AppendText(controladora.shared.cachesDatos.ElementAt(1)[i, j].ToString() + "/");
                    }
                    else {

                        txtCache01.AppendText(controladora.shared.cachesDatos.ElementAt(1)[i, j].ToString());
                    }
                }
                txtCache01.AppendText("\n");
            }

         


            // Cargar procesador 1

            //Cargar directorios
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j != 3)
                    {
                        txtdir1.AppendText(controladora.shared.directorios.ElementAt(1)[i, j].ToString() + "/");
                    }
                    else
                    {
                        txtdir1.AppendText(controladora.shared.directorios.ElementAt(1)[i, j].ToString());

                    }
                }
                txtdir1.AppendText("\n");
            }

            //carga cache 0 proce 1
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j != 3)
                    {
                        txtCache10.AppendText(controladora.shared.cachesDatos.ElementAt(2)[i, j].ToString() + "/");
                    }
                    else
                    {
                        txtCache10.AppendText(controladora.shared.cachesDatos.ElementAt(2)[i, j].ToString());
                    }
                }
                txtCache10.AppendText("\n");
            }




            int cont = 0;
            int contArchivo = 0;
            foreach (List<ContextoHilillo> contexto in controladora.shared.hilosFinalizados)
            {
                //txtHilillosFinalizados.AppendText("Procesador " + cont +"\n");
                for (int i = 0; i <= contexto.Count() - 1; i++)
                {
                    txtHilillosFinalizados.AppendText("Id Procesador: " + contexto.ElementAt(i).IdProcesador +"\n");
                    txtHilillosFinalizados.AppendText("Archivo " + contArchivo +"\n");
                    for (int j = 0; j <= contexto.ElementAt(i).Registros.Count() - 1; j++)
                    {
                        if (j % 8 == 0)
                        {
                           txtHilillosFinalizados.AppendText("\n");
                        }
                        txtHilillosFinalizados.AppendText(contexto.ElementAt(i).Registros[j] + "-");
                        //txtHilillosFinalizados.AppendText(contexto.ElementAt(i).horaFin.ToShortTimeString());
                        
                    }
                    contArchivo++;
                    txtHilillosFinalizados.AppendText("\n");
                    //txtHilillosFinalizados.AppendText("Inicio: "+contexto.ElementAt(i).horaInicio.ToLongTimeString()+"/ Fin:"+contexto.ElementAt(i).horaFin.ToLongTimeString() +"\n");
                    txtHilillosFinalizados.AppendText("PC"+contexto.ElementAt(i).PC);
                    txtHilillosFinalizados.AppendText("\n");
                }
                //txtHilillosFinalizados.AppendText("\n");
                contArchivo = 0;
                cont++;
                
            }


        }


        List<List<int>> listaHilillos = new List<List<int>>();
        List<string> nombreArchivos = new List<string>();

        private void btnCargarArchivos_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();

            try
            {
                System.IO.Stream fileStream = openFileDialog1.OpenFile();
                int cont = 0;

                
                foreach (string filename in openFileDialog1.FileNames)
                {
                    nombreArchivos.Add(filename);
                }

                foreach (String file in openFileDialog1.FileNames)
                {
                    
                    //para enviarlo al controlador de hilillos.
                    List<int> lista = new List<int>();
                    string lineasTodoElArchivo = String.Empty;

                    //para mostrar en pantalla.
                    txtArchivo.AppendText("Archivo " + nombreArchivos.ElementAt(cont)+ "\n");
                    string[] lines = System.IO.File.ReadAllLines(file);
                    string[] parts;
                    foreach (string line in lines)
                    {
                        char[] delimiters = new char[] { ' ' };
                        parts = line.Split(delimiters,
                                         StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i <= parts.Count() - 1; i++)
                        {
                            //txtArchivo.AppendText(parts[i]+"\n");
                            lista.Add(Convert.ToInt32(parts[i]));
                        }


                        txtArchivo.AppendText(line + "\n");
                        //lineasTodoElArchivo = lineasTodoElArchivo + "" + line;
                    }
                    listaHilillos.Add(lista);
                    cont++;

                }
                fileStream.Close();

                int cont2 = 0;
                foreach (List<int> listaDatosArchivos in listaHilillos)
                {
                    txtVerLista.AppendText("Archivo " + nombreArchivos.ElementAt(cont2) + "\n");
                    for (int i = 0; i <= listaDatosArchivos.Count() - 1; i++)
                    {
                        txtVerLista.AppendText(listaDatosArchivos[i] + " ");


                    }
                    txtVerLista.AppendText("\n");
                    cont2++;
                }



            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                txtArchivo.AppendText(Ex.ToString());

            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtQuantum.Text = String.Empty;

            txtVerLista.Text = String.Empty;
            txtArchivo.Text = String.Empty;

            txtMemInstrucciones1.Text = String.Empty;
            txtMemInstrucciones2.Text = String.Empty;

            txtdir0.Text = String.Empty;
            txtdir1.Text = String.Empty;

            txtCache00.Text = String.Empty;
            txtCache01.Text = String.Empty;
            txtCache10.Text = String.Empty;

            txtHilillosFinalizados.Text = String.Empty;

            openFileDialog1.Reset();
            listaHilillos.Clear();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnInicioLento_Click(object sender, EventArgs e)
        {

        }
    }
}