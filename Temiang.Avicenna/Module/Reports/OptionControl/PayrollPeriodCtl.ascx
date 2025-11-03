<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PayrollPeriodCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.PayrollPeriodCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Payroll Period" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboEntry" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboEntry_ItemDataBound"
                OnItemsRequested="cboEntry_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodName")%> </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 12 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
