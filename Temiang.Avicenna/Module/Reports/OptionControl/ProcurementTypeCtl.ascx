<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProcurementTypeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ProcurementTypeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Procurement Type" />
        </td>
        <td>
             <telerik:RadComboBox ID="cboProcurementType" runat="server" Width="100%">
             </telerik:RadComboBox>
        </td>
    </tr>
</table>