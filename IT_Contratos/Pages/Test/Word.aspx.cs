using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Text;

namespace RRHH5.Pages.Test
{
    public partial class Word : System.Web.UI.Page
    {
        public static object WdUnderline { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack  )
                {
                    DDCargarEmpresas();
                    CargarEmpleados();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        void DDCargarEmpresas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_FNC00101", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                ddEmpresas.DataSource = cmd.ExecuteReader();
                ddEmpresas.DataTextField = "Empresa";
                ddEmpresas.DataValueField = "Id_Empresa";
                ddEmpresas.DataBind();
                con.Close();
                ddEmpresas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDCargarFincas(int.Parse(ddEmpresas.SelectedValue));
        }
        public class Employee
        {
            public int Id_Empleado { get; set; }
            public string Nom_Ape { get; set; }
        }
        private void CargarEmpleados()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_CN_FNC202", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = IdEmpresa;
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
        void DDCargarFincas(long IdEmpresa)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00100", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = IdEmpresa;
                ddFincas.Items.Clear();
                con.Open();
                ddFincas.DataSource = cmd.ExecuteReader();
                ddFincas.DataTextField = "Finca";
                ddFincas.DataValueField = "Id_Finca";
                ddFincas.DataBind();
                ddFincas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void btnWord_Click(object sender, EventArgs e)
        {
            // No olvides agregar la referencia Microsoft.Office.Interop.Word a tu proyecto.
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var objAplicacion = new Microsoft.Office.Interop.Word.Application();
            var objDocumento = objAplicacion.Documents.Add();

            var objParrfafo1 = objDocumento.Content.Paragraphs.Add(Type.Missing);
            objParrfafo1.Range.Font.Size = 10;              // Tamaño de letra para el párrafo
            objParrfafo1.Range.Font.Name = "Lucida Bright"; // Tipo de letra para el párrafo
            objParrfafo1.Range.Text = "1.1\tCONTRATO INDIVIDUAL DE TRABAJO SUSCRITO ENTRE";
            objParrfafo1.Range.InsertParagraphAfter();

            var objParrfafo2 = objDocumento.Content.Paragraphs.Add(Type.Missing);
            objParrfafo2.Range.Font.Size = 12;      // Tamaño de letra para el párrafo
            objParrfafo2.Range.Font.Name = "Arial"; // Tipo de letra para el párrafo
            objParrfafo2.Range.Font.Bold = 1;       // Agregar negrita en el párrafo
            objParrfafo2.Range.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle; // Agregar subrayado
            objParrfafo2.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            objParrfafo2.Range.Text = "\r\nEXPORTADORA ENLASA, SOCIEDAD ANONIMA\r\nY EL TRABAJADOR MYNOR ESTUARDO CIFUENTES DEPAZ\r\n\r\n";
            objParrfafo2.Range.InsertParagraphAfter();

            var objParrfafo3 = objDocumento.Content.Paragraphs.Add(Type.Missing);
            objParrfafo3.Range.Font.Size = 12;      // Tamaño de letra para el párrafo
            objParrfafo3.Range.Font.Name = "Arial"; // Tipo de letra para el párrafo
            objParrfafo3.Range.Text = "1.1\tCONTRATO INDIVIDUAL DE TRABAJO SUSCRITO ENTRE test";
            objParrfafo3.Range.InsertParagraphAfter();

            //objDocumento.SaveAs2(ruta + "\\Contrato.docx");
            //objDocumento.Close();
            //objAplicacion.Quit();
            string nombreArchivo = NombreArchivo.Text + ".docx";
            objDocumento.SaveAs2(Path.Combine(ruta, nombreArchivo));
            objDocumento.Close();
            objAplicacion.Quit();
        }
    }
}
