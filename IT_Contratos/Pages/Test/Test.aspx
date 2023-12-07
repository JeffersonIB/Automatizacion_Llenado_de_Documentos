<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="RRHH5.Pages.Test.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <div class="scroll_checkboxes" cssclass="form-control">
        <asp:CheckBoxList ID="CheckBoxListEmpleados" runat="server" CssClass="FormText" DataTextField="Nom_Ape" DataValueField="Id_Empleado"></asp:CheckBoxList>
    </div>
    <br />
    <input type="button" value="Select All" onclick="SelectAll('selectAll')" />
</body>
</html>


</asp:Content>
