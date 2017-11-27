namespace ProyectoArqui
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
            this.btnInicioLento = new System.Windows.Forms.Button();
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
            this.txtCache00 = new System.Windows.Forms.TextBox();
            this.txtCache10 = new System.Windows.Forms.TextBox();
            this.txtdir0 = new System.Windows.Forms.TextBox();
            this.txtdir1 = new System.Windows.Forms.TextBox();
            this.txtCache01 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnInicioLento);
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
            // btnInicioLento
            // 
            this.btnInicioLento.Location = new System.Drawing.Point(164, 134);
            this.btnInicioLento.Name = "btnInicioLento";
            this.btnInicioLento.Size = new System.Drawing.Size(134, 23);
            this.btnInicioLento.TabIndex = 5;
            this.btnInicioLento.Text = "Inicio lento";
            this.btnInicioLento.UseVisualStyleBackColor = true;
            this.btnInicioLento.Click += new System.EventHandler(this.btnInicioLento_Click);
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
            this.btnIniciar.Location = new System.Drawing.Point(24, 134);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(134, 23);
            this.btnIniciar.TabIndex = 0;
            this.btnIniciar.Text = "Inicio Rapido";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnCargarArchivos
            // 
            this.btnCargarArchivos.Location = new System.Drawing.Point(91, 105);
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
            this.txtVerLista.Size = new System.Drawing.Size(294, 189);
            this.txtVerLista.TabIndex = 5;
            // 
            // txtMemInstrucciones1
            // 
            this.txtMemInstrucciones1.Location = new System.Drawing.Point(101, 232);
            this.txtMemInstrucciones1.Multiline = true;
            this.txtMemInstrucciones1.Name = "txtMemInstrucciones1";
            this.txtMemInstrucciones1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemInstrucciones1.Size = new System.Drawing.Size(312, 137);
            this.txtMemInstrucciones1.TabIndex = 6;
            // 
            // txtMemInstrucciones2
            // 
            this.txtMemInstrucciones2.Location = new System.Drawing.Point(546, 232);
            this.txtMemInstrucciones2.Multiline = true;
            this.txtMemInstrucciones2.Name = "txtMemInstrucciones2";
            this.txtMemInstrucciones2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemInstrucciones2.Size = new System.Drawing.Size(312, 137);
            this.txtMemInstrucciones2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Memoria de Datos 0 ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(543, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Memoria de Datos 1 ";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(893, 644);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtHilillosFinalizados
            // 
            this.txtHilillosFinalizados.Location = new System.Drawing.Point(104, 580);
            this.txtHilillosFinalizados.Multiline = true;
            this.txtHilillosFinalizados.Name = "txtHilillosFinalizados";
            this.txtHilillosFinalizados.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHilillosFinalizados.Size = new System.Drawing.Size(754, 87);
            this.txtHilillosFinalizados.TabIndex = 10;
            this.txtHilillosFinalizados.TextChanged += new System.EventHandler(this.txtHilillosFinalizados_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(98, 564);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Hilillos finalizados";
            // 
            // txtCache00
            // 
            this.txtCache00.Location = new System.Drawing.Point(101, 394);
            this.txtCache00.Multiline = true;
            this.txtCache00.Name = "txtCache00";
            this.txtCache00.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCache00.Size = new System.Drawing.Size(140, 137);
            this.txtCache00.TabIndex = 12;
            // 
            // txtCache10
            // 
            this.txtCache10.Location = new System.Drawing.Point(546, 395);
            this.txtCache10.Multiline = true;
            this.txtCache10.Name = "txtCache10";
            this.txtCache10.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCache10.Size = new System.Drawing.Size(312, 137);
            this.txtCache10.TabIndex = 13;
            // 
            // txtdir0
            // 
            this.txtdir0.Location = new System.Drawing.Point(16, 232);
            this.txtdir0.Multiline = true;
            this.txtdir0.Name = "txtdir0";
            this.txtdir0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtdir0.Size = new System.Drawing.Size(79, 137);
            this.txtdir0.TabIndex = 14;
            // 
            // txtdir1
            // 
            this.txtdir1.Location = new System.Drawing.Point(458, 232);
            this.txtdir1.Multiline = true;
            this.txtdir1.Name = "txtdir1";
            this.txtdir1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtdir1.Size = new System.Drawing.Size(79, 137);
            this.txtdir1.TabIndex = 15;
            // 
            // txtCache01
            // 
            this.txtCache01.Location = new System.Drawing.Point(273, 394);
            this.txtCache01.Multiline = true;
            this.txtCache01.Name = "txtCache01";
            this.txtCache01.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCache01.Size = new System.Drawing.Size(140, 137);
            this.txtCache01.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 378);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Caché 0";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(270, 378);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Caché 1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(543, 378);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Caché 0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Directorio 0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(458, 216);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Directorio 1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 678);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCache01);
            this.Controls.Add(this.txtdir1);
            this.Controls.Add(this.txtdir0);
            this.Controls.Add(this.txtCache10);
            this.Controls.Add(this.txtCache00);
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
        private System.Windows.Forms.TextBox txtCache00;
        private System.Windows.Forms.TextBox txtCache10;
        private System.Windows.Forms.TextBox txtdir0;
        private System.Windows.Forms.TextBox txtdir1;
        private System.Windows.Forms.TextBox txtCache01;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnInicioLento;
    }
}

