<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Crea_Puesto.aspx.cs" Inherits="RRHH5.Pages.Puesto.Crea_Puesto" %>

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
        </script>
        <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
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
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content modal-lg">
                <div class="modal-header">
                    <h5 class="modal-title">Agregar funciones
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body modal-lg" >
                    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                            <table align="center" Style="width: 100%;">
                                <%--<tr>
                                    <td>
                                        <asp:TextBox runat="server" id="id1"/>
                                    </td>
                                </tr>
                                     <tr>
                                    <td>
                                        <asp:TextBox runat="server" id="id2"/>
                                    </td>
                                </tr>
                                     <tr>
                                    <td>
                                        <asp:TextBox runat="server" id="id3"/>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <div class="col-md-2 ml-auto">
                                    <td align="center">Proceso :
                                    </td>
                                    </div>
                                    <td colspan="3">
                                        <div class="control">
                                            <div class="select">
                                                <asp:DropDownList ID="DDLProceso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLProceso_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">Puesto :
                                    </td>
                                    <td colspan="3">
                                        <div class="control">
                                            <div class="select">
                                                <asp:DropDownList ID="DDLPuesto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLPuesto_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <h3>
                                            <colspan>Funciones</colspan>
                                        </h3>
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">a) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="Funcion_a" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">b) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="Funcion_b" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">c) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="Funcion_c" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">d) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="Funcion_d" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">e) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="Funcion_e" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="center">f) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="Funcion_f" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">g) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="Funcion_g" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <p>
                                            <a data-toggle="collapse" href="#collapse" role="button" aria-expanded="false" aria-controls="collapse_1">Agregar inciso h ↓
                                            </a>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div class="collapse" id="collapse">
                                            h) :
                                        </div>
                                    </td>
                                    <td>
                                        <div class="collapse" id="collapse">
                                            <asp:TextBox runat="server" ID="Funcion_h" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <p>
                                            <a data-toggle="collapse" href="#collapse1" role="button" aria-expanded="false" aria-controls="collapse_1">Agregar inciso i ↓
                                            </a>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div class="collapse" id="collapse1">
                                            i) :
                                        </div>
                                    </td>
                                    <td>
                                        <div class="collapse" id="collapse1">
                                            <asp:TextBox runat="server" ID="Funcion_i" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <p>
                                            <a data-toggle="collapse" href="#collapse2" role="button" aria-expanded="false" aria-controls="collapse_1">Agregar inciso j ↓
                                            </a>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div class="collapse" id="collapse2">
                                            j) :
                                        </div>
                                    </td>
                                    <td>
                                        <div class="collapse" id="collapse2">
                                            <asp:TextBox runat="server" ID="Funcion_j" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <p>
                                            <a data-toggle="collapse" href="#collapse3" role="button" aria-expanded="false" aria-controls="collapse_1">Agregar inciso k ↓
                                            </a>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div class="collapse" id="collapse3">
                                            k) :
                                        </div>
                                    </td>
                                    <td>
                                        <div class="collapse" id="collapse3">
                                            <asp:TextBox runat="server" ID="Funcion_k" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <p>
                                            <a data-toggle="collapse" href="#collapse4" role="button" aria-expanded="false" aria-controls="collapse_1">Agregar inciso l ↓
                                            </a>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div class="collapse" id="collapse4">
                                            l) :
                                        </div>
                                    </td>
                                    <td>
                                        <div class="collapse" id="collapse4">
                                            <asp:TextBox runat="server" ID="Funcion_l" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <p>
                                            <a data-toggle="collapse" href="#collapse5" role="button" aria-expanded="false" aria-controls="collapseExample">Agregar inciso m ↓
                                            </a>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div class="collapse" id="collapse5">
                                            m) :
                                        </div>
                                    </td>
                                    <td>
                                        <div class="collapse" id="collapse5">
                                            <asp:TextBox runat="server" ID="Funcion_m" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <p>
                                            <a data-toggle="collapse" href="#collapse6" role="button" aria-expanded="false" aria-controls="collapseExample">Agregar inciso n ↓
                                            </a>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div class="collapse" id="collapse6">
                                            n) :
                                        </div>
                                    </td>
                                    <td>
                                        <div class="collapse" id="collapse6">
                                            <asp:TextBox runat="server" ID="Funcion_n" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                       <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button runat="server" ID="Agregar" class="btn btn-round btn-primary" OnClick="Agregar_Click" Text="Agregar" />
                            <asp:Button runat="server" ID="Cancelar" class="btn btn-round btn-danger" Text="Cancelar" />
                        </td>
                    </tr>
                    </table>
                        
                </div>
            </div>
        </div>
    </body>
    </html>
</asp:Content>
