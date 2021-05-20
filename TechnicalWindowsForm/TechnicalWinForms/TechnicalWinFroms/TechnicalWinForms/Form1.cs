using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using TechnicalWinForms;
using TechnicalWinForms.DTO;
using TechnicalWinForms.Services;

namespace TechnicalWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BindGrid();
        }
       
        public void BindGrid()
        {
            string constring = @"Data Source=212.101.89.7,55321;initial catalog=DDW04Pdb;user id=DDW04Pusu;password=T4buAzt2QcPYS7b";

            List<ProductDTO> products = GeneralData.GetProductsFromConnection(constring);
            dataGridView1.DataSource = products; 
            this.dataGridView1.Columns["Identifier"].Visible = false;

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.Name = "button";
                button.HeaderText = "";
                button.Text = "Edit";
                button.ToolTipText = "Edit";
                button.Width = 50;
                button.UseColumnTextForButtonValue = true;
            }
            if(dataGridView1.Columns.Contains("button") == false)
            {
                this.dataGridView1.Columns.Add(button);
            }

            this.dataGridView1.RowPrePaint
              += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(
                  this.dataGridView1_RowPrePaint);

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //ndryshon color te headerit
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            SetWidthColumns();
        }

        private void SetWidthColumns()
         {
            foreach (DataGridViewColumn columns in dataGridView1.Columns)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Beige;  // ndrsyhon color e backgorund te tabeles       
            //dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;  // ndryshon color e elementeve te gjithe tabeles       
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                Form2 f2 = new Form2(this);

                f2.txtId.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                f2.txtCodAlojamiento.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                f2.txtAlojamiento.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                f2.txtDireccion.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
                f2.txtObservaciones.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();     

                f2.ShowDialog();
            }
        }
    }
}
