<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationTypeCtl2.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.RegistrationTypeCtl2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Registration Type" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboRegistrationType" Width="100%" runat="server" HighlightTemplatedItems="true">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="All" Value="" />
                    <telerik:RadComboBoxItem runat="server" Text="Inpatient" Value="IPR" />
                    <telerik:RadComboBoxItem runat="server" Text="Outpatient" Value="OP" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
