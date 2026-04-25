using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controlador
{
    public class CategoriaController
    {

        public List<string> Validate(Categoria categoria)
        {
            List<string> errores = new List<string>();

            if (string.IsNullOrWhiteSpace(categoria.Descripcion))
                errores.Add("La descripción es obligatoria.");
            else if (categoria.Descripcion.Length > 50)
                errores.Add("La descripción no puede superar 50 caracteres.");

            return errores;
        }


    }
}
