﻿using System;
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

            for (int i = 0; i <= controladora.shared.memoriaInstrucciones.ElementAt(0).Count() - 1; i++)
            {
                txtMemInstrucciones1.AppendText(controladora.shared.memoriaInstrucciones.ElementAt(0)[i] + "-");

            }

            for (int i = 0; i <= controladora.shared.memoriaInstrucciones.ElementAt(1).Count() - 1; i++)
            {
                txtMemInstrucciones2.AppendText(controladora.shared.memoriaInstrucciones.ElementAt(1)[i] + "-");

            }
           
            //AQUII COMIENZA HACIENDO LOS PROCES

            controladora.iniciar();
            controladora.iniciarLoad();
            controladora.iniciarStore();

            int cont = 1;
            foreach (List<ContextoHilillo> contexto in controladora.shared.hilosFinalizados)
            {
                txtHilillosFinalizados.AppendText("Lista" + cont);
                for (int i = 0; i <= contexto.Count() - 1; i++)
                {
                    for (int j = 0; j <= contexto.ElementAt(i).Registros.Count() - 1; j++)
                    {
                        if (j % 8 == 0)
                        {
                            txtHilillosFinalizados.AppendText("\n");
                        }
                        txtHilillosFinalizados.AppendText(contexto.ElementAt(i).Registros[j] + "-");
                    }
                }
                txtHilillosFinalizados.AppendText("\n");


                cont++;
            }
            //controladora.shared.hilosFinalizados;


        }


        List<List<int>> listaHilillos = new List<List<int>>();


        private void btnCargarArchivos_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();

            try
            {
                System.IO.Stream fileStream = openFileDialog1.OpenFile();
                int cont = 1;
                foreach (String file in openFileDialog1.FileNames)
                {
                    //para enviarlo al controlador de hilillos.
                    List<int> lista = new List<int>();
                    string lineasTodoElArchivo = String.Empty;

                    //para mostrar en pantalla.
                    txtArchivo.AppendText("Archivo " + cont + "\n");
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

                int cont2 = 1;
                foreach (List<int> listaDatosArchivos in listaHilillos)
                {
                    txtVerLista.AppendText("Archivo " + cont2 + "\n");
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

            txtHilillosFinalizados.Text = String.Empty;

            openFileDialog1.Reset();
            listaHilillos.Clear();
        }
    }
}