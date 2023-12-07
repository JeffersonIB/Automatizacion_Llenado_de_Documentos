using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using DocumentFormat.OpenXml.Wordprocessing;
//using DocumentFormat.OpenXml.Office.Word;
//using DocumentFormat.OpenXml.Office2010.Excel;

namespace RRHH5.Pages.Puesto
{
    public partial class Modifica_Puesto : System.Web.UI.Page
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
                    string idProceso = DDLProceso.SelectedValue.ToString();
                    string idPuesto = DDLPuesto.SelectedValue.ToString();
                    ValidaAutorizados(idProceso, idPuesto);
                }
            }
            catch
            {
                throw;
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //Cargar Listado de Procesos en DropDownList
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
        // Función al seleccionar el Proceso
        protected void DDLProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLCargarPuesto(DDLProceso.SelectedValue.ToString());
            Funcionescreadas(DDLProceso.SelectedValue.ToString(), DDLPuesto.SelectedValue.ToString());
            ValidaAutorizados(DDLProceso.SelectedValue.ToString(), DDLPuesto.SelectedValue.ToString());
        }
        //Cargar Listado de Puestos en DropDownList
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
        // Función al seleccionar el Puesto
        protected void DDLPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funcionescreadas(DDLProceso.SelectedValue.ToString(), DDLPuesto.SelectedValue.ToString());
            ValidaAutorizados(DDLProceso.SelectedValue.ToString(), DDLPuesto.SelectedValue.ToString());
        }
        // Trae el texto de las funciones por Proceso y puesto
        protected void Funcionescreadas(string idProceso, string idPuesto)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT IdProceso,IdPuesto,Inciso_A,Inciso_B,Inciso_C,Inciso_D,Inciso_E,Inciso_F,Inciso_G,Inciso_H,Inciso_I,Inciso_J,Inciso_K,Inciso_L,Inciso_M,Inciso_N FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto ", con);
                cmd.Parameters.AddWithValue("@IdProceso", idProceso);
                cmd.Parameters.AddWithValue("@IdPuesto", idPuesto);


                SqlDataReader FuncionReader = cmd.ExecuteReader();

                if (FuncionReader.Read())
                {
                    string Inciso_A = FuncionReader["Inciso_A"].ToString();
                    string Inciso_B = FuncionReader["Inciso_B"].ToString();
                    string Inciso_C = FuncionReader["Inciso_C"].ToString();
                    string Inciso_D = FuncionReader["Inciso_D"].ToString();
                    string Inciso_E = FuncionReader["Inciso_E"].ToString();
                    string Inciso_F = FuncionReader["Inciso_F"].ToString();
                    string Inciso_G = FuncionReader["Inciso_G"].ToString();
                    string Inciso_H = FuncionReader["Inciso_H"].ToString();
                    string Inciso_I = FuncionReader["Inciso_I"].ToString();
                    string Inciso_J = FuncionReader["Inciso_J"].ToString();
                    string Inciso_K = FuncionReader["Inciso_K"].ToString();
                    string Inciso_L = FuncionReader["Inciso_L"].ToString();
                    string Inciso_M = FuncionReader["Inciso_M"].ToString();
                    string Inciso_N = FuncionReader["Inciso_N"].ToString();

                    Funcion_a.Text = Inciso_A;
                    Funcion_b.Text = Inciso_B;
                    Funcion_c.Text = Inciso_C;
                    Funcion_d.Text = Inciso_D;
                    Funcion_e.Text = Inciso_E;
                    Funcion_f.Text = Inciso_F;
                    Funcion_g.Text = Inciso_G;
                    Funcion_h.Text = Inciso_H;
                    Funcion_i.Text = Inciso_I;
                    Funcion_j.Text = Inciso_J;
                    Funcion_k.Text = Inciso_K;
                    Funcion_l.Text = Inciso_L;
                    Funcion_m.Text = Inciso_M;
                    Funcion_n.Text = Inciso_N;
                    
                }
                //else
                //{
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Error!', 'No se han encontrado registros para el proceso y puesto selecccionado!', 'error')", true);
                //    Limpiar_Texbox();
                //}
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "swal('Error!', 'No se han encontrado registros para el proceso y puesto seleccionado!', 'error')", true);

                    // Bloquear los TextBox
                    Funcion_a.Enabled = false;
                    Funcion_a.BorderStyle = BorderStyle.Solid;
                    Funcion_a.BorderColor = System.Drawing.Color.Black;

                    Funcion_b.Enabled = false;
                    Funcion_b.BorderStyle = BorderStyle.Solid;
                    Funcion_b.BorderColor = System.Drawing.Color.Black;

                    // Repite esto para los demás TextBox...

                    Limpiar_Texbox();
                }
                    FuncionReader.Close();
                //con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        //Validar los textbox ya autorizados
        private void ValidaAutorizados(string idProceso, string idPuesto)
        {
            Dictionary<string, TextBox> campoTextBoxMap = new Dictionary<string, TextBox>
            {
                { "Autoriza_A", Funcion_a },
                { "Autoriza_B", Funcion_b },
                { "Autoriza_C", Funcion_c },
                { "Autoriza_D", Funcion_d },
                { "Autoriza_E", Funcion_e },
                { "Autoriza_F", Funcion_f },
                { "Autoriza_G", Funcion_g },
                { "Autoriza_H", Funcion_h },
                { "Autoriza_I", Funcion_i },
                { "Autoriza_J", Funcion_j },
                { "Autoriza_K", Funcion_k },
                { "Autoriza_L", Funcion_l },
                { "Autoriza_M", Funcion_m },
                { "Autoriza_N", Funcion_n }
            };
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                foreach (var campoTextBoxPair in campoTextBoxMap)
                {
                    string campo = campoTextBoxPair.Key;
                    TextBox textBox = campoTextBoxPair.Value;
                    SqlCommand cmd = new SqlCommand($"SELECT {campo} FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto", con);
                    cmd.Parameters.AddWithValue("@IdProceso", idProceso);
                    cmd.Parameters.AddWithValue("@IdPuesto", idPuesto);

                    object valorCampoObj = cmd.ExecuteScalar();
                    bool autorizaCampo = valorCampoObj != null && valorCampoObj != DBNull.Value && Convert.ToBoolean(valorCampoObj);

                    if (autorizaCampo)
                    {
                        textBox.Enabled = false;
                        textBox.BorderStyle = BorderStyle.Solid;
                        textBox.BorderColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        textBox.Enabled = true;
                        textBox.BorderStyle = BorderStyle.Solid;
                        textBox.BorderColor = System.Drawing.Color.Black;
                    }
                }
                con.Close();
            }
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
        protected void Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AG_RCH00401", con);
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
                       "swal('Guardado con Exito!', 'Se han modificado las funciones correctamente!', 'success')", true);
                //Limpiar_Texbox();
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
    }
}