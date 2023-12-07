using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RRHH5.Pages.Puesto
{
    public partial class Puesto : System.Web.UI.Page
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
                cmd.Parameters.Add("@Id_Proceso", SqlDbType.Int).Value = 12; //Session["Id_Proceso"].ToString(); ;
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
                //DDLPuesto.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void DDLPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void ddlFincas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_a_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_b_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_c_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_d_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_e_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_f_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_g_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_h_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_i_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_j_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_k_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_l_Click(object sender, EventArgs e)
        {

        }
        protected void btnAutorizar_m_Click(object sender, EventArgs e)
        {

        }
        protected void Guardar_Click(object sender, EventArgs e)
        {
            int Id_Gerencia = 3;
            String IdGerencia = "00002";
            int Id_Proceso = 12;
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AG_RCH00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = Session["Id_Empresa"].ToString();
                cmd.Parameters.Add("@Id_Gerencia", System.Data.SqlDbType.Int).Value = Id_Gerencia;
                cmd.Parameters.Add("@IdGerencia", System.Data.SqlDbType.VarChar).Value = IdGerencia;
                cmd.Parameters.Add("@Id_Proceso", System.Data.SqlDbType.Int).Value = Id_Proceso;
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

                cmd.Parameters.Add("@Id_Usr_Crea", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                //throw;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
    }
}