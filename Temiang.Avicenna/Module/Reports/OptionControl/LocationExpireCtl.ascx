<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationExpireCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.LocationExpireCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblName" runat="server" Text="Location" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboLocationName" Width="100%" runat="server" >
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
