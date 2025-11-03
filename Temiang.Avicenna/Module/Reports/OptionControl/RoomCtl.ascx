<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RoomCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.RoomCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblOutPatientRoomID" runat="server" Text="OutPatient Room" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboOutPatientRoomID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboOutPatientRoomID_ItemDataBound"
                OnItemsRequested="cboOutPatientRoomID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
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
