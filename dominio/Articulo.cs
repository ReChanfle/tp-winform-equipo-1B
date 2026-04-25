using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace dominio
{
    public class Articulo
    {

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int? IdMarca { get; set; }
        public int? IdCategoria { get; set; }

        public decimal? Precio { get; set; }

        public string Marca { get; set; }
        public string Categoria { get; set; }

        public List<Imagen> Imagenes { get; set; } = new List<Imagen>();



    }
}
