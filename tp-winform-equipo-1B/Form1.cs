using System;
using infraestructura;
using servicio;
using System.Windows.Forms;

namespace tp_winform_equipo_1B
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                var conexion = new ConexionDb("Server=localhost,1433;Database=CATALOGO_P3_DB;User Id=sa;Password=NuevaPassword123;TrustServerCertificate=True;");
                var repo = new ArticuloRepository(conexion);
                var service = new ArticuloService(repo);
                var productos = service.Listar();
                dataGridView1.DataSource = productos;

            }
            catch (Exception ex)
            {
              
                MessageBox.Show($"Error al cargar los datos: {ex.ToString()}");
            }

        }
    }
}
