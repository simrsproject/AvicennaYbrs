<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetNameCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AssetNameCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Asset Name" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboAssetName" runat="server" Width="100%" HighlightTemplatedItems="True"
                AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboAssetName_ItemDataBound"
                OnItemsRequested="cboAssetName_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "AssetName")%>
                    </b>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 result
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>