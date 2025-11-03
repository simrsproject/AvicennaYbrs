<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ServiceRoomCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ServiceRoomCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Room" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboServiceRoomID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboServiceRoomID_ItemDataBound"
                OnItemsRequested="cboServiceRoomID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "RoomName")%> </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
