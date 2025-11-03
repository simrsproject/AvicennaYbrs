<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationConsignmentCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.LocationConsignmentCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblName" runat="server" Text="Location" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboLocationName" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboLocationName_ItemDataBound"
                OnItemsRequested="cboLocationName_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "LocationName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "LocationID")%>) </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
