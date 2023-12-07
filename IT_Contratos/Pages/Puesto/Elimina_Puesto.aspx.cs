using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RRHH5.Pages.Puesto
{
    public partial class Elimina_Puesto : System.Web.UI.Page
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
                    string idProceso = DDLProceso.SelectedValue;
                    string idPuesto = DDLPuesto.SelectedValue;
                    ValidaAutorizados(idProceso, idPuesto);
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
        private bool AutorizacionRealizada_a()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_A=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_b()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_B=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_c()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_C=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_d()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_D=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_e()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_E=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_f()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_F=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_g()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_G=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_h()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_H=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_i()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_I=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_j()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_J=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_k()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_K=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_l()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_L=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_m()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_M=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        private bool AutorizacionRealizada_n()
        {
            bool resultado = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VW_RCH00401 WHERE IdProceso=@IdProceso AND IdPuesto=@IdPuesto AND Autoriza_N=1", con);
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        protected void btnEliminar_a_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_EL_RCH00401a", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Usuario", System.Data.SqlDbType.Int).Value = Session["Id_Usuario"].ToString();
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_b_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401b", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_c_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401c", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                //throw;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_d_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401d", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                //throw;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_e_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401e", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                //throw;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_f_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401f", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_g_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401g", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_h_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401h", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_i_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401i", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                //throw;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_j_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401j", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_k_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401k", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                //throw;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_l_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401l", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                //throw;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_m_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401m", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);

            }
            catch (Exception)
            {
                //throw;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void btnEliminar_n_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401n", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se ha autorizado correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                //throw;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
        protected void AutorizarTodo_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AC_RCH00401", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdProceso", System.Data.SqlDbType.VarChar).Value = DDLProceso.SelectedValue.ToString();
                cmd.Parameters.Add("@IdPuesto", System.Data.SqlDbType.VarChar).Value = DDLPuesto.SelectedValue.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                //  "swal('Éxito!', 'Se han autorizado todas las funciones correctamente!', 'success')", true);
                string idProceso = DDLProceso.SelectedValue;
                string idPuesto = DDLPuesto.SelectedValue;
                ValidaAutorizados(idProceso, idPuesto);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Error!', 'Error en validación de datos!', 'error')", true);
            }
        }
    }
}