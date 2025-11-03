<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemGroupCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ItemGroupCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Item Group" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboItemGroupID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboItemGroupID_ItemDataBound"
                OnItemsRequested="cboItemGroupID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%> </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
