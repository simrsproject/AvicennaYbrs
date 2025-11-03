<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OKPatientTypeCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.OKPatientTypeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Patient Type" />
        </td>
        <td>
             <telerik:RadComboBox ID="cboOKPatientType" runat="server" Width="100%">
             </telerik:RadComboBox>
        </td>
    </tr>
</table>
