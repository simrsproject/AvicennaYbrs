<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetGroupCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AssetGroupCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Asset Group" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboAssetGroupID" runat="server" Width="100%" HighlightTemplatedItems="True"
                AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboAssetGroupID_ItemDataBound"
                OnItemsRequested="cboAssetGroupID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "GroupName")%>
                    </b>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 result
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>