using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ElementoNegocio
    {
        public List<Elemento> Listar()
        {
			List<Elemento> lista = new List<Elemento>();
			
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.SetearConsulta("select Id, Descripcion from ELEMENTOS");
				datos.EjecutarLectura();

				while (datos.Lector.Read())
				{
					Elemento aux = new Elemento();
					aux.Id =(int) datos.Lector["ID"];
					aux.Descripcion = (string)datos.Lector["Descripcion"];

					lista.Add(aux);
				}
				return lista;
			}
			catch (Exception)
			{

				throw;
			}

			finally
			{
				datos.CerrarConexion(); 
			}
        }


    }
}
 