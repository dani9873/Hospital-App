namespace HospitalValleXelajuApp
{
    partial class AsignarCamasForm
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
            this.cmbCamas = new System.Windows.Forms.ComboBox();
            this.cmbPacientes = new System.Windows.Forms.ComboBox();
            this.cmbPlantas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbCamas
            // 
            this.cmbCamas.FormattingEnabled = true;
            this.cmbCamas.Location = new System.Drawing.Point(453, 129);
            this.cmbCamas.Name = "cmbCamas";
            this.cmbCamas.Size = new System.Drawing.Size(161, 21);
            this.cmbCamas.TabIndex = 0;
            this.cmbCamas.SelectedIndexChanged += new System.EventHandler(this.cmbCamas_SelectedIndexChanged);
            // 
            // cmbPacientes
            // 
            this.cmbPacientes.FormattingEnabled = true;
            this.cmbPacientes.Location = new System.Drawing.Point(453, 75);
            this.cmbPacientes.Name = "cmbPacientes";
            this.cmbPacientes.Size = new System.Drawing.Size(161, 21);
            this.cmbPacientes.TabIndex = 1;
            // 
            // cmbPlantas
            // 
            this.cmbPlantas.FormattingEnabled = true;
            this.cmbPlantas.Location = new System.Drawing.Point(453, 187);
            this.cmbPlantas.Name = "cmbPlantas";
            this.cmbPlantas.Size = new System.Drawing.Size(161, 21);
            this.cmbPlantas.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Paciente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Planta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Camas";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(508, 310);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(352, 310);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(75, 23);
            this.btnAsignar.TabIndex = 7;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // AsignarCamasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPlantas);
            this.Controls.Add(this.cmbPacientes);
            this.Controls.Add(this.cmbCamas);
            this.Name = "AsignarCamasForm";
            this.Text = "AsignarCamasForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCamas;
        private System.Windows.Forms.ComboBox cmbPacientes;
        private System.Windows.Forms.ComboBox cmbPlantas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAsignar;
    }
}