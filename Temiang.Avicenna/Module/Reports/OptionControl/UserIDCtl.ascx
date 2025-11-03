<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserIDCtl.ascx.cs" 
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.UserIDCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="User ID" />
        </td>
        <td>
            <telerik:RadTextBox runat="server" ID="txtUserID" Width="100px" ReadOnly="true">
            </telerik:RadTextBox>
        </td>
    </tr>
</table>
