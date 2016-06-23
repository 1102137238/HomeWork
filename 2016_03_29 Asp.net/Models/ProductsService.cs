using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace _2016_03_29_Asp.net.Models
{
    public class ProductsService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        public List<SelectListItem> GetProductsArray()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT ProductID,ProductName FROM Production.Products ORDER BY ProductName";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = new SqlCommand(sql, conn);
                sqlAdapter.Fill(result);
                conn.Close();

            }
            return this.MapProductIDList(result);
        }

        private List<SelectListItem> MapProductIDList(DataTable orderData)
        {
            List<SelectListItem> result = new List<SelectListItem>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Value = row["ProductID"].ToString(),
                    Text = row["ProductName"].ToString()
                });
            }
            return result;
        }

        public List<SelectListItem> GetProductUnitPrice()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT UnitPrice FROM Production.Products";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = new SqlCommand(sql, conn);
                sqlAdapter.Fill(result);
                conn.Close();
            }
            return this.MapUnitPriceList(result);
        }

        private List<SelectListItem> MapUnitPriceList(DataTable orderData)
        {
            List<SelectListItem> result = new List<SelectListItem>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Value = row["UnitPrice"].ToString(),
                    Text = row["UnitPrice"].ToString()
                });
            }
            return result;
        }
    }
}