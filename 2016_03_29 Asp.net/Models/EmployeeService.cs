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
    public class EmployeeService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        public List<SelectListItem> GetEmployeeArray()
        {
            DataTable dt = new DataTable();
            string sql = @"Select EmployeeID As CodeId,LastName+'-'+FirstName As CodeName FROM HR.Employees";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapInfo(dt);
        }

        private List<SelectListItem> MapInfo(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            result.Add(new SelectListItem() {
                Text = "",
                Value = null
            });

            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CodeName"].ToString(),
                    Value = row["CodeId"].ToString()
                });
            }
            return result;
        }

    }
}