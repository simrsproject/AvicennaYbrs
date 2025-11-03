<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemTypeCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ItemTypeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Item Type" />
        </td>
        <td>
             <telerik:RadComboBox ID="cboItemType" runat="server" Width="100%">
             </telerik:RadComboBox>
        </td>
    </tr>
</table>
