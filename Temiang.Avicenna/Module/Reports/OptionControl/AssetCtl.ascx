<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AssetCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Asset" />
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
