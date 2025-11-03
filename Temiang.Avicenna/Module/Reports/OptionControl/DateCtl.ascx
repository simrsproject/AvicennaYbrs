<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DateCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Date" />
        </td>
        <td>
            <telerik:RadDatePicker runat="server" ID="txtDate" Width="100px">
            </telerik:RadDatePicker>
        </td>
    </tr>
</table>
