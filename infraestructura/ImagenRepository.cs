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
            var conn = _factory.CreateConnection();
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

        public void DeleteByArticuloId(int idArticulo)
        {
            var conn = _factory.CreateConnection();
            
                conn.Open();

                var query = "DELETE FROM IMAGENES WHERE IdArticulo = @id";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idArticulo);
                cmd.ExecuteNonQuery();
            
        }

        public List<Imagen> GetByArticuloId(int idArticulo)
        {
            List<Imagen> lista = new List<Imagen>();

            var conn = _factory.CreateConnection();
            
                conn.Open();

                var query = "SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo = @id";

            var cmd = new SqlCommand(query, conn);
                
                    cmd.Parameters.AddWithValue("@id", idArticulo);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Imagen
                        {
                            Id = (int)reader["Id"],
                            IdArticulo = (int)reader["IdArticulo"],
                            ImagenUrl = reader["ImagenUrl"].ToString()
                        });
                    }
                
            

            return lista;
        }

    }
}
