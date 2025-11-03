<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AccessFailed.aspx.cs" Inherits="Temiang.Avicenna.AccessFailed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="border-width: medium; border-color: Yellow; width: 100%;">
            <asp:Label ID="lblMessage" runat="server" Text="You don't have the authorization access for this page, please contact your administrator." />
            <asp:Button ID="btnBack" Width="70px" runat="server" Text="Back" OnClientClick="javascript:history.go(-1);return false;" />
        </div>
    </form>
</body>
</html>
