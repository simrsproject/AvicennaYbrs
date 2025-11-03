<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateSalaryItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Payroll.Transaction.UpdateSalaryItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumWageTransactionItem" runat="server" ValidationGroup="WageTransactionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="WageTransactionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblWageTransactionItemID" runat="server" Text="WageTransactionItemID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtWageTransactionItemID" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvWageTransactionItemID" runat="server" ErrorMessage="WageTransaction Item ID required."
                ControlToValidate="txtWageTransactionItemID" SetFocusOnError="True" ValidationGroup="WageTransactionItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSalaryComponentID" runat="server" Text="Salary Component Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSalaryComponentID" runat="server" Width="304px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSalaryComponentID_ItemDataBound"
                OnItemsRequested="cboSalaryComponentID_ItemsRequested" Enabled="False">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSalaryComponentID" runat="server" ErrorMessage="Salary Component ID required."
                ControlToValidate="cboSalaryComponentID" SetFocusOnError="True" ValidationGroup="WageTransactionItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblNominalAmount" runat="server" Text="Nominal Amount"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtNominalAmount" runat="server" Width="100px" AutoPostBack="True"
                OnTextChanged="txtNominalAmount_TextChanged" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvNominalAmount" runat="server" ErrorMessage="Nominal Amount required."
                ControlToValidate="txtNominalAmount" SetFocusOnError="True" ValidationGroup="WageTransactionItem"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblCurrencyRate" runat="server" Text="Currency Code / Rate"></asp:Label>
        </td>
        <td class="entry">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadTextBox ID="txtSRCurrencyCode" runat="server" Width="50px" ReadOnly="True" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtCurrencyRate" runat="server" Width="44px" ReadOnly="True" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblCurrencyAmount" runat="server" Text="Currency Amount"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtCurrencyAmount" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvCurrencyAmount" runat="server" ErrorMessage="Currency Amount required."
                ControlToValidate="txtCurrencyAmount" SetFocusOnError="True" ValidationGroup="WageTransactionItem"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="WageTransactionItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="WageTransactionItem" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
