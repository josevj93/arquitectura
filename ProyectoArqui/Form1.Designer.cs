﻿namespace ProyectoArqui
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuantum = new System.Windows.Forms.TextBox();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnCargarArchivos = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtArchivo = new System.Windows.Forms.TextBox();
            this.txtVerLista = new System.Windows.Forms.TextBox();
            this.txtMemInstrucciones1 = new System.Windows.Forms.TextBox();
            this.txtMemInstrucciones2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtHilillosFinalizados = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtQuantum);
            this.panel1.Controls.Add(this.btnIniciar);
            this.panel1.Controls.Add(this.btnCargarArchivos);
            this.panel1.Location = new System.Drawing.Point(329, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 189);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(88, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "SIMULADOR MIPS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Quantum:";
            // 
            // txtQuantum
            // 
            this.txtQuantum.Location = new System.Drawing.Point(120, 60);
            this.txtQuantum.Name = "txtQuantum";
            this.txtQuantum.Size = new System.Drawing.Size(75, 20);
            this.txtQuantum.TabIndex = 1;
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(120, 104);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 0;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnCargarArchivos
            // 
            this.btnCargarArchivos.Location = new System.Drawing.Point(92, 143);
            this.btnCargarArchivos.Name = "btnCargarArchivos";
            this.btnCargarArchivos.Size = new System.Drawing.Size(134, 23);
            this.btnCargarArchivos.TabIndex = 4;
            this.btnCargarArchivos.Text = "Cargar Archivos";
            this.btnCargarArchivos.UseVisualStyleBackColor = true;
            this.btnCargarArchivos.Click += new System.EventHandler(this.btnCargarArchivos_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtArchivo
            // 
            this.txtArchivo.Location = new System.Drawing.Point(652, 12);
            this.txtArchivo.Multiline = true;
            this.txtArchivo.Name = "txtArchivo";
            this.txtArchivo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtArchivo.Size = new System.Drawing.Size(312, 189);
            this.txtArchivo.TabIndex = 4;
            // 
            // txtVerLista
            // 
            this.txtVerLista.Location = new System.Drawing.Point(16, 12);
            this.txtVerLista.Multiline = true;
            this.txtVerLista.Name = "txtVerLista";
            this.txtVerLista.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVerLista.Size = new System.Drawing.Size(294, 357);
            this.txtVerLista.TabIndex = 5;
            // 
            // txtMemInstrucciones1
            // 
            this.txtMemInstrucciones1.Location = new System.Drawing.Point(329, 232);
            this.txtMemInstrucciones1.Multiline = true;
            this.txtMemInstrucciones1.Name = "txtMemInstrucciones1";
            this.txtMemInstrucciones1.Size = new System.Drawing.Size(312, 137);
            this.txtMemInstrucciones1.TabIndex = 6;
            // 
            // txtMemInstrucciones2
            // 
            this.txtMemInstrucciones2.Location = new System.Drawing.Point(652, 232);
            this.txtMemInstrucciones2.Multiline = true;
            this.txtMemInstrucciones2.Name = "txtMemInstrucciones2";
            this.txtMemInstrucciones2.Size = new System.Drawing.Size(312, 137);
            this.txtMemInstrucciones2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Memoria de Instrucciones 0 ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(649, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Memoria de Instrucciones 1 ";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(889, 504);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtHilillosFinalizados
            // 
            this.txtHilillosFinalizados.Location = new System.Drawing.Point(329, 411);
            this.txtHilillosFinalizados.Multiline = true;
            this.txtHilillosFinalizados.Name = "txtHilillosFinalizados";
            this.txtHilillosFinalizados.Size = new System.Drawing.Size(635, 87);
            this.txtHilillosFinalizados.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(326, 395);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Hilillos finalizados";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 539);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHilillosFinalizados);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMemInstrucciones2);
            this.Controls.Add(this.txtMemInstrucciones1);
            this.Controls.Add(this.txtVerLista);
            this.Controls.Add(this.txtArchivo);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulador MIPS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuantum;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtArchivo;
        private System.Windows.Forms.Button btnCargarArchivos;
        private System.Windows.Forms.TextBox txtVerLista;
        private System.Windows.Forms.TextBox txtMemInstrucciones1;
        private System.Windows.Forms.TextBox txtMemInstrucciones2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtHilillosFinalizados;
        private System.Windows.Forms.Label label5;
    }
}

