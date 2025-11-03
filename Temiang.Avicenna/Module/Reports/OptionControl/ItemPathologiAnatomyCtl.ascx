<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemPathologiAnatomyCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ItemPathologiAnatomyCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Item Pathologi Anatomy" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboItemPathologiAnatomy" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboItemPathologiAnatomy_ItemDataBound"
                OnItemsRequested="cboItemPathologiAnatomy_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>