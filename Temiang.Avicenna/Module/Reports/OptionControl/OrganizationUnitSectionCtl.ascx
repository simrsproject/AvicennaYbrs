<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationUnitSectionCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.OrganizationUnitSectionCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Organization Unit" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboOrganizationUnitID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound"
                OnItemsRequested="cboOrganizationUnitID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitName")%> </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>