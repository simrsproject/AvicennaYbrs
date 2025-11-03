<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAResultTypeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.PAResultTypeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Type" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboResultType" Width="100%" runat="server" HighlightTemplatedItems="true">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="All" Value="" />
                    <telerik:RadComboBoxItem runat="server" Text="Cytology" Value="01" />
                    <telerik:RadComboBoxItem runat="server" Text="Histology" Value="02" />
                    <telerik:RadComboBoxItem runat="server" Text="Pap Smear" Value="03" />
                    <telerik:RadComboBoxItem runat="server" Text="Immunohistochemistry" Value="04" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>