using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechnicalWinForms
{
    public partial class Form2 : Form
    {
        private Form1 form1;

        public Form2(Form1 form)
        {
            InitializeComponent();
            form1 = form;
        }

        string constring = @"Data Source=212.101.89.7,55321;initial catalog=DDW04Pdb;user id=DDW04Pusu;password=T4buAzt2QcPYS7b";

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(constring);
                SqlCommand cmd = new SqlCommand("Update ALOJAMIENTO Set CodAlojamiento=@CodAlojamiento,Alojamiento=@Alojamiento," +
                    "Direccion=@Direccion,Observaciones=@Observaciones Where Identifier=@Identifier", con);
                //Conn is SqlConnection object that You have created.
                cmd.Parameters.AddWithValue("@Identifier", txtId.Text);
                cmd.Parameters.AddWithValue("@CodAlojamiento", txtCodAlojamiento.Text);
                cmd.Parameters.AddWithValue("@Alojamiento", txtAlojamiento.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@Observaciones", txtObservaciones.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                txtCodAlojamiento.Text = "";
                txtAlojamiento.Text = "";
                txtDireccion.Text = "";
                txtObservaciones.Text = "";

                form1.BindGrid();
                MessageBox.Show("The data has been updated successfully!");

                this.Close();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
