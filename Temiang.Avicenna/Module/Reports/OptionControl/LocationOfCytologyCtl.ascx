<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationOfCytologyCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.LocationOfCytologyCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Location" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboLocationID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboLocationID_ItemDataBound"
                OnItemsRequested="cboLocationID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%> </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>