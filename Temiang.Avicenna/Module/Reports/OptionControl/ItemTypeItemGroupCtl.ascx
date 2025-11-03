<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemTypeItemGroupCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ItemTypeItemGroupCtl" %>
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
            <asp:Label ID="lblItemGroup" runat="server" Text="Item Group" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboItemGroup" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboItemGroup_ItemDataBound"
                OnItemsRequested="cboItemGroup_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupID")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
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