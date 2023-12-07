using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Office.Word;
using RRHH5.Pages.Admin;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace RRHH5.Pages.Puesto
{
    public partial class Crea_Puesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    DDLCargarProceso();
                    DDLCargarPuesto(DDLProceso.SelectedValue.ToString());
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //Cargar Listado de Empresas en DropDownList para modal Agregar
        protected void DDLCargarProceso()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_RCH00300", con);
                cmd.Parameters.Add("@Id_Proceso", System.Data.SqlDbType.Int).Value = Session["Id_Proceso"].ToString();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                DDLProceso.DataSource = cmd.ExecuteReader();
                DDLProceso.DataTextField = "DSCRIPTN";
                DDLProceso.DataValueField = "DEPRTMNT";
                DDLProceso.DataBind();
                con.Close();
                //DDLProceso.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void DDLProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLCargarPuesto(DDLProceso.SelectedValue.ToString());

        }
        protected void DDLCargarPuesto(string IdProceso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_RCH00301", con);
                cmd.Parameters.Add("@DEPRTMNT", SqlDbType.VarChar).Value = IdProceso;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                DDLPuesto.DataSource = cmd.ExecuteReader();
                DDLPuesto.DataTextField = "DSCRIPTN";
                DDLPuesto.DataValueField = "JOBTITLE";
                DDLPuesto.DataBind();
                con.Close();
                DDLPuesto.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void DDLPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // id1.Text = DDLProceso.Text;
        }
        private bool Validacreados()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM RCH00400 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto", con);
                cmd.Parameters.AddWithValue("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.Text;
                cmd.Parameters.AddWithValue("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.Text;
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        protected void Limpiar_Texbox()
        {
            Funcion_a.Text = "";
            Funcion_b.Text = "";
            Funcion_c.Text = "";
            Funcion_d.Text = "";
            Funcion_e.Text = "";
            Funcion_f.Text = "";
            Funcion_g.Text = "";
            Funcion_h.Text = "";
            Funcion_i.Text = "";
            Funcion_j.Text = "";
            Funcion_k.Text = "";
            Funcion_l.Text = "";
            Funcion_m.Text = "";
            Funcion_n.Text = "";
        }
        protected void Agregar_Click(object sender, EventArgs e)
        {
            if (Validacreados())
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Ya se han creado funciones para el proceso y puesto seleccionado!', 'error')", true);
                return;
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_AG_RCH00400", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = Session["Id_Empresa"].ToString();
                    //cmd.Parameters.Add("@Id_Gerencia", System.Data.SqlDbType.Int).Value = Id_Gerencia;
                    //cmd.Parameters.Add("@IdGerencia", System.Data.SqlDbType.VarChar).Value = IdGerencia;
                    //cmd.Parameters.Add("@Id_Proceso", System.Data.SqlDbType.Int).Value = Id_Proceso;
                    cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.Text;
                    cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.Text;
                    cmd.Parameters.Add("@Inciso_A", System.Data.SqlDbType.VarChar).Value = Funcion_a.Text;
                    cmd.Parameters.Add("@Inciso_B", System.Data.SqlDbType.VarChar).Value = Funcion_b.Text;
                    cmd.Parameters.Add("@Inciso_C", System.Data.SqlDbType.VarChar).Value = Funcion_c.Text;
                    cmd.Parameters.Add("@Inciso_D", System.Data.SqlDbType.VarChar).Value = Funcion_d.Text;
                    cmd.Parameters.Add("@Inciso_E", System.Data.SqlDbType.VarChar).Value = Funcion_e.Text;
                    cmd.Parameters.Add("@Inciso_F", System.Data.SqlDbType.VarChar).Value = Funcion_f.Text;
                    cmd.Parameters.Add("@Inciso_G", System.Data.SqlDbType.VarChar).Value = Funcion_g.Text;
                    cmd.Parameters.Add("@Inciso_H", System.Data.SqlDbType.VarChar).Value = Funcion_h.Text;
                    cmd.Parameters.Add("@Inciso_I", System.Data.SqlDbType.VarChar).Value = Funcion_i.Text;
                    cmd.Parameters.Add("@Inciso_J", System.Data.SqlDbType.VarChar).Value = Funcion_j.Text;
                    cmd.Parameters.Add("@Inciso_K", System.Data.SqlDbType.VarChar).Value = Funcion_k.Text;
                    cmd.Parameters.Add("@Inciso_L", System.Data.SqlDbType.VarChar).Value = Funcion_l.Text;
                    cmd.Parameters.Add("@Inciso_M", System.Data.SqlDbType.VarChar).Value = Funcion_m.Text;
                    cmd.Parameters.Add("@Inciso_N", System.Data.SqlDbType.VarChar).Value = Funcion_n.Text;
                    cmd.Parameters.Add("@Id_Usr_Crea", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                            "swal('Guardado con Exito!', 'Se han credo las funciones correctamente!', 'success')", true);
                    Limpiar_Texbox();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}