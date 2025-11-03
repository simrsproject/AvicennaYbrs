<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TypeAndStatusWorkOrderCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.TypeAndStatusWorkOrderCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Work Type" />
        </td>
        <td>
             <telerik:RadComboBox ID="cboSRWorkType" runat="server" Width="100%" >
             </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblWorkStatus" runat="server" Text="Work Status" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboSRWorkStatus" Width="100%" runat="server" >
            </telerik:RadComboBox>
        </td>
    </tr>
</table>