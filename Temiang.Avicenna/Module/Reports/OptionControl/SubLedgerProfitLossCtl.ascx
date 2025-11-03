<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubLedgerProfitLossCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.SubLedgerProfitLossCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Subledger Unit" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboSubledger" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboSubLedger_ItemDataBound" OnItemsRequested="cboSubLedger_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
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
