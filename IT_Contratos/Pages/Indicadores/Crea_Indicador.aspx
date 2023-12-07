<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Crea_Indicador.aspx.cs" Inherits="RRHH5.Pages.Indicadores.Crea_Indicadore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="<%= ResolveClientUrl("~/CSS/Indi.css") %>" rel="stylesheet" />
        <script src="<%= ResolveClientUrl("~/JS/Indi.js") %>"> </script>
    </head>
    <body>
        <div class="container box">
            <center>
                <h1 class="title">Crear indicadores 
                </h1>
                <br />
                <br />
            </center>
        </div>
        <!-- Modal Agregar -->
        <div class="modal fade" id="Modal_Agregar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Agregar nuevo indicador
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <table align="center">
                            <tr>
                                <td align="center">Objetivo 
                                </td>
                                <td align="center">No SGC
                                </td>
                                <td align="center">No 
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div class="control">
                                        <div class="select">
                                            <asp:DropDownList ID="ddlObjetivo" runat="server" Width="100px" AutoPosBack="true" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </td>
                                <td align="center">
                                    <div class="control">
                                        <div class="select">
                                            <asp:DropDownList ID="ddlSGC" runat="server" Width="100px" AutoPosBack="true" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtNo" runat="server" type="number" CssClass="form-control" Width="100px"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">Indicador 
                                        <br />
                                    <asp:TextBox ID="txtIndicador" runat="server" CssClass="form-control" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">Meta Proceso 
                                </td>
                                <td align="center">Peso Proceso
                                </td>
                                <td align="center">Meta Objetivo
                                </td>
                                <td align="center">Peso Objetivo
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtMeta_Proceso" runat="server" type="number" CssClass="form-control" min="0" step="0.01" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtMeta_Proceso"
                                        ErrorMessage="Campo obligatorio"
                                        ForeColor="Red"
                                        ValidationGroup="Validate">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtPeso_Proceso" runat="server" type="number" CssClass="form-control" min="0" step="0.01" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtPeso_Proceso"
                                        ErrorMessage="Campo obligatorio"
                                        ForeColor="Red"
                                        ValidationGroup="Validate">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtMeta_Objetivo" runat="server" type="number" CssClass="form-control" min="0" step="0.01" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtMeta_Objetivo"
                                        ErrorMessage="Campo obligatorio"
                                        ForeColor="Red"
                                        ValidationGroup="Validate">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtPeso_Objetivo" runat="server" type="number" CssClass="form-control" min="0" step="0.01" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="txtPeso_Objetivo"
                                        ErrorMessage="Campo obligatorio"
                                        ForeColor="Red"
                                        ValidationGroup="Validate">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">Elige el tipo de calificación que tendrá
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlTC" runat="server" Width="100px" AutoPosBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <br />
                                    <asp:Button ID="btnAgregar" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="Validate" OnClick="Agregar_Click" />
                                    <asp:Button ID="btnCancelar" runat="server" class="btn btn-danger" Text="Cancelar" OnClientClick="CloseModalAg();return false;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Actualizar -->
        <div class="modal fade" id="Modal_Actualizar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Actualizar datos de Indicador
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <table align="center">
                            <tr>
                                <tb>
                                    <asp:Label ID="lbId_Empresa" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lbId_Gerencia" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lbId_Proceso" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lbId_Indicador" runat="server" Visible="false"></asp:Label>
                                </tb>
                            </tr>
                            <tr>
                                <td align="center">Objetivo 
                                </td>
                                <td align="center">No SGC
                                </td>
                                <td align="center">No 
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div class="control">
                                        <div class="select">
                                            <asp:DropDownList ID="ddObjetivo" runat="server" Width="100px" AutoPosBack="true" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </td>
                                <td align="center">
                                    <div class="control">
                                        <div class="select">
                                            <asp:DropDownList ID="ddSGC" runat="server" Width="100px" AutoPosBack="true" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txNo" runat="server" type="number" CssClass="form-control" Width="100px"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">Indicador 
                                        <br />
                                    <asp:TextBox ID="txIndicador" runat="server" CssClass="form-control" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">Meta Proceso 
                                </td>
                                <td align="center">Peso Proceso
                                </td>
                                <td align="center">Meta Objetivo
                                </td>
                                <td align="center">Peso Objetivo
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txMeta_Proceso" runat="server" type="number" CssClass="form-control" min="0" step="0.01" Width="100px"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txPeso_Proceso" runat="server" type="number" CssClass="form-control" min="0" step="0.01" Width="100px"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txMeta_Objetivo" runat="server" type="number" CssClass="form-control" min="0" step="0.01" Width="100px"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txPeso_Objetivo" runat="server" type="number" CssClass="form-control" min="0" step="0.01" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">Elige el tipo de calificación que tendrá
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddTC" runat="server" Width="100px" AutoPosBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <br />
                                    <asp:Button ID="btActualizar" runat="server" class="btn btn-primary" Text="Actualizar" OnClick="Actualizar_Click" />
                                    <asp:Button ID="btCancelar" runat="server" class="btn btn-danger" Text="Cancelar" OnClientClick="CloseModalAc();return false;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Eliminar -->
        <div class="modal fade" id="Modal_Eliminar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">
                            <strong>Advertencia!
                            </strong>
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <table align="center">
                            <tr>
                                <td colspan="2">Esta seguró de eliminar : 
                                        <asp:Label ID="lblIndicaddor" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>No
                                </td>
                                <td>Indicador
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbNo" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbIndicador" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <br />
                                    <asp:Button ID="bEliminar" runat="server" class="btn btn-primary" Text="Eliminar" OnClick="Eliminar_Click" />
                                    <asp:Button ID="bCancelar" runat="server" class="btn btn-danger" Text="Cancelar" OnClientClick="CloseModalEl();return false;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <!--  Tabla de Indicadores de Finca -->
        <center>
            <table>
                <tr>
                    <td>
                        <div class="row">
                            <div class="row">
                                <h3 class="title">
                                    <asp:Label ID="Responsable" runat="server" Text='<%#Eval("Responsable") %>'></asp:Label>
                                </h3>
                            </div>
                            <div class="ml-auto">
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="DDLProceso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLProceso_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="ml-auto">
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Nuevo Indicador" OnClientClick="ShowModalAg();return false;" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvIndicadores" runat="server"
                            DataKeyNames="Id_Indicador"
                            PageSize="17"
                            GridLines="both"
                            AllowPaging="true"
                            HorizontalAlign="Center"
                            ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False"
                            EmptyDataText="No Records Found"
                            EmptyDataRowStyle-ForeColor="Red"
                            PagerStyle-CssClass="pagination-ys"
                            OnRowCommand="gvIndicadores_OnRowCommand"
                            OnPageIndexChanging="gvIndicadores_PageIndexChanging">

                            <Columns>
                                <asp:TemplateField HeaderText="Id_Empresa" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Empresa" runat="server" Text='<%#Eval("Id_Empresa") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empresa" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvEmpresa" runat="server" Text='<%#Eval("Empresa") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Gerencia" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Gerencia" runat="server" Text='<%#Eval("Id_Gerencia") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Proces" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Proceso" runat="server" Text='<%#Eval("Id_Proceso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proces" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvProceso" runat="server" Text='<%#Eval("Proceso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Objetivo" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Objetivo" runat="server" Text='<%#Eval("Id_Objetivo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_No_SGC" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_No_SGC" runat="server" Text='<%#Eval("Id_No_SGC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Indicador" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Indicador" runat="server" Text='<%#Eval("Id_Indicador") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvNo_Indicador" runat="server" Text='<%#Eval("No_Indicador") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Indicador" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvIndicador" runat="server" Text='<%#Eval("Indicador") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Meta Proceso" Visible="true">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="gvMeta_Proceso" runat="server" Text='<%# Eval("Tipo_Califica").ToString() == "1" ? Eval("Meta_Proceso") + "%" : Eval("Meta_Proceso") %>'></asp:Label>--%>
                                        <asp:Label ID="gvMeta_Proceso" runat="server" Text='<%# Eval("Tipo_Califica").ToString() == "1" ? String.Format("{0:N}", Eval("Meta_Proceso")) + "%" : String.Format("{0:N}", Eval("Meta_Proceso")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Peso Proceso" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="gvPeso_Proceso" runat="server" Text='<%#Eval("Peso_Proceso") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Meta_Objetivo" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvMeta_Objetivo" runat="server" Text='<%#Eval("Meta_Objetivo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Peso_Objetivo" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvPeso_Objetivo" runat="server" Text='<%#Eval("Peso_Objetivo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsActive" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvIsActive" runat="server" Text='<%#Eval("IsActive")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Se_Califica" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvSe_Califica" runat="server" Text='<%#Eval("Se_Califica") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo_Califica" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvTipo_Califica" runat="server" Text='<%#Eval("Tipo_Califica") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Rol" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvId_Rol" runat="server" Text='<%#Eval("Id_Rol")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Pages/Images/Editar.png" CommandName="ShowModalAc" CommandArgument='<%#Eval("Id_Indicador") %>' Style="display: block; margin: 0 auto;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Pages/Images/Eliminar.png" CommandName="ShowModalEl" CommandArgument='<%#Eval("Id_Indicador") %>' Style="display: block; margin: 0 auto;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </center>
    </body>
    </html>
</asp:Content>
