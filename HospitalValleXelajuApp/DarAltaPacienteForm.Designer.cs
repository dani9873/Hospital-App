namespace HospitalValleXelajuApp
{
    partial class DarAltaPacienteForm
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
            this.cmbPlantas = new System.Windows.Forms.ComboBox();
            this.cmbCamas = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbPlantas
            // 
            this.cmbPlantas.FormattingEnabled = true;
            this.cmbPlantas.Location = new System.Drawing.Point(366, 77);
            this.cmbPlantas.Name = "cmbPlantas";
            this.cmbPlantas.Size = new System.Drawing.Size(121, 21);
            this.cmbPlantas.TabIndex = 1;
            // 
            // cmbCamas
            // 
            this.cmbCamas.FormattingEnabled = true;
            this.cmbCamas.Location = new System.Drawing.Point(366, 132);
            this.cmbCamas.Name = "cmbCamas";
            this.cmbCamas.Size = new System.Drawing.Size(121, 21);
            this.cmbCamas.TabIndex = 2;
            // 
            // DarAltaPacienteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbCamas);
            this.Controls.Add(this.cmbPlantas);
            this.Name = "DarAltaPacienteForm";
            this.Text = "DarAltaPacienteForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPlantas;
        private System.Windows.Forms.ComboBox cmbCamas;
    }
}