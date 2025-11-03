<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CodeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.CodeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td colspan="2">
            <asp:Label ID="lblParameterCaption" runat="server" Text="" Font-Bold="true" />
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Start" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboChartOfAccountCodeStart" Width="100%" runat="server"
                EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnItemDataBound="cboChartOfAccountCodeStart_ItemDataBound"
                OnItemsRequested="cboChartOfAccountCodeStart_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
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
            <asp:Label ID="Label1" runat="server" Text="End" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboChartOfAccountCodeEnd" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboChartOfAccountCodeEnd_ItemDataBound"
                OnItemsRequested="cboChartOfAccountCodeEnd_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
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
