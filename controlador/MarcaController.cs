using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controlador
{
    public class MarcaController
    {

        public List<string> Validate(Marca marca)
        {
            List<string> errores = new List<string>();

         
            if (string.IsNullOrWhiteSpace(marca.Descripcion))
                errores.Add("La descripción es obligatoria.");
            else if (marca.Descripcion.Length > 50)
                errores.Add("La descripción no puede superar 50 caracteres.");

            return errores;
        }


    }
}
