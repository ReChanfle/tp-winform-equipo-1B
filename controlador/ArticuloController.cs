using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controlador
{
    public class ArticuloController
    {

        public List<string> Validate(Articulo art)
        {
            List<string> errores = new List<string>();

           
            if (string.IsNullOrWhiteSpace(art.Codigo))
                errores.Add("El código es obligatorio.");
            else if (art.Codigo.Length > 50)
                errores.Add("El código no puede superar 50 caracteres.");

          
            if (string.IsNullOrWhiteSpace(art.Nombre))
                errores.Add("El nombre es obligatorio.");
            else if (art.Nombre.Length > 50)
                errores.Add("El nombre no puede superar 50 caracteres.");

           
            if (!string.IsNullOrWhiteSpace(art.Descripcion) &&
                art.Descripcion.Length > 150)
                errores.Add("La descripción no puede superar 150 caracteres.");

           
            if (art.IdMarca == null || art.IdMarca <= 0)
                errores.Add("Debe seleccionar una marca.");

            
            if (art.IdCategoria == null || art.IdCategoria <= 0)
                errores.Add("Debe seleccionar una categoría.");

           
            if (art.Precio == null)
                errores.Add("El precio es obligatorio.");
            else if (art.Precio <= 0)
                errores.Add("El precio debe ser mayor a cero.");

            return errores;
        }


    }
}
