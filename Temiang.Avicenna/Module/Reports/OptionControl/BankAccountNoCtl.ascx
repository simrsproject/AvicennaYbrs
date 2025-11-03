<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BankAccountNoCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.BankAccountNoCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Account No" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboAccountNo" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboAccountNo_ItemDataBound"
                OnItemsRequested="cboAccountNo_ItemsRequested">
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