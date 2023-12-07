using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace RRHH5.Pages.Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        private void CargarEmpleados()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00202", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = Session["Id_Finca"].ToString();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Employee> empleados = new List<Employee>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int idEmpleado = reader.GetInt32(0);
                        string nombreEmpleado = reader.GetString(1);
                        empleados.Add(new Employee { Id_Empleado = idEmpleado, Nom_Ape = nombreEmpleado });
                    }
                }
                reader.Close();
                CheckBoxListEmpleados.DataSource = empleados;
                CheckBoxListEmpleados.DataTextField = "Nom_Ape";
                CheckBoxListEmpleados.DataValueField = "Id_Empleado";
                CheckBoxListEmpleados.DataBind();

                // Generar el código JavaScript para cargar los nombres de empleados en el cliente
                List<string> empleadosNombres = new List<string>();
                foreach (Employee empleado in empleados)
                {
                    empleadosNombres.Add(empleado.Nom_Ape);
                }
                string empleadosNombresJson = string.Join(",", empleadosNombres.Select(name => "\"" + name + "\""));
                Page.ClientScript.RegisterStartupScript(GetType(), "LoadEmployees", $"var empleados = [{empleadosNombresJson}];", true);
            }
        }
        public class Employee
        {
            public int Id_Empleado { get; set; }
            public string Nom_Ape { get; set; }
        }

    }
}