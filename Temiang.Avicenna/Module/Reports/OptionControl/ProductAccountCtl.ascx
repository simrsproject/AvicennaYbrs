<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductAccountCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ProductAccountCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Product Account" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboProductAccountID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboProductAccountID_ItemDataBound"
                OnItemsRequested="cboProductAccountID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ProductAccountName")%> </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
