<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="WORD1.aspx.cs" Inherits="RRHH5.Pages.Test.WORD1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Generador de Documentos Word</title>
    </head>
    <body>
        <div>
            <h1>Generador de Documentos Word</h1>
            <div>
                <label for="ddlParagraph">Selecciona un párrafo:</label>
                <asp:DropDownList ID="ddlParagraph" runat="server">
                    <asp:ListItem Text="Párrafo 1" Value="Parrafo1"></asp:ListItem>
                    <asp:ListItem Text="Párrafo 2" Value="Parrafo2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                <label for="txtCustomText">Ingresa texto personalizado:</label>
                <asp:TextBox ID="txtCustomText" runat="server"></asp:TextBox>
            </div>
        </div>
        <div>
            <label for="txtFileName">Nombre del archivo:</label>
            <asp:TextBox ID="txtFileName" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnGenerate" runat="server" Text="Generar Documento Word" OnClick="btnGenerate_Click" />
        </div>

    </body>
    </html>

</asp:Content>
