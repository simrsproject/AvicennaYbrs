<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentMethodCtl.ascx.cs" 
Inherits="Temiang.Avicenna.Module.Reports.OptionControl.PaymentMethodCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Payment Method" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboPaymentMethod" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboPaymentMethod_ItemDataBound"
                OnItemsRequested="cboPaymentMethod_ItemsRequested">
                <ItemTemplate>
                    <b>
                       <%# DataBinder.Eval(Container.DataItem, "PaymentTypeName")%> <%# DataBinder.Eval(Container.DataItem, "PaymentMethodName")%> </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>