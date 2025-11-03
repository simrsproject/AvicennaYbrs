<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAImpressionGroupCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.PAImpressionGroupCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Item" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboPAImpressionGroup" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboPAImpressionGroup_ItemDataBound"
                OnItemsRequested="cboPAImpressionGroup_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "GroupID")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "GroupName")%>
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