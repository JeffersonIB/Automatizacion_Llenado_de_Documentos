using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RRHH5.Pages.Indicadores
{
    public partial class Asigna_Indicador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
        }
    }
}