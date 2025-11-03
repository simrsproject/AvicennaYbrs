<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BankCtl.ascx.cs" 
Inherits="Temiang.Avicenna.Module.Reports.OptionControl.BankCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblBankAccount" runat="server" Text="Bank" />
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlBankAccount" Width="100%" />
        </td>
    </tr>
</table>