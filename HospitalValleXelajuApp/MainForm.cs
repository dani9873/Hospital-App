using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HospitalValleXelajuApp
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        // Evento que se dispara cuando se carga el formulario principal
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Aquí puedes realizar operaciones iniciales, como cargar información en listas o controles, etc.
        }

        private void AsignarCamasForm_Click(object sender, EventArgs e)
        {
            AsignarCamasForm gestionPacientesForm = new AsignarCamasForm();
            gestionPacientesForm.Show();
        }

        private void AsignarDiagnostico_Click(object sender, EventArgs e)
        {
            AsignarDiagnosticoForm gestionPacientesForm = new AsignarDiagnosticoForm();
            gestionPacientesForm.Show();
        }

        private void AsignarMedicosForm_Click(object sender, EventArgs e)
        {
            AsignarMedicosForm gestionPacientesForm = new AsignarMedicosForm();
            gestionPacientesForm.Show();
        }

        private void DarAltaPacienteForm_Click(object sender, EventArgs e)
        {
            DarAltaPacienteForm gestionPacientesForm = new DarAltaPacienteForm();
            gestionPacientesForm.Show();
        }

        private void MantenimientoUsuariosForm_Click(object sender, EventArgs e)
        {
            MantenimientoUsuariosForm gestionPacientesForm = new MantenimientoUsuariosForm();
            gestionPacientesForm.Show();
        }

        private void RegistrarDiagnosticoForm_Click(object sender, EventArgs e)
        {
            RegistrarDiagnosticoForm gestionPacientesForm = new RegistrarDiagnosticoForm();
            gestionPacientesForm.Show();
        }

        private void RegistrarMedicoForm_Click(object sender, EventArgs e)
        {
            RegistrarMedicoForm gestionPacientesForm = new RegistrarMedicoForm();
            gestionPacientesForm.Show();
        }

        private void TrasladarPacientesForm_Click(object sender, EventArgs e)
        {
            TrasladarPacientesForm gestionPacientesForm = new TrasladarPacientesForm();
            gestionPacientesForm.Show();
        }

        private void RegistrarPacienteForm_Click(object sender, EventArgs e)
        {
            RegistrarPacienteForm gestionPacientesForm = new RegistrarPacienteForm();
            gestionPacientesForm.Show();
        }
    }
}
