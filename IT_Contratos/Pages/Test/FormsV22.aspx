<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="FormsV22.aspx.cs" Inherits="RRHH5.Pages.Test.FormsV22" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
    Formuario V2
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <%--<link href="<%= ResolveClientUrl("~/CSS/Default.css") %>" rel="stylesheet" />--%>
        <script src="<%= ResolveClientUrl("~/JS/Forms.js") %>"> </script>
        <style type="text/css">
            .scroll_checkboxes {
                height: 120px;
                width: 100%;
                padding: 5px;
                overflow: auto;
                border: 1px solid #ccc;
                display: block;
                padding: .375rem .75rem;
                font-size: 1rem;
                line-height: 1.5;
                color: #495057;
                background-color: #fff;
                background-clip: padding-box;
                border: 1px solid #ced4da;
                border-radius: .25rem;
                transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out
            }

            .scroll_checkboxess {
                height: 120px;
                width: 200px;
                padding: 5px;
                overflow: auto;
                border: 1px solid #ccc;
            }

            .FormText {
                FONT-SIZE: 11px;
                FONT-FAMILY: tahoma,sans-serif
            }
        </style>
    </head>
    <body>
        <!-- Modal-->
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Agregar actividades V2
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <table align="center">
                        <tr>
                            <td>Lote
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="ddlLotes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotes_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="ddlLotes"
                                    ErrorMessage="Campo obligatorio"
                                    ForeColor="Red"
                                    ValidationGroup="Validate"
                                    InitialValue="0">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Proceso
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="ddlProcesos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProcesos_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="ddlProcesos"
                                    ErrorMessage="Campo obligatorio"
                                    ForeColor="Red"
                                    ValidationGroup="Validate"
                                    InitialValue="0">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Actividad 1
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="ddlActividad1" runat="server" AutoPosBack="true" OnSelectedIndexChanged="ddlActividad1_OnSelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="ddlActividad1"
                                    ErrorMessage="Campo obligatorio"
                                    ForeColor="Red"
                                    ValidationGroup="Validate"
                                    InitialValue="0">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    <a data-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample">Actividad 2 ↓ </a>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="collapse" id="collapseExample2">
                                    <div>
                                        Actividad 2
                                        <asp:DropDownList ID="ddlActividad2" runat="server" AutoPosBack="true" OnSelectedIndexChanged="ddlActividad2_OnSelectedIndexChanged" Style="width: 100%;" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    <a data-toggle="collapse" href="#collapseExample3" role="button" aria-expanded="false" aria-controls="collapseExample">Actividad 3 ↓
                                    </a>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="collapse" id="collapseExample3">
                                    <div>
                                        Actividad 3
                                    <asp:DropDownList ID="ddlActividad3" runat="server" AutoPosBack="true" OnSelectedIndexChanged="ddlActividad3_OnSelectedIndexChanged" Style="width: 100%;" CssClass="form-control">
                                    </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="scroll_checkboxes" cssclass="form-control">
                                    <asp:CheckBoxList ID="CheckBoxListEmpleados" runat="server" CssClass="FormText" DataTextField="Nom_Ape" DataValueField="Id_Empleado"></asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="ml-auto">
                                        <asp:Button ID="ButtonAgregarEmpleados" runat="server" class="btn btn-round btn-primary" Text="Agregar" OnClick="AgregarEmpleados_Click" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewCalificaciones" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="Id_Empleado" HeaderText="ID Proveedor" Visible="false" />
                                        <asp:BoundField DataField="Nom_Ape" HeaderText="Nombre Proveedor" />
                                        <asp:TemplateField HeaderText="Tipo Pago">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTipo_Actividad" runat="server" AutoPostBack="true" CssClass="form-control" Style="width: 100%;"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad1">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCantidad1" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                                                <asp:RequiredFieldValidator
                                                    ID="Validator4" runat="server"
                                                    ControlToValidate="txtCantidad1"
                                                    ErrorMessage=">0"
                                                    ForeColor="Red"
                                                    ValidationGroup="Validate"
                                                    Display="Dynamic"
                                                    InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad2" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCantidad2" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad3" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCantidad3" runat="server" type="number" CssClass="form-control" Text="0" min="0" step="0.01" placeholder="0.00" Style="width: 100%;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="ml-auto">
                                    <asp:Button ID="ButtonInsertar" runat="server" class="btn btn-round btn-success" Text="Insertar" ValidationGroup="Validate" OnClick="ButtonInsertar_Click" Visible="false" />
                                    <%--<asp:Button runat="server" ID="btnCancelar" class="btn btn-round btn-danger" Text="Cancelar" OnClientClick="CloseModalAg();return false;" />--%>
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
        <%--Modal--%>
        <center>
            <%--            <asp:Button Text="Regresar" runat="server" class="btn btn-round btn-success" OnClick="Regresar_Click" />
            <asp:Button Text="Salir" runat="server" class="btn btn-round btn-danger" OnClick="Salir_Click" />--%>
        </center>
    </body>
    </html>
</asp:Content>
