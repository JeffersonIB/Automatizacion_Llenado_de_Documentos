using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RRHH5.Pages.Forms
{
    public partial class FormsV2 : System.Web.UI.Page
    {
        //int IdFinca = 0;
        //int IdEmpresa = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            if (!IsPostBack && Session["Usuario"] != null)
            {
                //IdFinca = Convert.ToInt32(Session["Id_Finca"].ToString());
                //IdEmpresa = Convert.ToInt32(Session["Id_Empresa"].ToString());

                ViewState["CalificacionesDataTable"] = CreateDataTable();
                if (CheckBoxListEmpleados.Items.Count == 0)
                {
                    CargarLotes();
                    CargarEmpleados();
                    CheckBoxListEmpleados.DataBind();
                }
            }
            else
            {
                //Response.Redirect("~/Default.aspx");
            }
            //Actualiza el tiempo de última actividad de la sesión
           Session["LastActivity"] = DateTime.Now;
            DateTime lastActivity = (DateTime)Session["LastActivity"];
            TimeSpan timeSinceLastActivity = DateTime.Now - lastActivity;
            if (timeSinceLastActivity.TotalMinutes > Session.Timeout)
            {
                Response.Redirect("~/Default.aspx");
            }
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        void CargarLotes()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00500", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = Session["Id_Finca"].ToString();
                ddlLotes.Items.Clear();
                con.Open();
                ddlLotes.DataSource = cmd.ExecuteReader();
                ddlLotes.DataTextField = "Lote";
                ddlLotes.DataValueField = "Id_Lote";
                ddlLotes.DataBind();
                ddlLotes.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlLotes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcesos.ClearSelection();
            CargarProcesos(int.Parse(ddlLotes.SelectedValue));
            ddlActividad1.ClearSelection();
            ddlActividad2.ClearSelection();
            ddlActividad3.ClearSelection();
        }
        void CargarProcesos(long IdLote)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00300", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = IdFinca;
                cmd.Parameters.Add("@Id_Lote", SqlDbType.Int).Value = IdLote;
                ddlProcesos.Items.Clear();
                con.Open();
                ddlProcesos.DataSource = cmd.ExecuteReader();
                ddlProcesos.DataTextField = "Proceso";
                ddlProcesos.DataValueField = "Id_Proceso";
                ddlProcesos.DataBind();
                ddlProcesos.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlProcesos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlActividad1.ClearSelection();
            ddlActividad2.ClearSelection();
            ddlActividad3.ClearSelection();
            CargarActividad1(long.Parse(ddlProcesos.SelectedValue));
            CargarActividad2(long.Parse(ddlProcesos.SelectedValue));
            CargarActividad3(long.Parse(ddlProcesos.SelectedValue));
            //CargarddlTipo_Actividad();
            //CargarTipoTrabajo2();
            //CargarTipoTrabajo3();
        }
        void CargarActividad1(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlActividad1.Items.Clear();
                con.Open();
                ddlActividad1.DataSource = cmd.ExecuteReader();
                ddlActividad1.DataTextField = "Actividad";
                ddlActividad1.DataValueField = "Id_Actividad";
                ddlActividad1.DataBind();
                ddlActividad1.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlActividad1_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void CargarActividad2(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlActividad2.Items.Clear();
                con.Open();
                ddlActividad2.DataSource = cmd.ExecuteReader();
                ddlActividad2.DataTextField = "Actividad";
                ddlActividad2.DataValueField = "Id_Actividad";
                ddlActividad2.DataBind();
                ddlActividad2.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlActividad2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            TemplateField campoCantidad2 = (TemplateField)GridViewCalificaciones.Columns[4];
            campoCantidad2.Visible = true;
        }
        void CargarActividad3(long IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = IdProceso;
                ddlActividad3.Items.Clear();
                con.Open();
                ddlActividad3.DataSource = cmd.ExecuteReader();
                ddlActividad3.DataTextField = "Actividad";
                ddlActividad3.DataValueField = "Id_Actividad";
                ddlActividad3.DataBind();
                ddlActividad3.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlActividad3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            TemplateField campoCantidad3 = (TemplateField)GridViewCalificaciones.Columns[5];
            campoCantidad3.Visible = true;
        }
        void CargarTipo_Actividad(DropDownList ddlTipo_Actividad)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00401", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ddlTipo_Actividad.Items.Clear();
                con.Open();
                ddlTipo_Actividad.DataSource = cmd.ExecuteReader();
                ddlTipo_Actividad.DataTextField = "Tipo_Actividad";
                ddlTipo_Actividad.DataValueField = "Id_Tipo_Actividad";
                ddlTipo_Actividad.DataBind();
                //ddlTipoActividad.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CargarEmpleados()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00202", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = IdFinca;
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
            }
        }
        public class Employee
        {
            public int Id_Empleado { get; set; }
            public string Nom_Ape { get; set; }
        }
        protected void CustomValidatorEmpleados_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = CheckBoxListEmpleados.SelectedIndex != -1;
        }

        private DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id_Empleado", typeof(int));
            dataTable.Columns.Add("Nom_Ape", typeof(string));
            return dataTable;
        }
        protected void AgregarEmpleados_Click(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)ViewState["CalificacionesDataTable"];
            bool empleadoRepetido = false;

            foreach (ListItem item in CheckBoxListEmpleados.Items)
            {
                if (item.Selected)
                {
                    string idEmpleado = item.Value;
                    string nombreEmpleado = item.Text;
                    // Validar si el empleado ya está cargado en el GridView
                    bool empleadoExistente = false;
                    foreach (GridViewRow row in GridViewCalificaciones.Rows)
                    {
                        Label labelIdEmpleado = (Label)row.FindControl("LabelIdEmpleado");
                        if (labelIdEmpleado != null && labelIdEmpleado.Text == idEmpleado)
                        {
                            empleadoExistente = true;
                            empleadoRepetido = true;
                            break;
                        }
                    }
                    if (!empleadoExistente)
                    {
                        DataRow newRow = dataTable.NewRow();
                        newRow["Id_Empleado"] = idEmpleado;
                        newRow["Nom_Ape"] = nombreEmpleado;
                        dataTable.Rows.Add(newRow);
                    }
                }
            }
            GridViewCalificaciones.DataSource = dataTable;
            GridViewCalificaciones.DataBind();
            // Limpiar la selección de empleados
            foreach (ListItem item in CheckBoxListEmpleados.Items)
            {
                item.Selected = false;
            }
            if (empleadoRepetido)
            {
                LabelError.Text = "No se pueden agregar empleados duplicados.";
            }
            else
            {
                LabelError.Text = "";
            }
            // Cargar los DropDownList en el GridView por cada Empleado
            //foreach (GridViewRow row in GridViewCalificaciones.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        DropDownList ddlTipo_Actividad = (DropDownList)row.FindControl("ddlTipo_Actividad");
            //        CargarTipo_Actividad(ddlTipo_Actividad);

            //        if (ddlTipo_Actividad.ClientID.EndsWith("ddlActividad2") && ddlTipo_Actividad.SelectedValue != "")
            //        {
            //            TemplateField campoCantidad2 = (TemplateField)GridViewCalificaciones.Columns[4]; // Índice de la columna "Cantidad2"
            //            campoCantidad2.Visible = true;
            //        }
            //        else if (ddlTipo_Actividad.ClientID.EndsWith("ddlActividad3") && ddlTipo_Actividad.SelectedValue != "")
            //        {
            //            TemplateField campoCantidad3 = (TemplateField)GridViewCalificaciones.Columns[5]; // Índice de la columna "Cantidad3"
            //            campoCantidad3.Visible = true;
            //        }
            //    }
            //}
            foreach (GridViewRow row in GridViewCalificaciones.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlTipo_Actividad = (DropDownList)row.FindControl("ddlTipo_Actividad");
                    CargarTipo_Actividad(ddlTipo_Actividad);

                    if (ddlTipo_Actividad.ClientID.EndsWith("ddlActividad2") && ddlTipo_Actividad.SelectedValue != "")
                    {
                        TemplateField campoCantidad2 = (TemplateField)GridViewCalificaciones.Columns[4];
                        campoCantidad2.Visible = true;
                    }
                    else if (ddlTipo_Actividad.ClientID.EndsWith("ddlActividad3") && ddlTipo_Actividad.SelectedValue != "")
                    {
                        TemplateField campoCantidad3 = (TemplateField)GridViewCalificaciones.Columns[5];
                        campoCantidad3.Visible = true;
                    }
                }
            }
            Insertar.Visible = GridViewCalificaciones.Rows.Count > 0;
        }

        protected void Insertar_Click(object sender, EventArgs e)
        {
            Insertar.Visible = false;
            DataTable dataTable = (DataTable)ViewState["CalificacionesDataTable"];
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                foreach (DataRow row in dataTable.Rows)
                {
                    int IdEmpleado = Convert.ToInt32(row["Id_Empleado"]);
                    DropDownList ddlTipo_Actividad = GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("ddlTipo_Actividad") as DropDownList;
                    string Cantidad1 = ((TextBox)GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("txtCantidad1")).Text;
                    string Cantidad2 = ((TextBox)GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("txtCantidad2")).Text;
                    string Cantidad3 = ((TextBox)GridViewCalificaciones.Rows[row.Table.Rows.IndexOf(row)].FindControl("txtCantidad3")).Text;
                    if (ddlLotes != null)
                    {
                        SqlCommand cmd = new SqlCommand("SP_AG_FNC00601", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Finca", System.Data.SqlDbType.Int).Value = Session["Id_Finca"].ToString();
                        cmd.Parameters.AddWithValue("@Id_Empleado", IdEmpleado);
                        cmd.Parameters.AddWithValue("@Id_Lote", System.Data.SqlDbType.Int).Value = ddlLotes.Text;
                        cmd.Parameters.AddWithValue("@Id_Proceso", System.Data.SqlDbType.Int).Value = ddlProcesos.Text;
                        cmd.Parameters.AddWithValue("@Id_Actividad1", System.Data.SqlDbType.Int).Value = ddlActividad1.Text;
                        cmd.Parameters.AddWithValue("@Id_Actividad2", System.Data.SqlDbType.Int).Value = ddlActividad2.Text;
                        cmd.Parameters.AddWithValue("@Id_Actividad3", System.Data.SqlDbType.Int).Value = ddlActividad3.Text;
                        cmd.Parameters.AddWithValue("@Id_Tipo_Actividad1", ddlTipo_Actividad.SelectedValue);
                        cmd.Parameters.AddWithValue("@Cantidad1", Cantidad1);
                        cmd.Parameters.AddWithValue("@Cantidad2", Cantidad2);
                        cmd.Parameters.AddWithValue("@Cantidad3", Cantidad3);
                        cmd.Parameters.AddWithValue("@Id_Empresa", System.Data.SqlDbType.Int).Value = Session["Id_Empresa"].ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            GridViewCalificaciones.DataSource = null;
            GridViewCalificaciones.DataBind();
            ViewState["CalificacionesDataTable"] = CreateDataTable();
            ddlLotes.ClearSelection();
            ddlProcesos.ClearSelection();
            ddlActividad1.ClearSelection();
            ddlActividad2.ClearSelection();
            ddlActividad3.ClearSelection();
            CreateDataTable();
            TemplateField campoCantidad2 = (TemplateField)GridViewCalificaciones.Columns[4];
            campoCantidad2.Visible = false;
            TemplateField campoCantidad3 = (TemplateField)GridViewCalificaciones.Columns[5];
            campoCantidad3.Visible = false;
        }

    }
}