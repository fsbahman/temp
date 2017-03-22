<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test1.aspx.cs" Inherits="test_aspx.test1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lbl1" runat="server" Text="First Number:" />
        <asp:TextBox ID="txt1" runat="server" />
        <asp:Label ID="lbl2" runat="server" Text="Second Number:" />
        <asp:TextBox ID="txt2" runat="server" />
        <asp:Label ID="lbl3" runat="server" Text="Result:" />
        <asp:TextBox ID="txt3" runat="server" ReadOnly="true"/>
        <asp:Button ID="btn1" runat="server" Text="Plus" OnClick="btn1_Click" />
    </div>
    </form>
</body>
</html>
