using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Mvc;

namespace _2016_03_29_Asp.net.Models
{
    public class CustomersService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }


        public List<SelectListItem> GetCustomersArray()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT CustomerID,CompanyName FROM Sales.Customers ORDER BY CompanyName";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(result);
                conn.Close();
            }
            return this.MapCustomerList(result);

        }

        public List<SelectListItem> MapCustomerList(DataTable CustomersData)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            foreach (DataRow row in CustomersData.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CompanyName"].ToString(),
                    Value = row["CustomerID"].ToString()
                });
            }

            return result;

        }

        public string InsertOrder(Orders order)
        {
            string sql = @"INSERT INTO Sales.Orders
                           (CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipperID,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry) 
                           VALUES (@CustomerID,@EmployeeID,@OrderDate,@RequiredDate,@ShippedDate,@ShipperID,@Freight,@ShipName,@ShipAddress,@ShipCity,@ShipRegion,@ShipPostalCode,@ShipCountry)
                           ";
            
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                 command.Parameters.Add(new SqlParameter("@CustomerID", order.CustomerID));
                command.Parameters.Add(new SqlParameter("@EmployeeID", order.EmployeeID));
                command.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                command.Parameters.Add(new SqlParameter("@RequiredDate", order.RequiredDate));
                command.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate));
                command.Parameters.Add(new SqlParameter("@ShipperID", order.ShipperID));
                command.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                command.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
                command.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
                command.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
                command.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
                command.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
                command.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
                command.ExecuteNonQuery();
                conn.Close();
            }
            return "succes";
        }
    }
}