<%@ Page Language="C#" CodeBehind="Index.aspx.cs" Inherits="EService.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Banco EFood</title>
</head>
<body>
<form id="form" runat="server" method="post">
    <div>
        <h1>Banco EFood</h1>
        <asp:Button ID="Btn_Tarjeta" runat="server" Text="Tarjetas" OnClick="Btn_Tarjeta_Click"/>
        <asp:Button ID="Btn_Cheque" runat="server" Text="Cheques" OnClick="Btn_Cheque_Click"/>
        <br/>
    </div>
</form>
</body>
</html>