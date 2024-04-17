﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace winform_app
{
    public partial class fmrAltaPokemon : Form
    {
        public fmrAltaPokemon()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Pokemon poke=new Pokemon();

            PokemonNegocio negocio=new PokemonNegocio();

            try
            {
                poke.Numero= int.Parse(txtNumero.Text);

                poke.Nombre= txtNombre.Text;

                poke.Descripcion= txtDescripcion.Text;  

                negocio.Agregar(poke);

                MessageBox.Show("Agregado Exitosamente");

                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
