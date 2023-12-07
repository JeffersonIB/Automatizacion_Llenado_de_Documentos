using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RRHH5.Pages.Indicadores
{
    public partial class Crea_Indicadore : System.Web.UI.Page
    {
        int Id_Rol = 13;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    Tb_Finca(Id_Rol);
                    CargarDDLObjetivo();
                    CargarDDLSGC();
                    CargarDDObjetivo();
                    CargarDDSGC();
                    CargarDDLTC();
                    CargarDDTC();
                    DDLCargarProceso();
                    //Proceso13.Text = Session["13"].ToString();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
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
            //Tb_Finca(DDLProceso.SelectedValue.ToString());
        }
        //Cargar tabla con listado de indicadores
        void Tb_Finca(int Id_Rol)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_TB_IND00405", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = Session["Id_Empresa"].ToString();
                cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = Id_Rol;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvIndicadores.DataSource = dt;
                gvIndicadores.DataBind();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Reducir listado de GridView despues de 17 lineas
        protected void gvIndicadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            Tb_Finca(Id_Rol);
        }
        //Cargar Listado de Objetivo en DropDownList para modal Agregar
        public void CargarDDLObjetivo()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CN_IND00401";
                cmd.Connection = con;
                con.Open();
                ddlObjetivo.DataSource = cmd.ExecuteReader();
                ddlObjetivo.DataTextField = "Objetivo";
                ddlObjetivo.DataValueField = "Id_Objetivo";
                ddlObjetivo.DataBind();
                //ddlObjetivo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
        }
        //Cargar Listado de SGC en DropDownList para modal Agregar
        public void CargarDDLSGC()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CN_IND00402";
                cmd.Connection = con;
                con.Open();
                ddlSGC.DataSource = cmd.ExecuteReader();
                ddlSGC.DataTextField = "No_SGC";
                ddlSGC.DataValueField = "Id_No_SGC";
                ddlSGC.DataBind();
                ddlSGC.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
        }
        //Cargar Listado de Tipos de Calificaciones en DropDownList para modal Agregar
        public void CargarDDLTC()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CN_IND00410";
                cmd.Connection = con;
                con.Open();
                ddlTC.DataSource = cmd.ExecuteReader();
                ddlTC.DataTextField = "Descripcion";
                ddlTC.DataValueField = "Tipo_Califica";
                ddlTC.DataBind();
                ddlTC.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
        }
        //Obtener Ids de proceso y genrecia según seción para agregar nuevo indicador
        void ObtenerIdProceso(out int idProceso, out int idGerencia)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Id_Proceso, Id_Gerencia FROM IND00300 WHERE Id_Empresa = @Id_Empresa AND Proceso = 'Finca'", con);
            cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = Session["Id_Empresa"].ToString();
            SqlDataReader reader = cmd.ExecuteReader();
            idProceso = 0;
            idGerencia = 0;
            if (reader.Read())
            {
                string idProcesoStr = reader["Id_Proceso"].ToString();
                int.TryParse(idProcesoStr, out idProceso);

                string idGerenciaStr = reader["Id_Gerencia"].ToString();
                int.TryParse(idGerenciaStr, out idGerencia);
            }
            reader.Close();
            con.Close();
        }
        //Agrega nuevo Indicador dentro del Modal_Agregar
        protected void Agregar_Click(object sender, EventArgs e)
        {
            int idProceso, idGerencia;
            ObtenerIdProceso(out idProceso, out idGerencia);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AG_IND00405", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = Session["Id_Empresa"].ToString();
                cmd.Parameters.Add("@Id_Gerencia", System.Data.SqlDbType.Int).Value = idGerencia;
                cmd.Parameters.Add("@Id_Proceso", System.Data.SqlDbType.Int).Value = idProceso;
                cmd.Parameters.Add("@Id_Objetivo", System.Data.SqlDbType.Int).Value = ddlObjetivo.Text;
                cmd.Parameters.Add("@No_Indicador", System.Data.SqlDbType.Int).Value = txtNo.Text;
                cmd.Parameters.Add("@Id_No_SGC", System.Data.SqlDbType.Int).Value = ddlSGC.Text;
                cmd.Parameters.Add("@Indicador", System.Data.SqlDbType.VarChar).Value = txtIndicador.Text;
                cmd.Parameters.Add("@Meta_Proceso", System.Data.SqlDbType.Decimal).Value = txtMeta_Proceso.Text;
                cmd.Parameters.Add("@Peso_Proceso", System.Data.SqlDbType.Decimal).Value = txtPeso_Proceso.Text;
                cmd.Parameters.Add("@Meta_Objetivo", System.Data.SqlDbType.Decimal).Value = txtMeta_Objetivo.Text;
                cmd.Parameters.Add("@Peso_Objetivo", System.Data.SqlDbType.Decimal).Value = txtPeso_Objetivo.Text;
                cmd.Parameters.Add("@Id_Usr_Crea", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                cmd.Parameters.Add("@Id_Rol", System.Data.SqlDbType.Int).Value = Id_Rol;
                cmd.Parameters.Add("@Tipo_Califica", System.Data.SqlDbType.Int).Value = ddlTC.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/Pages/Indicadores/Indi_Finca.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar Listado de Objetivo en DropDownList para modal Actualizar
        public void CargarDDObjetivo()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CN_IND00401";
                cmd.Connection = con;
                con.Open();
                ddObjetivo.DataSource = cmd.ExecuteReader();
                ddObjetivo.DataTextField = "Objetivo";
                ddObjetivo.DataValueField = "Id_Objetivo";
                ddObjetivo.DataBind();
                //ddObjetivo.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
        }
        //Cargar Listado de SGC en DropDownList para modal Actualizar
        public void CargarDDSGC()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CN_IND00402";
                cmd.Connection = con;
                con.Open();
                ddSGC.DataSource = cmd.ExecuteReader();
                ddSGC.DataTextField = "No_SGC";
                ddSGC.DataValueField = "Id_No_SGC";
                ddSGC.DataBind();
                //ddSGC.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
        }
        //Cargar Listado de Tipos de Calificaciones en DropDownList para modal Actualizar
        public void CargarDDTC()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CN_IND00410";
                cmd.Connection = con;
                con.Open();
                ddTC.DataSource = cmd.ExecuteReader();
                ddTC.DataTextField = "Descripcion";
                ddTC.DataValueField = "Tipo_Califica";
                ddTC.DataBind();
                ddTC.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
        }

        //Actualizar registro dentro del Modal_Actualizar
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            //decimal Meta_Proceso;
            //decimal.TryParse(txMeta_Proceso.Text, out Meta_Proceso);

            //decimal Peso_Proceso;
            //decimal.TryParse(txPeso_Proceso.Text, out Peso_Proceso);

            //decimal Meta_Objetivo;
            //decimal.TryParse(txMeta_Objetivo.Text, out Meta_Objetivo);

            //decimal Peso_Objetivo;
            //decimal.TryParse(txPeso_Objetivo.Text, out Peso_Objetivo);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_IND00405", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Indicador", lbId_Indicador.Text);
                cmd.Parameters.Add("@Id_Objetivo", System.Data.SqlDbType.Int).Value = ddObjetivo.Text;
                cmd.Parameters.Add("@Id_No_SGC", System.Data.SqlDbType.Int).Value = ddSGC.Text;
                cmd.Parameters.Add("@No_Indicador", System.Data.SqlDbType.Int).Value = txNo.Text;
                cmd.Parameters.Add("@Indicador", System.Data.SqlDbType.VarChar).Value = txIndicador.Text;
                cmd.Parameters.Add("@Meta_Proceso", System.Data.SqlDbType.Decimal).Value = txMeta_Proceso.Text.Replace("%", "");
                cmd.Parameters.Add("@Peso_Proceso", System.Data.SqlDbType.Decimal).Value = txPeso_Proceso.Text;
                cmd.Parameters.Add("@Meta_Objetivo", System.Data.SqlDbType.Decimal).Value = txMeta_Objetivo.Text;
                cmd.Parameters.Add("@Peso_Objetivo", System.Data.SqlDbType.Decimal).Value = txPeso_Objetivo.Text;
                cmd.Parameters.Add("@Tipo_Califica", System.Data.SqlDbType.Int).Value = ddTC.Text;
                //cmd.Parameters.Add("@Indicador", System.Data.SqlDbType.VarChar).Value = txtIndicador.Text;
                //cmd.Parameters.Add("@Meta_Proceso", System.Data.SqlDbType.Decimal).Value = Meta_Proceso.ToString();
                //cmd.Parameters.Add("@Peso_Proceso", System.Data.SqlDbType.Decimal).Value = Peso_Proceso.ToString();
                //cmd.Parameters.Add("@Meta_Objetivo", System.Data.SqlDbType.Decimal).Value = Meta_Objetivo.ToString();
                //cmd.Parameters.Add("@Peso_Objetivo", System.Data.SqlDbType.Decimal).Value = Peso_Objetivo.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalAc(false);
                Response.Redirect("~/Pages/Indicadores/Indi_Finca.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Eliminar registro dentro del Modal_Eliminar
        protected void Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_El_IND00405", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Indicador", lblIndicaddor.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalEl(false);
                Response.Redirect("~/Pages/Indicadores/Indi_Finca.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Retraer Modal_Actualizar y Modal_Eliminar detro del GridView por Id_
        protected void gvIndicadores_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowModalAc")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lbId_Indicador.Text = gvIndicadores.DataKeys[gvrow.RowIndex].Value.ToString();
                lbId_Empresa.Text = (gvrow.FindControl("gvId_Empresa") as Label).Text;
                lbId_Gerencia.Text = (gvrow.FindControl("gvId_Gerencia") as Label).Text;
                lbId_Proceso.Text = (gvrow.FindControl("gvId_Proceso") as Label).Text;
                lbId_Indicador.Text = (gvrow.FindControl("gvId_Indicador") as Label).Text;
                ddObjetivo.Text = (gvrow.FindControl("gvId_Objetivo") as Label).Text;
                ddSGC.Text = (gvrow.FindControl("gvId_No_SGC") as Label).Text;
                txNo.Text = (gvrow.FindControl("gvNo_Indicador") as Label).Text;
                txIndicador.Text = (gvrow.FindControl("gvIndicador") as Label).Text;
                //txMeta_Proceso.Text = (gvrow.FindControl("gvMeta_Proceso") as Label).Text;
                txMeta_Proceso.Text = (gvrow.FindControl("gvMeta_Proceso") as Label).Text.Replace("%", "");
                txPeso_Proceso.Text = (gvrow.FindControl("gvPeso_Proceso") as Label).Text;
                txMeta_Objetivo.Text = (gvrow.FindControl("gvMeta_Objetivo") as Label).Text;
                txPeso_Objetivo.Text = (gvrow.FindControl("gvPeso_Objetivo") as Label).Text;
                ddTC.Text = (gvrow.FindControl("gvTipo_Califica") as Label).Text;
                ModalAc(true);
            }
            if (e.CommandName == "ShowModalEl")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lblIndicaddor.Text = gvIndicadores.DataKeys[gvrow.RowIndex].Value.ToString();
                lbNo.Text = (gvrow.FindControl("gvNo_Indicador") as Label).Text;
                lbIndicador.Text = (gvrow.FindControl("gvIndicador") as Label).Text;
                ModalEl(true);
            }
        }
        //Traer Modal_Actualizar
        void ModalAc(bool isDisplay)
        {
            StringBuilder builder = new StringBuilder();
            if (isDisplay)
            {
                builder.Append("<script language=JavaScript> ShowModalAc(); </script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModalAc", builder.ToString());
            }
            else
            {
                builder.Append("<script language=JavaScript> CloseModalAc(); </script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseModalAc", builder.ToString());
            }
        }
        //Traer Modal_Eliminar
        void ModalEl(bool isDisplay)
        {
            StringBuilder builder = new StringBuilder();
            if (isDisplay)
            {
                builder.Append("<script language=JavaScript> ShowModalEl(); </script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModalAc", builder.ToString());
            }
            else
            {
                builder.Append("<script language=JavaScript> CloseModalEl(); </script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseModalAc", builder.ToString());
            }
        }
        //Error con texto en mayuscula
        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/scripts/jquery-1.8.3.min.js",
                DebugPath = "~/scripts/jquery-1.8.3.js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.js"
            });
        }
    }
}