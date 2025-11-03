<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StdSurveilansPenyakitCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.StdSurveilansPenyakitCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Nama Penyakit" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboSurveilansPenyakit" Width="100%" runat="server">
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
