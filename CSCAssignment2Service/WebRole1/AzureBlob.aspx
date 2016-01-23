<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AzureBlob.aspx.cs" Inherits="WebRole1.AzureBlob" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="display" runat="server"></div>
<asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <p>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </p>
    </form>
    
</body>
</html>
