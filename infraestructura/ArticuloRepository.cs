using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using dominio;

namespace infraestructura
{
    public class ArticuloRepository
    {
        private readonly ConexionDb _factory;

        public ArticuloRepository(ConexionDb factory)
        {
            _factory = factory;
        }

        public void Update(Articulo art)
        {
            using (var conn = _factory.CreateConnection())
            {
                conn.Open();

                var query = @"UPDATE Articulos SET
                Codigo = @codigo,
                Nombre = @nombre,
                Descripcion = @desc,
                Precio = @precio,
                IdMarca = @IdMarca,
                IdCategoria = @IdCategoria
              WHERE Id = @id";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codigo", art.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", art.Nombre);
                    cmd.Parameters.AddWithValue("@desc", art.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", art.Precio);
                    cmd.Parameters.AddWithValue("@IdMarca", art.IdMarca);
                    cmd.Parameters.AddWithValue("@IdCategoria", art.IdCategoria);
                    cmd.Parameters.AddWithValue("@id", art.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Articulo> GetAll()
        {
            var lista = new List<Articulo>();

            using (var conn = _factory.CreateConnection())
            {
                conn.Open();

                var query = @"
            SELECT 
                A.Id,
                A.Codigo,
                A.Nombre,
                A.Descripcion,
                M.Id AS IdMarca,
                M.Descripcion AS MarcaDes,
                C.Id AS IdCategoria,
                C.Descripcion AS CategoriaDes,
                A.Precio
            FROM ARTICULOS A
            LEFT JOIN MARCAS M ON A.IdMarca = M.Id
            LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Articulo
                        {
                            Id = (int)reader["Id"],
                            Codigo = reader["Codigo"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = (decimal)reader["Precio"],
                            Marca = reader["MarcaDes"]?.ToString() ?? "Sin marca",
                            Categoria = reader["CategoriaDes"]?.ToString() ?? "Sin categoria",
                            IdCategoria = reader["IdCategoria"] != DBNull.Value ? (int?)reader["IdCategoria"] : null,
                            IdMarca = reader["IdMarca"] != DBNull.Value ? (int?)reader["IdMarca"] : null
                        });
                    }
                }
            }

            return lista;
        }

        public List<Articulo> Filtrar(int idMarca, int idCategoria)
        {
            var lista = new List<Articulo>();

            using (var conn = _factory.CreateConnection())
            {
                conn.Open();

                var query = @"
            SELECT 
                A.Id,
                A.Codigo,
                A.Nombre,
                A.Descripcion,
                M.Id AS IdMarca,
                M.Descripcion AS Marca,
                C.Id AS IdCategoria,
                C.Descripcion AS Categoria,
                A.Precio
            FROM ARTICULOS A
            LEFT JOIN MARCAS M ON A.IdMarca = M.Id
            LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id
            WHERE (@IdMarca = 0 OR A.IdMarca = @IdMarca)
            AND (@IdCategoria = 0 OR A.IdCategoria = @IdCategoria)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdMarca", idMarca);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Articulo
                            {
                                Id = (int)reader["Id"],
                                Codigo = reader["Codigo"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Precio = (decimal)reader["Precio"],
                                Marca = reader["Marca"].ToString(),
                                Categoria = reader["Categoria"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public int Add(Articulo art)
        {
            var conn = _factory.CreateConnection();
            conn.Open();

            var query = @"INSERT INTO Articulos 
                  (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) 
                  VALUES (@codigo, @nombre, @desc, @precio, @idMarca, @idCategoria);
                  SELECT SCOPE_IDENTITY();";

            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@codigo", art.Codigo);
            cmd.Parameters.AddWithValue("@nombre", art.Nombre);
            cmd.Parameters.AddWithValue("@desc", art.Descripcion);
            cmd.Parameters.AddWithValue("@precio", art.Precio);
            cmd.Parameters.AddWithValue("@idMarca", art.IdMarca);
            cmd.Parameters.AddWithValue("@idCategoria", art.IdCategoria);

            int newId = Convert.ToInt32(cmd.ExecuteScalar());

            conn.Close();

            return newId;
        }

        public void Delete(int id)
        {
            using (var conn = _factory.CreateConnection())
            {
                conn.Open();

                var query = "DELETE FROM ARTICULOS WHERE Id = @id";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
