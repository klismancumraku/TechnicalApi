using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalWinForms.DTO;

namespace TechnicalWinForms.Services
{
    public class GeneralData
    {
        public static List<ProductDTO> GetProductsFromConnection(string constring)
        {
            List<ProductDTO> dataProducts = new List<ProductDTO>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Identifier, CodAlojamiento,Alojamiento,Direccion,Observaciones FROM ALOJAMIENTO", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            //dataGridView1.DataSource = dt;
                            //dataGridView1.Rows.Add("", "Edit");
                            List<object[]> prods = dt.AsEnumerable().Select(n => n.ItemArray).ToList();
                            foreach(var prod in prods)
                            {
                                ProductDTO prd = new ProductDTO
                                {
                                    Identifier = (int)prod[0],
                                    CodAlojamiento = prod[1] == DBNull.Value ? "" : (string)prod[1],
                                    Alojamiento = prod[2] == DBNull.Value ? "" : (string)prod[2],
                                    Direccion = prod[3] == DBNull.Value ? "" : (string)prod[3],
                                    Observaciones = prod[4]== DBNull.Value ? "" : (string)prod[4]
                                };
                                dataProducts.Add(prd);
                            }
                        }
                    }
                }
            }
            return dataProducts;
        }
    }
}
