<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitLocationCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ServiceUnitLocationCtl" %>
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
            <asp:Label ID="lblLocation" runat="server" Text="Location" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboLocationID" runat="server" Width="100%">
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
