﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RRHH5.Pages.Admin
{
    public partial class Actividades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack && Session["Usuario"] != null)
                {
                    TB_Actividades();
                    DDLCargarEmpresas();
                    DDCargarEmpresas();
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //Cargar tabla con listado de Fincas
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string lote = txtBuscarActividad.Text.Trim();
                DataTable dt = GetFilteredData(lote);
                gvActividades.DataSource = dt;
                gvActividades.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private DataTable GetFilteredData(string lote)
        {
            SqlCommand cmd = new SqlCommand("SP_TB_FNC00400", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Id_Empresa", 1);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (!string.IsNullOrEmpty(lote))
            {
                // Filtrar los datos en la columna "Lote"
                dt.DefaultView.RowFilter = string.Format("Actividad LIKE '%{0}%'", lote);
                dt = dt.DefaultView.ToTable();
            }

            con.Close();
            return dt;
        }

        protected void TB_Actividades()
        {
            try
            {
                DataTable dt = GetFilteredData("");
                gvActividades.DataSource = dt;
                gvActividades.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Reducir listado de GridView despues de 17 lineas
        protected void gvActividades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            TB_Actividades();
        }
        //Cargar Listado de Empresas en DropDownList para Modal_Agregar
        void DDLCargarEmpresas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CN_FNC00101", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                ddlEmpresas.DataSource = cmd.ExecuteReader();
                ddlEmpresas.DataTextField = "Empresa";
                ddlEmpresas.DataValueField = "Id_Empresa";
                ddlEmpresas.DataBind();
                con.Close();
                ddlEmpresas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar Listado de Fincas en DropDownList para Modal_Agregar
        void DDLCargarFincas(long IdEmpresa)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00100", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = IdEmpresa;
                ddlFincas.Items.Clear();
                con.Open();
                ddlFincas.DataSource = cmd.ExecuteReader();
                ddlFincas.DataTextField = "Finca";
                ddlFincas.DataValueField = "Id_Finca";
                ddlFincas.DataBind();
                ddlFincas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar Listado de Lotes en DropDownList para Modal_Agregar
        void DDLCargarLotes(long IdFinca)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00500", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = IdFinca;
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
        //Cargar Listado de Procesos en DropDownList para Modal_Agregar
        void DDLCargarProcesos(long IdLote)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00300", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
        //Agrega nuevo registro dentro del Modal_Agregar
        protected void Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AG_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = ddlEmpresas.Text;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = ddlFincas.Text;
                cmd.Parameters.Add("@Id_Lote", System.Data.SqlDbType.Int).Value = ddlLotes.Text;
                cmd.Parameters.Add("@Id_Proceso", System.Data.SqlDbType.VarChar).Value = ddlProcesos.Text;
                cmd.Parameters.Add("@Actividad", System.Data.SqlDbType.VarChar).Value = txtActividad.Text;
                cmd.Parameters.Add("@CostoDia", System.Data.SqlDbType.Decimal).Value = txtCostoDia.Text;
                cmd.Parameters.Add("@CostoProd", System.Data.SqlDbType.Decimal).Value = txtCostoProd.Text;
                cmd.Parameters.Add("@Id_Usuario", System.Data.SqlDbType.VarChar).Value = Session["Id_Usuario"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/Pages/Admin/Actividades.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar Listado de Empresas en DropDownList para Modal_Actualizar
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
        //Cargar Listado de Fincas en DropDownList para Modal_Actualizar
        void DDCargarFincas(long IdEmpresa)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00100", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = IdEmpresa;
                ddlFincas.Items.Clear();
                con.Open();
                ddFincas.DataSource = cmd.ExecuteReader();
                ddFincas.DataTextField = "Finca";
                ddFincas.DataValueField = "Id_Finca";
                ddFincas.DataBind();
                ddlFincas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar Listado de Lotes en DropDownList para Modal_Actualizar
        void DDCargarLotes(long IdFinca)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00500", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Finca", SqlDbType.Int).Value = IdFinca;
                ddLotes.Items.Clear();
                con.Open();
                ddLotes.DataSource = cmd.ExecuteReader();
                ddLotes.DataTextField = "Lote";
                ddLotes.DataValueField = "Id_Lote";
                ddLotes.DataBind();
                ddlFincas.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Cargar Listado de Procesos en DropDownList para Modal_Actualizar
        void DDCargarProcesos(long IdLote)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_FNC00300", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Lote", SqlDbType.Int).Value = IdLote;
                ddProcesos.Items.Clear();
                con.Open();
                ddProcesos.DataSource = cmd.ExecuteReader();
                ddProcesos.DataTextField = "Proceso";
                ddProcesos.DataValueField = "Id_Proceso";
                ddProcesos.DataBind();
                ddProcesos.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Actualizar registro dentro del Modal_Actualizar
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Actividad", lbId_Actividad.Text);
                cmd.Parameters.Add("@Id_Empresa", System.Data.SqlDbType.Int).Value = ddEmpresas.Text;
                cmd.Parameters.Add("@Id_Finca", System.Data.SqlDbType.Int).Value = ddFincas.Text;
                cmd.Parameters.Add("@Id_Lote", System.Data.SqlDbType.Int).Value = ddLotes.Text;
                cmd.Parameters.Add("@Id_Proceso", System.Data.SqlDbType.Int).Value = ddProcesos.Text;
                cmd.Parameters.Add("@Actividad", System.Data.SqlDbType.VarChar).Value = txActividad.Text;
                cmd.Parameters.Add("@CostoDia", System.Data.SqlDbType.Decimal).Value = txCostoDia.Text;
                cmd.Parameters.Add("@CostoProd", System.Data.SqlDbType.Decimal).Value = txCostoProd.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalAc(false);
                Response.Redirect("~/Pages/Admin/Actividades.aspx");
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
                SqlCommand cmd = new SqlCommand("SP_EL_FNC00400", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Actividad", lId_Actividad.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalEl(false);
                Response.Redirect("~/Pages/Admin/Actividades.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Retraer Modal_Actualizar y Modal_Eliminar detro del GridView por Id_
        protected void gvActividades_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowModalAc")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lbId_Actividad.Text = gvActividades.DataKeys[gvrow.RowIndex].Value.ToString(); ;
                //lbEmpresas.Text = (gvrow.FindControl("gvEmpresa") as Label).Text;
                //lbFincas.Text = (gvrow.FindControl("gvFinca") as Label).Text;
                //lbLotes.Text = (gvrow.FindControl("gvLote") as Label).Text;
                //lbProceso.Text = (gvrow.FindControl("gvProceso") as Label).Text;
                ddEmpresas.Text = (gvrow.FindControl("gvId_Empresa") as Label).Text;
                ddFincas.Text = (gvrow.FindControl("gvId_Finca") as Label).Text;
                ddLotes.Text = (gvrow.FindControl("gvId_Lote") as Label).Text;
                ddProcesos.Text = (gvrow.FindControl("gvId_Proceso") as Label).Text;
                long idEmpresa = Convert.ToInt64((gvrow.FindControl("gvId_Empresa") as Label).Text);
                DDCargarFincas(idEmpresa);
                long idFinca = Convert.ToInt64((gvrow.FindControl("gvId_Finca") as Label).Text);
                DDCargarLotes(idFinca);
                long idLote = Convert.ToInt64((gvrow.FindControl("gvId_Lote") as Label).Text);
                DDCargarProcesos(idLote);
                txActividad.Text = (gvrow.FindControl("gvActividad") as Label).Text;
                txCostoDia.Text = (gvrow.FindControl("gvCosto_Dia") as Label).Text;
                txCostoProd.Text = (gvrow.FindControl("gvCosto_Produccion") as Label).Text;
                ModalAc(true);
            }
            if (e.CommandName == "ShowModalEl")
            {
                ImageButton btndetails = (ImageButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lId_Actividad.Text = gvActividades.DataKeys[gvrow.RowIndex].Value.ToString();
                lActividad.Text = (gvrow.FindControl("gvActividad") as Label).Text;
                ModalEl(true);
            }
        }
        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLCargarFincas(int.Parse(ddlEmpresas.SelectedValue));
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
        }
        protected void ddlFincas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLCargarLotes(int.Parse(ddlFincas.SelectedValue));
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
        }
        protected void ddlLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLCargarProcesos(int.Parse(ddlLotes.SelectedValue));
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
        }
        protected void ddlProcesos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Agregar", "$('#Modal_Agregar').modal('show')", true);
        }

        protected void ddEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDCargarFincas(int.Parse(ddEmpresas.SelectedValue));
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Actualizar", "$('#Modal_Actualizar').modal('show')", true);
        }
        protected void ddFincas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDCargarLotes(int.Parse(ddFincas.SelectedValue));
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Actualizar", "$('#Modal_Actualizar').modal('show')", true);
        }
        protected void ddLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDCargarProcesos(int.Parse(ddLotes.SelectedValue));
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Actualizar", "$('#Modal_Actualizar').modal('show')", true);
        }
        protected void ddProcesos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Modal_Actualizar", "$('#Modal_Actualizar').modal('show')", true);
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