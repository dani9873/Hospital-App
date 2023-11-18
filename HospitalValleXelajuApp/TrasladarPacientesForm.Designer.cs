namespace HospitalValleXelajuApp
{
    partial class TrasladarPacientesForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbPlantas
            // 
            this.cmbPlantas.FormattingEnabled = true;
            this.cmbPlantas.Location = new System.Drawing.Point(411, 164);
            this.cmbPlantas.Name = "cmbPlantas";
            this.cmbPlantas.Size = new System.Drawing.Size(121, 21);
            this.cmbPlantas.TabIndex = 1;
            // 
            // cmbCamas
            // 
            this.cmbCamas.FormattingEnabled = true;
            this.cmbCamas.Location = new System.Drawing.Point(411, 222);
            this.cmbCamas.Name = "cmbCamas";
            this.cmbCamas.Size = new System.Drawing.Size(121, 21);
            this.cmbCamas.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(234, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Camas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(234, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Plantas";
            // 
            // TrasladarPacientesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCamas);
            this.Controls.Add(this.cmbPlantas);
            this.Name = "TrasladarPacientesForm";
            this.Text = "TrasladarPacientesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPlantas;
        private System.Windows.Forms.ComboBox cmbCamas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}