using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CompanyWebApp_Core.Models;

namespace CompanyWebApp_Core
{
    public class DataLayer
    {
        private string connectionString = "Data Source=DIVYANSHOO\\SQLEXPRESS;Initial Catalog=Company;Integrated Security=True;";

        // Get all departments
        public IEnumerable<Dept> GetDepts()
        {
            var lst = new List<Dept>();

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM Department_Details", con);
                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new Dept
                    {
                        Department_ID = Convert.ToInt32(rdr["Department_ID"]),
                        Department_Name = rdr["Department_Name"].ToString()
                    });
                }
            }
            return lst;
        }

        // Get a single department
        public Dept GetDept(int id)
        {
            Dept dpt = null;
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM Department_Details WHERE Department_ID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    dpt = new Dept
                    {
                        Department_ID = Convert.ToInt32(rdr["Department_ID"]),
                        Department_Name = rdr["Department_Name"].ToString()
                    };
                }
            }
            return dpt;
        }

        // Create
        public void CreateDept(Dept dpt)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("INSERT INTO Department_Details (Department_ID, Department_Name) VALUES (@id, @name)", con);
                cmd.Parameters.AddWithValue("@id", dpt.Department_ID);
                cmd.Parameters.AddWithValue("@name", dpt.Department_Name);
                cmd.ExecuteNonQuery();
            }
        }

        // Update
        public void UpdateDept(Dept dpt)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("UPDATE Department_Details SET Department_Name=@name WHERE Department_ID=@id", con);
                cmd.Parameters.AddWithValue("@id", dpt.Department_ID);
                cmd.Parameters.AddWithValue("@name", dpt.Department_Name);
                cmd.ExecuteNonQuery();
            }
        }

        // Delete
        public void DeleteDept(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("DELETE FROM Department_Details WHERE Department_ID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
