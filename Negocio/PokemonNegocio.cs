using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using System.Xml.Serialization.Configuration;


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
                comando.CommandText = "select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo and D.Id = P.IdDebilidad\r\n";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)lector["Id"];
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];

                    //validar datos null
                    if (!lector.IsDBNull (lector.GetOrdinal("UrlImagen"))) //si no es nulo, lo leo
                    aux.UrlImagen=(string) lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id =(int)lector["IdTipo"];
                   
                    aux.Tipo.Descripcion= (string)lector["Tipo"];
                    aux.Debilidad= new Elemento();
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
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
                datos.SetearConsulta("insert into POKEMONS (Numero, Nombre, Descripcion, Activo, IdTipo, IdDebilidad, UrlImagen) values (" + nuevo.Numero + ", '" + nuevo.Nombre +"', '" + nuevo.Descripcion +"', 1, @IdTipo,@IdDebilidad,@UrlImagen) \r\n");
                datos.SetearParametros("@IdTipo", nuevo.Tipo.Id);
                datos.SetearParametros("@IdDebilidad", nuevo.Tipo.Id);
                datos.SetearParametros("@UrlImagen", nuevo.UrlImagen);
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

        public void Modificar(Pokemon poke)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update POKEMONS set Numero=@numero, Nombre=@nombre, Descripcion=@descripcion, UrlImagen=@img, IdTipo=@idTipo,IdDebilidad=@idDebilidad where id=@id");
                datos.SetearParametros("numero", poke.Numero);
                datos.SetearParametros("@nombre", poke.Nombre);
                datos.SetearParametros("@descripcion", poke.Descripcion);
                datos.SetearParametros("@img", poke.UrlImagen);
                datos.SetearParametros("@idTipo", poke.Tipo.Id);
                datos.SetearParametros("@idDebilidad", poke.Debilidad.Id);
                datos.SetearParametros("@id", poke.Id);

                datos.EjecuctarAccion();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
    

}
