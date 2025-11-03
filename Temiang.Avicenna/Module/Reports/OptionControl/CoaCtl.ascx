<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CoaCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.CoaCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Chart Of Account" />
        </td>        
        <td>
            <telerik:RadComboBox ID="cboChartOfAccountCode" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboChartOfAccountCode_ItemDataBound"
                OnItemsRequested="cboChartOfAccount_ItemsRequested">
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
