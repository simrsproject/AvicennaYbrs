<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CashTransactionTemplateItem.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.CashTransactionTemplateItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMemorialItem" runat="server" ValidationGroup="MemorialItem" />
<asp:CustomValidator ErrorMessage="" ID="customValidator" OnServerValidate="customValidator_ServerValidate"
    runat="server" ValidationGroup="MemorialItem">&nbsp;</asp:CustomValidator>
<asp:Label ID="lblDetailId" runat="server" Visible="false"></asp:Label>
<table width="100%">
    <tr>
        <td class="label">
            Account Code
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="txtChartOfAccountCode" TabIndex="0" runat="server" Height="310px"
                Width="300px" AutoPostBack="true" HighlightTemplatedItems="true" DataTextField="ChartOfAccountCode"
                DataValueField="ChartOfAccountId" NoWrap="true">
                <ItemTemplate>
                    <div>
                        <b>
                            <%# Eval("ChartOfAccountCode")%></b><%# Eval("ChartOfAccountName", " - {0}") %>
                    </div>
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
        <td style="width: 20px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Account Code required."
                ControlToValidate="txtChartOfAccountCode" SetFocusOnError="True" ValidationGroup="MemorialItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            Account Name
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtChartOfAccountName" runat="server" Width="300px" ReadOnly="true" />
        </td>
        <td style="width: 20px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Account Name."
                ValidationGroup="MemorialItem" ControlToValidate="txtChartOfAccountName" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            Sub Ledger
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="ddlSubLedger" TabIndex="0" runat="server" Height="190px"
                Width="300px" AutoPostBack="true" HighlightTemplatedItems="true" DataTextField="Description"
                DataValueField="SubLedgerId" NoWrap="true">
                <ItemTemplate>
                    <div>
                        <b>
                            <%# Eval("SubLedgerName")%></b><%# Eval("Description", " - {0}") %>
                    </div>
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
        <td style="width: 20px" />
        <td />
    </tr>
    <tr>
        <td class="label">
            Variable Amount Percentage
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtAmountVariablePctg" runat="server" Value="0" Width="150px"
                Type="Percent" MaxValue="100" MinValue="0">
                <EnabledStyle HorizontalAlign="Right" />
            </telerik:RadNumericTextBox>
        </td>
        <td style="width: 20px" />
        <td />
    </tr>
    <tr>
        <td class="label">
            Fixed Amount
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtAmountFixed" runat="server" Value="0" Width="150px" MinValue="0">
                <EnabledStyle HorizontalAlign="Right" />
            </telerik:RadNumericTextBox>
        </td>
        <td style="width: 20px" />
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" ValidationGroup="MemorialItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="MemorialItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel" />
        </td>
        <td style="width: 20px" />
        <td />
    </tr>
</table>
