namespace HospitalValleXelajuApp
{
    partial class AsignarMedicosForm
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
            this.cmbPacientes = new System.Windows.Forms.ComboBox();
            this.cmbMedicos = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbPacientes
            // 
            this.cmbPacientes.FormattingEnabled = true;
            this.cmbPacientes.Location = new System.Drawing.Point(415, 59);
            this.cmbPacientes.Name = "cmbPacientes";
            this.cmbPacientes.Size = new System.Drawing.Size(121, 21);
            this.cmbPacientes.TabIndex = 2;
            // 
            // cmbMedicos
            // 
            this.cmbMedicos.FormattingEnabled = true;
            this.cmbMedicos.Location = new System.Drawing.Point(415, 129);
            this.cmbMedicos.Name = "cmbMedicos";
            this.cmbMedicos.Size = new System.Drawing.Size(121, 21);
            this.cmbMedicos.TabIndex = 3;
            // 
            // AsignarMedicosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbMedicos);
            this.Controls.Add(this.cmbPacientes);
            this.Name = "AsignarMedicosForm";
            this.Text = "AsignarMedicosForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPacientes;
        private System.Windows.Forms.ComboBox cmbMedicos;
    }
}