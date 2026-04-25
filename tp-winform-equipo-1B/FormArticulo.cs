using dominio;
using infraestructura;
using servicio;
using System;
using System.Windows.Forms;

namespace tp_winform_equipo_1B
{
    public partial class FormArticulo : Form
    {
        private Articulo articulo = null;

        
        public FormArticulo()
        {
            InitializeComponent();
        }

        public FormArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        
        private void FormArticulo_Load(object sender, EventArgs e)
        {
            if (articulo != null)
            {
                txtId.Text = articulo.Id.ToString();

                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                
                var conexion = new ConexionDb(
                    "server=localhost\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true"
                );

                var repo = new ArticuloRepository(conexion);
                var service = new ArticuloService(repo);

                
                if (articulo == null)
                    articulo = new Articulo();

                
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                
                if (articulo.Id != 0)
                {
                    service.Update(articulo);
                    MessageBox.Show("Artículo modificado correctamente");
                }
                else
                {
                    service.Add(articulo);
                    MessageBox.Show("Artículo agregado correctamente");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}