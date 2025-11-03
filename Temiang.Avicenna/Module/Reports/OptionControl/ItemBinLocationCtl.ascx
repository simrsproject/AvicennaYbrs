<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemBinLocationCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ItemBinLocationCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblLocation" runat="server" Text="Location" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboLocationID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemsRequested="cboLocationID_ItemsRequested" OnItemDataBound="cboLocationID_ItemDataBound" OnSelectedIndexChanged="cboLocationID_SelectedIndexChanged" AutoPostBack="True">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "LocationName")%></b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblItemBin" runat="server" Text="Item Bin" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboItemBin" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true">
            </telerik:RadComboBox>
        </td>
    </tr>
</table>