<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncidentTypeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.IncidentTypeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Incident Type" />
        </td>
        <td>
             <telerik:RadComboBox ID="cboSRIncidentType" runat="server" Width="100%" AutoPostBack="True"
                OnSelectedIndexChanged="cboSRIncidentType_SelectedIndexChanged">
             </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblComponentID" runat="server" Text="Component" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboComponentID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboComponentID_ItemDataBound"
                OnItemsRequested="cboComponentID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ComponentID")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "ComponentName")%>
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