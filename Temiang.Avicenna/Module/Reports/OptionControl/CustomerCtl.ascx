<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.CustomerCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Customer" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboCustomerID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboCustomerID_ItemDataBound"
                OnItemsRequested="cboCustomer_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "CustomerName")%>
                    </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
