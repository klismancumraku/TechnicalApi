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
                                    identifier = (int)prod[0],
                                    codAlojamiento = prod[1] == DBNull.Value ? "" : (string)prod[1],
                                    alojamiento = prod[2] == DBNull.Value ? "" : (string)prod[2],
                                    direccion = prod[3] == DBNull.Value ? "" : (string)prod[3],
                                    observaciones = prod[4]== DBNull.Value ? "" : (string)prod[4]
                                };
                                dataProducts.Add(prd);
                            }
                        }
                    }
                }
            }
            return dataProducts;
        }
        public static ProductDTO GetProductFromId(int id, string constring)
        {
            List<ProductDTO> dataProducts = new List<ProductDTO>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand($@"Select Identifier, CodAlojamiento,Alojamiento,Direccion,Observaciones FROM ALOJAMIENTO where Identifier = {id}", con))
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
                            foreach (var prod in prods)
                            {
                                ProductDTO prd = new ProductDTO
                                {
                                    identifier = (int)prod[0],
                                    codAlojamiento = prod[1] == DBNull.Value ? "" : (string)prod[1],
                                    alojamiento = prod[2] == DBNull.Value ? "" : (string)prod[2],
                                    direccion = prod[3] == DBNull.Value ? "" : (string)prod[3],
                                    observaciones = prod[4] == DBNull.Value ? "" : (string)prod[4]
                                };
                                dataProducts.Add(prd);
                            }
                        }
                    }
                }
            }
            return dataProducts.FirstOrDefault();
        }

        public static void UpdateProduct(ProductDTO dto, string constring)
        {
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("Update ALOJAMIENTO Set CodAlojamiento=@CodAlojamiento,Alojamiento=@Alojamiento," +
                "Direccion=@Direccion,Observaciones=@Observaciones Where Identifier=@Identifier", con);
            //Conn is SqlConnection object that You have created.
            cmd.Parameters.AddWithValue("@Identifier", dto.identifier);
            cmd.Parameters.AddWithValue("@CodAlojamiento", dto.codAlojamiento);
            cmd.Parameters.AddWithValue("@Alojamiento", dto.alojamiento);
            cmd.Parameters.AddWithValue("@Direccion", dto.direccion);
            cmd.Parameters.AddWithValue("@Observaciones", dto.observaciones);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
