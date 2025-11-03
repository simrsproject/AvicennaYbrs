<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FactoryCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.FactoryCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Factory" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboFabricID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboFabricID_ItemDataBound"
                OnItemsRequested="cboFabricID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "FabricName")%>
                    </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
