<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="WORD2.aspx.cs" Inherits="RRHH5.Pages.Test.WORD2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Exportar a Word</title>
        <script type="text/javascript">
            var empleados = [];

            function filterEmployees() {
                var searchText = document.getElementById('txtSearch').value.toLowerCase();
                var checkboxes = document.getElementById('<%= CheckBoxListEmpleados.ClientID %>').getElementsByTagName('input');

                for (var i = 0; i < checkboxes.length; i++) {
                    var checkbox = checkboxes[i];
                    var employeeName = checkbox.parentNode.innerText.toLowerCase();
                    if (employeeName.indexOf(searchText) > -1) {
                        checkbox.parentNode.style.display = 'block';
                    } else {
                        checkbox.parentNode.style.display = 'none';
                    }
                }
            }
        </script>
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
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Generar contrato
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table align="center">
                        <tr>
                            <td>
                                <br />
                                    Representante legal:
                                <br />
                            </td>
                            <td>
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="DDLRepresentante" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLRepresentante_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                    Empleado :
                                <br />
                            </td>
                            <td>
                                <div class="scroll_checkboxes" cssclass="form-control">
                                    <input type="text" id="txtSearch" oninput="filterEmployees()" placeholder="Buscar por nombre" />
                                    <asp:CheckBoxList ID="CheckBoxListEmpleados" runat="server" CssClass="FormText" DataTextField="Nom_Ape" DataValueField="Id_Empleado"></asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                    Puesto:
                                <br />
                            </td>
                            <td>
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="DDLPuesto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLPuesto_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                            <br />
                            </td>
                        </tr>
                        <tr>
                            <td>Nombre de documento :
                            <br />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NombreDocumento" CssClass="form-control" MaxLength="200" Style="width: auto;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:Button ID="btnWord" runat="server" class="btn btn-round btn-success" Text="Generar documento Word" OnClick="btnWord_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
    </body>
    </html>

</asp:Content>
