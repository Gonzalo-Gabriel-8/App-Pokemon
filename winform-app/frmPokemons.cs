using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace winform_app
{
    public partial class frmPokemons : Form
    {
        private List<Pokemon> listaPokemons;
        public frmPokemons()
        {
            InitializeComponent();
        }

        private void frmPokemons_Load(object sender, EventArgs e)
        {
            refrescarFormulario();
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvPokemons.CurrentRow != null)
            {
                Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                CargaImagen(seleccionado.UrlImagen);
            }
           
        }

        private void refrescarFormulario()
        {
            PokemonNegocio negocio = new PokemonNegocio(); //crea una instancia de PokemonNegocio
            try
            {
                listaPokemons = negocio.listar();
                dgvPokemons.DataSource = listaPokemons;
                OcultarColumnas();
                pictureBoxPokemon.Load(listaPokemons[0].UrlImagen);

            }
            catch (Exception ex)
            {   /*larnzar un mensaje de advertencia pero no dejar que el programa caiga*/

                MessageBox.Show(ex.ToString());
            }
        } 

        private void OcultarColumnas()
        {
            dgvPokemons.Columns["UrlImagen"].Visible = false;
            dgvPokemons.Columns["Id"].Visible = false;
        }

        private void CargaImagen(string imagen)
        {
            try
            {
                pictureBoxPokemon.Load(imagen);
            }
            catch (Exception ex)
            {

                pictureBoxPokemon.Load("https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM=");
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            fmrAltaPokemon alta =new fmrAltaPokemon();

            alta.ShowDialog(); /*no poder ir a otra ventana*/

            refrescarFormulario();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado;
            seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem; //le paso por parametros el objeto pokemon que voy a modificar

            fmrAltaPokemon modificar = new fmrAltaPokemon(seleccionado); //llamo al otro constructor con el parametro

            modificar.ShowDialog(); 

            refrescarFormulario();
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }

        private void eliminar(bool logico=false)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            Pokemon seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas Eliminarlo?", "Eeliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;

                    if (logico)
                    {
                        negocio.EliminarLogico(seleccionado.Id);
                    }
                    else
                    {
                        negocio.Eliminar(seleccionado.Id);
                    }
                        
                    
                    refrescarFormulario();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            //Aplicar un filto sobre la listaPokemon

            List<Pokemon> ListaFiltrada;


            string filtro=txtFiltro.Text;

            if(filtro != "") //resetear la lista
            {
                ListaFiltrada = listaPokemons.FindAll(x => x.Nombre.ToLower().Contains(filtro.ToLower())); //una suerte de forEach para evaluar si el nombre del objeto es igual al filtro que le di
            }
            else
            {
                ListaFiltrada = listaPokemons;
            }
                      

            dgvPokemons.DataSource = null; //una limpieza

            dgvPokemons.DataSource = ListaFiltrada;

            OcultarColumnas();
        }
    }
}
