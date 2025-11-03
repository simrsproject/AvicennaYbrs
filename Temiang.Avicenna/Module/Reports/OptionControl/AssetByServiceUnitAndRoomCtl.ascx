<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetByServiceUnitAndRoomCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AssetByServiceUnitAndRoomCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table width="90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Service Unit" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboServiceUnitID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                OnItemsRequested="cboServiceUnitID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                    </b>
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
            <asp:Label ID="lblRoom" runat="server" Text="Room" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboRoomID" runat="server" Width="100%" AutoPostBack="True"
                OnSelectedIndexChanged="cboRoomID_SelectedIndexChanged">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblAsset" runat="server" Text="Asset" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboAssetID" runat="server" Width="100%" HighlightTemplatedItems="True"
                AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboAssetID_ItemDataBound"
                OnItemsRequested="cboAssetID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "AssetName")%>
                    </b>
                    <br />
                    Serial No :
                    <%# DataBinder.Eval(Container.DataItem, "SerialNumber")%>
                    <br />
                    Location :&nbsp;<%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                    <br />
                    Unit Maintenance :&nbsp;<%# DataBinder.Eval(Container.DataItem, "MaintenanceServiceUnitName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 result
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
