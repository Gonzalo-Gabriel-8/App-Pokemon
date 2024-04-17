using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Negocio
{
    public class AccesoDatos
    {
        /* para crear una lectura*/
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        /*Metodo de acceso: poder leer el lector desde el exterior*/
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            /*crear la conexion y pasar por parametros (nombre de la maquina- nombre de la BBDD- tipo de seguridad)*/
            conexion = new SqlConnection("server=DESKTOP-A95RF6B; database=POKEDEX_DB; integrated security=true");

            /*una consulta. Una accion contra la BBDD*/
            comando = new SqlCommand();
        }

        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void EjecutarLectura()
        {
            comando.Connection= conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //Metodo para la ExecuteNonQuery
        public void EjecuctarAccion()
        { 
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void SetearParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void CerrarConexion()
        {
            if (lector!= null)
            {
                lector.Close();

                conexion.Close();
            }
            
        }
    }
}
