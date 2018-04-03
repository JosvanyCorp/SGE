<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SGE_Presentacion.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <link href="Content/login.css" rel="stylesheet" />
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <div class="contenedor">
        <section>
            <article class="col-lg-6 col-md-6 col-sm-6 col-xs-12">

                <h1 class="pos">SEG</h1>

            </article>
            <article class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <form class="jumbotron">
                    <input type="text" class="form-control" placeholder="Nombre" />

                    <input type="email" class="form-control" placeholder="Email" />

                    <input type="button" class="btn btn-default" value="Enviar" />
                </form>
            </article>
        </section>
    </div>
</body>
</html>
