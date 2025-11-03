<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessProgress.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PCare.ProcessProgress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
            <telerik:RadProgressArea ID="progressArea" runat="server" Width="500px" />
        </div>
    </form>
</body>
</html>
