<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemTypeItemProductCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ItemTypeItemProductCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Item Type" />
        </td>
        <td>
             <telerik:RadComboBox ID="cboItemType" runat="server" Width="100%" AutoPostBack="True"
                OnSelectedIndexChanged="cboItemType_SelectedIndexChanged">
             </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblItem" runat="server" Text="Item" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboItemProduct" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboItemProduct_ItemDataBound"
                OnItemsRequested="cboItemProduct_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemID")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
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
