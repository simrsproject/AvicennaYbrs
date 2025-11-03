<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitRoomCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ServiceUnitRoomCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Service Unit" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboServiceUnitID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                OnItemsRequested="cboServiceUnitID_ItemsRequested" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged"
                AutoPostBack="True">
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
            <telerik:RadComboBox ID="cboRoomID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboRoomID_ItemDataBound" OnItemsRequested="cboRoomID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
