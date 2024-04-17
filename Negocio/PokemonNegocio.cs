using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;


namespace Negocio 
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar()
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=DESKTOP-A95RF6B; database=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion tipo, D.Descripcion Debilidad from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo and D.Id = P.IdDebilidad\r\n";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.UrlImagen=(string) lector["UrlImagen"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion= (string)lector["Tipo"];
                    aux.Debilidad= new Elemento();
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];
                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Agregar(Pokemon nuevo)
        {
            AccesoDatos datos =new AccesoDatos();
            try
            {
                datos.SetearConsulta("insert into POKEMONS (Numero, Nombre, Descripcion, Activo, IdTipo, IdDebilidad) values (" + nuevo.Numero + ", '" + nuevo.Nombre +"', '" + nuevo.Descripcion +"', 1, @IdTipo,@IdDebilidad) \r\n");
                datos.SetearParametros("@IdTipo", nuevo.Tipo.Id);
                datos.SetearParametros("@IdDebilidad", nuevo.Tipo.Id);
                /*ejecucion no query. Ejecucion de tipo no consulta*/
                datos.EjecuctarAccion();
            }
            catch (Exception ex )
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Pokemon modificar)
        {

        }
    }
    

}
