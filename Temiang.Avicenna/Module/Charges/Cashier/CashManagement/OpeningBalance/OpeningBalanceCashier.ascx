<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpeningBalanceCashier.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.Cashier.OpeningBalanceCashier" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCashManagementCashier" runat="server" ValidationGroup="CashManagementCashier" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CashManagementCashier"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblCashierUserID" runat="server" Text="Cashier Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboCashierUserID" Width="304px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboCashierUserID_ItemDataBound"
                OnItemsRequested="cboCashierUserID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvCashierUserID" runat="server" ErrorMessage="Cashier Name required."
                ValidationGroup="entry" ControlToValidate="cboCashierUserID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CashManagementCashier"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
            </asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="CashManagementCashier" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
