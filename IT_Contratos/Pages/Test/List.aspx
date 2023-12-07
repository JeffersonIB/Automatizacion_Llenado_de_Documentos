<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="RRHH5.Pages.Test.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <%--<link href="<%= ResolveClientUrl("~/CSS/Default.css") %>" rel="stylesheet" />--%>
        <%--<script src="<%= ResolveClientUrl("~/JS/Forms.js") %>"> </script>--%>
        <script type="text/javascript">
            var color = 'White';

            function changeColor(obj) {
                var rowObject = getParentRow(obj);
                var parentTable = document.getElementById("<%=CheckBoxListEmpleados.ClientID%>");
                if (color === '') {
                    color = getRowColor();
                }
                if (obj.checked) {
                    rowObject.style.backgroundColor = '#A3B1D8';
                } else {
                    rowObject.style.backgroundColor = color;
                    color = 'White';
                }
            }

            // Este método devuelve la fila padre del objeto
            function getParentRow(obj) {
                do {
                    obj = obj.parentElement;
                } while (obj.tagName !== "TR");
                return obj;
            }

            function TurnCheckBoixGridView(id) {
                var checkBoxList = document.getElementById("<%= CheckBoxListEmpleados.ClientID %>");
                var checkboxes = checkBoxList.getElementsByTagName("input");

                for (var i = 0; i < checkboxes.length; i++) {
                    if (checkboxes[i].type === "checkbox") {
                        checkboxes[i].checked = document.getElementById(id).checked;
                        changeColor(checkboxes[i]);
                    }
                }
            }

            function SelectAll(id) {
                var parentTable = document.getElementById("<%=CheckBoxListEmpleados.ClientID%>");
                var color = document.getElementById(id).checked ? '#A3B1D8' : 'White';

                for (var i = 0; i < parentTable.rows.length; i++) {
                    var checkbox = parentTable.rows[i].getElementsByTagName("input")[0];
                    checkbox.checked = document.getElementById(id).checked;
                    changeColor(checkbox);
                }

                TurnCheckBoixGridView(id);
            }
        </script>

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
                    <h5 class="modal-title">Agregar actividades V2
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddEmpresas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddEmpresas_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="scroll_checkboxes" cssclass="form-control">
                                            <input type="text" id="txtSearch" oninput="filterEmployees()" placeholder="Buscar por nombre" />
                                            <asp:CheckBoxList ID="CheckBoxListEmpleados" runat="server" CssClass="FormText" DataTextField="Nom_Ape" DataValueField="Id_Empleado"></asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>

                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </body>
    </html>

</asp:Content>
