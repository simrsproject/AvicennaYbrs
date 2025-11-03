<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiagnosaNeonatalCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DiagnosaNeonatalCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Item Medical" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboDiagnosaNeonatal" Width="100%" runat="server">
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
