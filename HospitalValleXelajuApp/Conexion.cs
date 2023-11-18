using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace HospitalValleXelajuApp
{
    internal class Conexion
    {
        public OleDbConnection con { get; private set; }

        public Conexion()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Usuario\Documents\UMG\Proyecto\HospitalValleXelajuApp\bdValledeXelaju.accdb";
            con = new OleDbConnection(connectionString);
        }

        public void AbrirConexion()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
        }

        public void CerrarConexion()
        {
            try
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
            }
        }

        public DataTable ObtenerRegistros(string tabla)
        {
            AbrirConexion();

            DataTable dt = new DataTable();

            try
            {
                string query = "SELECT * FROM " + tabla;
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los registros de la tabla " + tabla + ": " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return dt;
        }

        public void InsertarRegistro(string tabla, Dictionary<string, object> datos)
        {
            AbrirConexion();

            try
            {
                string query = "INSERT INTO " + tabla + " (";
                string values = "VALUES (";

                OleDbCommand cmd = new OleDbCommand();

                foreach (var kvp in datos)
                {
                    query += kvp.Key + ", ";
                    values += "@" + kvp.Key + ", ";
                    cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                }

                query = query.TrimEnd(',', ' ') + ")";
                values = values.TrimEnd(',', ' ') + ")";

                cmd.CommandText = query + " " + values;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el registro en la tabla " + tabla + ": " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public void ModificarRegistro(string tabla, string condicion, Dictionary<string, object> datos)
        {
            AbrirConexion();

            try
            {
                string query = "UPDATE " + tabla + " SET ";

                OleDbCommand cmd = new OleDbCommand();

                foreach (var kvp in datos)
                {
                    query += kvp.Key + " = @" + kvp.Key + ", ";
                    cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                }

                query = query.TrimEnd(',', ' ') + " WHERE " + condicion;

                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar el registro en la tabla " + tabla + ": " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public void EliminarRegistro(string tabla, string condicion)
        {
            AbrirConexion();

            try
            {
                string query = "DELETE FROM " + tabla + " WHERE " + condicion;

                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el registro de la tabla " + tabla + ": " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public DataTable BuscarRegistros(string tabla, string condicion)
        {
            AbrirConexion();

            DataTable dt = new DataTable();

            try
            {
                string query = "SELECT * FROM " + tabla + " WHERE " + condicion;
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar los registros en la tabla " + tabla + ": " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return dt;
        }
        public DataTable ObtenerAutomovilesDisponibles()
        {
            AbrirConexion();

            DataTable dt = new DataTable();

            try
            {
                string query = "SELECT * FROM Automoviles WHERE Disponibilidad = true";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los automóviles disponibles: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return dt;
        }
        public bool ExisteCodigo(string tabla, string columna, string codigo)
        {
            AbrirConexion();

            try
            {
                string query = "SELECT COUNT(*) FROM " + tabla + " WHERE " + columna + " = @codigo";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar el código en la tabla " + tabla + ": " + ex.Message);
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }


    }
}

