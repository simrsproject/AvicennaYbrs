<%@ Control Language="C#" AutoEventWireup="true" Codebehind="CashierNameCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.CashierNameCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Cashier" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboEntry" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboEntry_ItemDataBound"
                OnItemsRequested="cboEntry_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "UserName")%> </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
