using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;


namespace _2016_03_29_Asp.net.Models
{
    public class OrderService
    {

        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }


        public List<Orders> GetOrders(Models.Orders orders) {

            string sql = @"select OrderId ,CompanyName,OrderDate,ShippedDate 
                from Sales.Orders o join Sales.Customers c on o.CustomerID = c.CustomerID 
                where (c.CompanyName LIKE '%'+@CompanyName+'%' OR @CompanyName = '') AND 
                           (o.OrderID LIKE @OrderID OR @OrderID = '') AND 
                            (o.EmployeeID LIKE @EmployeeID OR @EmployeeID = '') AND 
                            (o.ShipperID LIKE @ShipperID OR @ShipperID = '') AND 
                            (OrderDate >= @OrderDate AND OrderDate < DATEADD(DAY,1,@OrderDate) OR @OrderDate = '') AND 
                            (ShippedDate >= @ShippedDate AND ShippedDate < DATEADD(DAY,1,@ShippedDate) OR @ShippedDate = '')";


            string conn = this.GetDBConnectionString();
            SqlConnection sqlCon = new SqlConnection(conn);
            SqlDataAdapter sqlAdp = new SqlDataAdapter();
            SqlCommand cmd;
            DataSet ds = new DataSet();
            sqlCon.Open();

            cmd = new SqlCommand(sql, sqlCon);
            cmd.Parameters.Add(new SqlParameter("@OrderID", orders.OrderID == null ? string.Empty : orders.OrderID));
            cmd.Parameters.Add(new SqlParameter("@CompanyName", orders.CompanyName == null ? string.Empty : orders.CompanyName));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", orders.EmployeeID == null ? string.Empty : orders.EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", orders.ShipperID == null ? string.Empty : orders.ShipperID));
            cmd.Parameters.Add(new SqlParameter("@OrderDate", orders.OrderDate == null ? string.Empty : orders.OrderDate));
            cmd.Parameters.Add(new SqlParameter("@ShippedDate", orders.ShippedDate == null ? string.Empty : orders.ShippedDate));

            sqlAdp.SelectCommand = cmd;
            sqlAdp.Fill(ds);
            return this.MapList(ds.Tables[0]);
        }


        private List<Orders> MapList(DataTable data)
        {
            List<Orders> result = new List<Orders>();


            foreach (DataRow row in data.Rows)
            {
                result.Add(new Orders()
                {
                    OrderID = row["OrderID"].ToString(),
                    CompanyName = row["CompanyName"].ToString(),
                    OrderDate = row["OrderDate"].ToString(),
                    ShippedDate = row["ShippedDate"].ToString()
                });
            }
            return result;
        }

        public void DeleteOrderById(string OrderID)
        {
            try
            {
                string sql = "Delete FROM Sales.Orders Where OrderID=@OrderID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOrderDetailById(string OrderID)
        {
            try
            {
                string sql = "DELETE FROM Sales.OrderDetails WHERE OrderID=@OrderID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }catch (Exception ex) {
                throw ex;
            }
        }


    }
}