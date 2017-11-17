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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuantum = new System.Windows.Forms.TextBox();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtArchivo = new System.Windows.Forms.TextBox();
            this.btnCargarArchivos = new System.Windows.Forms.Button();
            this.txtVerLista = new System.Windows.Forms.TextBox();
            this.txtMemInstrucciones1 = new System.Windows.Forms.TextBox();
            this.txtMemInstrucciones2 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtQuantum);
            this.panel1.Controls.Add(this.btnIniciar);
            this.panel1.Location = new System.Drawing.Point(544, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 153);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(62, 17);
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
            this.label1.Location = new System.Drawing.Point(25, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Quantum:";
            // 
            // txtQuantum
            // 
            this.txtQuantum.Location = new System.Drawing.Point(94, 57);
            this.txtQuantum.Name = "txtQuantum";
            this.txtQuantum.Size = new System.Drawing.Size(75, 20);
            this.txtQuantum.TabIndex = 1;
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(94, 101);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 0;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtArchivo
            // 
            this.txtArchivo.Location = new System.Drawing.Point(842, 12);
            this.txtArchivo.Multiline = true;
            this.txtArchivo.Name = "txtArchivo";
            this.txtArchivo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtArchivo.Size = new System.Drawing.Size(294, 153);
            this.txtArchivo.TabIndex = 4;
            // 
            // btnCargarArchivos
            // 
            this.btnCargarArchivos.Location = new System.Drawing.Point(638, 205);
            this.btnCargarArchivos.Name = "btnCargarArchivos";
            this.btnCargarArchivos.Size = new System.Drawing.Size(134, 23);
            this.btnCargarArchivos.TabIndex = 4;
            this.btnCargarArchivos.Text = "Cargar Archivos";
            this.btnCargarArchivos.UseVisualStyleBackColor = true;
            this.btnCargarArchivos.Click += new System.EventHandler(this.btnCargarArchivos_Click);
            // 
            // txtVerLista
            // 
            this.txtVerLista.Location = new System.Drawing.Point(106, 29);
            this.txtVerLista.Multiline = true;
            this.txtVerLista.Name = "txtVerLista";
            this.txtVerLista.Size = new System.Drawing.Size(294, 495);
            this.txtVerLista.TabIndex = 5;
            // 
            // txtMemInstrucciones1
            // 
            this.txtMemInstrucciones1.Location = new System.Drawing.Point(479, 289);
            this.txtMemInstrucciones1.Multiline = true;
            this.txtMemInstrucciones1.Name = "txtMemInstrucciones1";
            this.txtMemInstrucciones1.Size = new System.Drawing.Size(336, 168);
            this.txtMemInstrucciones1.TabIndex = 6;
            // 
            // txtMemInstrucciones2
            // 
            this.txtMemInstrucciones2.Location = new System.Drawing.Point(883, 289);
            this.txtMemInstrucciones2.Multiline = true;
            this.txtMemInstrucciones2.Name = "txtMemInstrucciones2";
            this.txtMemInstrucciones2.Size = new System.Drawing.Size(336, 168);
            this.txtMemInstrucciones2.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 634);
            this.Controls.Add(this.txtMemInstrucciones2);
            this.Controls.Add(this.txtMemInstrucciones1);
            this.Controls.Add(this.txtVerLista);
            this.Controls.Add(this.btnCargarArchivos);
            this.Controls.Add(this.txtArchivo);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

