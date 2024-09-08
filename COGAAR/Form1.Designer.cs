namespace COGAAR
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnHabilitar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnGuardarPrograma = new System.Windows.Forms.Button();
            this.btnCargar = new System.Windows.Forms.Button();
            this.txtPrograma = new System.Windows.Forms.TextBox();
            this.btnEditar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTokens = new System.Windows.Forms.RichTextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnGuardarArchivo = new System.Windows.Forms.Button();
            this.dgvErroresLexicos = new System.Windows.Forms.DataGridView();
            this.cLinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTablaSimbolos = new System.Windows.Forms.DataGridView();
            this.cNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCantidadErrores = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErroresLexicos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablaSimbolos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnHabilitar);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btnGuardarPrograma);
            this.groupBox1.Controls.Add(this.btnCargar);
            this.groupBox1.Controls.Add(this.txtPrograma);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 308);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Programa Fuente";
            // 
            // btnHabilitar
            // 
            this.btnHabilitar.Location = new System.Drawing.Point(119, 263);
            this.btnHabilitar.Name = "btnHabilitar";
            this.btnHabilitar.Size = new System.Drawing.Size(75, 39);
            this.btnHabilitar.TabIndex = 9;
            this.btnHabilitar.TabStop = false;
            this.btnHabilitar.Text = "Editar\r\nPrograma";
            this.btnHabilitar.UseVisualStyleBackColor = true;
            this.btnHabilitar.Click += new System.EventHandler(this.btnHabilitar_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(19, 215);
            this.textBox1.TabIndex = 5;
            // 
            // btnGuardarPrograma
            // 
            this.btnGuardarPrograma.Location = new System.Drawing.Point(234, 263);
            this.btnGuardarPrograma.Name = "btnGuardarPrograma";
            this.btnGuardarPrograma.Size = new System.Drawing.Size(75, 39);
            this.btnGuardarPrograma.TabIndex = 4;
            this.btnGuardarPrograma.TabStop = false;
            this.btnGuardarPrograma.Text = "Guardar\r\nPrograma";
            this.btnGuardarPrograma.UseVisualStyleBackColor = true;
            this.btnGuardarPrograma.Click += new System.EventHandler(this.btnGuardarPrograma_Click);
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(6, 263);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(75, 39);
            this.btnCargar.TabIndex = 1;
            this.btnCargar.TabStop = false;
            this.btnCargar.Text = "Cargar\r\nPrograma";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // txtPrograma
            // 
            this.txtPrograma.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtPrograma.Enabled = false;
            this.txtPrograma.Location = new System.Drawing.Point(26, 19);
            this.txtPrograma.Multiline = true;
            this.txtPrograma.Name = "txtPrograma";
            this.txtPrograma.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtPrograma.Size = new System.Drawing.Size(283, 240);
            this.txtPrograma.TabIndex = 0;
            this.txtPrograma.WordWrap = false;
            this.txtPrograma.TextChanged += new System.EventHandler(this.txtPrograma_TextChanged);
           // this.txtPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrograma_KeyDown);
            this.txtPrograma.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrograma_KeyPress);
            this.txtPrograma.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtPrograma_PreviewKeyDown);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(327, 125);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(58, 56);
            this.btnEditar.TabIndex = 4;
            this.btnEditar.TabStop = false;
            this.btnEditar.Text = "Ejecutar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTokens);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.btnGuardarArchivo);
            this.groupBox2.Location = new System.Drawing.Point(385, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 308);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Archivo de Tokens";
            // 
            // txtTokens
            // 
            this.txtTokens.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtTokens.Location = new System.Drawing.Point(27, 19);
            this.txtTokens.Name = "txtTokens";
            this.txtTokens.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.txtTokens.Size = new System.Drawing.Size(282, 238);
            this.txtTokens.TabIndex = 10;
            this.txtTokens.TabStop = false;
            this.txtTokens.Text = "";
            this.txtTokens.WordWrap = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(6, 19);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(19, 215);
            this.textBox2.TabIndex = 9;
            // 
            // btnGuardarArchivo
            // 
            this.btnGuardarArchivo.Enabled = false;
            this.btnGuardarArchivo.Location = new System.Drawing.Point(6, 263);
            this.btnGuardarArchivo.Name = "btnGuardarArchivo";
            this.btnGuardarArchivo.Size = new System.Drawing.Size(75, 39);
            this.btnGuardarArchivo.TabIndex = 4;
            this.btnGuardarArchivo.TabStop = false;
            this.btnGuardarArchivo.Text = "Guardar\r\nArchivo";
            this.btnGuardarArchivo.UseVisualStyleBackColor = true;
            this.btnGuardarArchivo.Click += new System.EventHandler(this.btnGuardarArchivo_Click);
            // 
            // dgvErroresLexicos
            // 
            this.dgvErroresLexicos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErroresLexicos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cLinea,
            this.cError});
            this.dgvErroresLexicos.Location = new System.Drawing.Point(12, 355);
            this.dgvErroresLexicos.Name = "dgvErroresLexicos";
            this.dgvErroresLexicos.RowHeadersVisible = false;
            this.dgvErroresLexicos.Size = new System.Drawing.Size(315, 167);
            this.dgvErroresLexicos.TabIndex = 5;
            this.dgvErroresLexicos.TabStop = false;
            // 
            // cLinea
            // 
            this.cLinea.HeaderText = "Linea";
            this.cLinea.Name = "cLinea";
            this.cLinea.Width = 50;
            // 
            // cError
            // 
            this.cError.HeaderText = "ERROR";
            this.cError.Name = "cError";
            this.cError.Width = 262;
            // 
            // dgvTablaSimbolos
            // 
            this.dgvTablaSimbolos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTablaSimbolos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNum,
            this.cNombre});
            this.dgvTablaSimbolos.Location = new System.Drawing.Point(385, 356);
            this.dgvTablaSimbolos.Name = "dgvTablaSimbolos";
            this.dgvTablaSimbolos.RowHeadersVisible = false;
            this.dgvTablaSimbolos.Size = new System.Drawing.Size(315, 167);
            this.dgvTablaSimbolos.TabIndex = 6;
            this.dgvTablaSimbolos.TabStop = false;
            // 
            // cNum
            // 
            this.cNum.HeaderText = "NUM";
            this.cNum.Name = "cNum";
            this.cNum.Width = 50;
            // 
            // cNombre
            // 
            this.cNombre.HeaderText = "NOMBRE";
            this.cNombre.Name = "cNombre";
            this.cNombre.Width = 262;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Errores Lexicos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(382, 340);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tabla de Simbolos";
            // 
            // lblCantidadErrores
            // 
            this.lblCantidadErrores.AutoSize = true;
            this.lblCantidadErrores.Location = new System.Drawing.Point(216, 340);
            this.lblCantidadErrores.Name = "lblCantidadErrores";
            this.lblCantidadErrores.Size = new System.Drawing.Size(105, 13);
            this.lblCantidadErrores.TabIndex = 9;
            this.lblCantidadErrores.Text = "Cantidad de errores: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 534);
            this.Controls.Add(this.lblCantidadErrores);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.dgvTablaSimbolos);
            this.Controls.Add(this.dgvErroresLexicos);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "COGAAR";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErroresLexicos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablaSimbolos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGuardarPrograma;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGuardarArchivo;
        private System.Windows.Forms.DataGridView dgvErroresLexicos;
        private System.Windows.Forms.DataGridView dgvTablaSimbolos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnHabilitar;
        private System.Windows.Forms.Label lblCantidadErrores;
        private System.Windows.Forms.TextBox txtPrograma;
        private System.Windows.Forms.RichTextBox txtTokens;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLinea;
        private System.Windows.Forms.DataGridViewTextBoxColumn cError;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNombre;
    }
}

