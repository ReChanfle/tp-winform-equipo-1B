using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infraestructura
{
    public class ImagenRepository
    {

        private readonly ConexionDb _factory;

        public ImagenRepository(ConexionDb factory)
        {
            _factory = factory;
        }

        public void Add(Imagen img)
        {
            using (var conn = _factory.CreateConnection())
            {
                conn.Open();

                var query = @"INSERT INTO IMAGENES
                              (IdArticulo, ImagenUrl)
                              VALUES (@idArticulo, @url)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@idArticulo", img.IdArticulo);
                cmd.Parameters.AddWithValue("@url", img.ImagenUrl);

                cmd.ExecuteNonQuery();
            }
        }

    }
}
