<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationTypeServiceUnitCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.RegistrationTypeServiceUnitCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px" />
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Registration Type" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboRegistrationType" Width="100%" runat="server" HighlightTemplatedItems="true"
                AutoPostBack="True" OnSelectedIndexChanged="cboRegistrationType_SelectedIndexChanged">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="All" Value="" />
                    <telerik:RadComboBoxItem runat="server" Text="Inpatient" Value="IPR" />
                    <telerik:RadComboBoxItem runat="server" Text="Outpatient" Value="OP" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboServiceUnitID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                OnItemsRequested="cboServiceUnitID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
