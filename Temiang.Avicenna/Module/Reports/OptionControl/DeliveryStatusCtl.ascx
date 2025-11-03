<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeliveryStatusCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DeliveryStatusCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Delivery Status" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboPendingDelivery" Width="100%" runat="server" HighlightTemplatedItems="true">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="All" Value="" />
                    <telerik:RadComboBoxItem runat="server" Text="Pending Delivery" Value="0" />
                    <telerik:RadComboBoxItem runat="server" Text="Delivered" Value="1" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
