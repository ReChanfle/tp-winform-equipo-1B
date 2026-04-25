using dominio;
using infraestructura;
using servicio;
using System;
using System.Collections.Generic;
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

        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                var conexion = new ConexionDb(
                    "server=localhost\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true"
                );

                var repo = new ArticuloRepository(conexion);
                var service = new ArticuloService(repo);

                dataGridView1.DataSource = service.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;

            FormArticulo frm = new FormArticulo(seleccionado);
            frm.ShowDialog();

            // refreshhhhh
            btnCargar_Click(sender, e);
        }
    }
}
