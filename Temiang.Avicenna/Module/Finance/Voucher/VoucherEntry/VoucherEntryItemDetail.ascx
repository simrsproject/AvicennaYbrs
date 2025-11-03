<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VoucherEntryItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.VoucherEntryItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMemorialItem" runat="server" ValidationGroup="MemorialItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="MemorialItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:Label ID="lblDetailId" runat="server" Visible="false"></asp:Label>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 38%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label" style="width: 79px">
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
                        <asp:Label runat="server" ID="lblChartOfAccountCode" />
                    </td>
                    <td style="width: 20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Account Code required."
                            ControlToValidate="txtChartOfAccountCode" SetFocusOnError="True" ValidationGroup="MemorialItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 79px; height: 15px;">
                        Account Name
                    </td>
                    <td class="entry" style="height: 15px;">
                        <telerik:RadTextBox ID="txtChartOfAccountName" runat="server" Width="300px" ReadOnly="true" />
                        <asp:Label runat="server" ID="lblChartOfAccountName" />
                    </td>
                    <td style="height: 15px; width: 20px;">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Account Name." ValidationGroup="MemorialItem"
                            ControlToValidate="txtChartOfAccountName" SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 79px; height: 15px;">
                        Sub Ledger
                    </td>
                    <td class="entry" style="height: 15px;">
                        <telerik:RadComboBox ID="ddlSubLedger" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td style="height: 15px; width: 20px;">
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 79px; height: 15px;">
                        Reference#
                    </td>
                    <td class="entry" style="height: 15px;">
                        <telerik:RadTextBox ID="txtDocumentNumber" runat="server" Width="300px" MaxLength="25" />
                    </td>
                    <td style="height: 15px; width: 20px;">
                    </td>
                </tr>
                <tr style="display:none;">
                    <td class="label" style="width: 79px; height: 15px;">
                        RegistrationNo
                    </td>
                    <td class="entry" style="height: 15px;">
                        <telerik:RadTextBox ID="txtRegistrationNo" TextMode="MultiLine" runat="server" Width="300px"
                            MaxLength="250" Height="60px" />
                    </td>
                    <td style="height: 15px; width: 20px;">
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 79px; height: 15px;">
                        Description
                    </td>
                    <td class="entry" style="height: 15px;">
                        <telerik:RadTextBox ID="txtDescription" TextMode="MultiLine" runat="server" Width="300px"
                            MaxLength="250" Height="60px" />
                    </td>
                    <td style="height: 15px; width: 20px;">
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 60%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label" style="width: 16px">
                        Debit Amount
                    </td>
                    <td class="entry" style="width: 361px">
                        <telerik:RadNumericTextBox ID="txtDebit" runat="server" Value="0" Width="150px">
                            <EnabledStyle HorizontalAlign="Right" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 16px">
                        Credit Amount
                    </td>
                    <td class="entry" style="width: 361px">
                        <telerik:RadNumericTextBox ID="txtCredit" runat="server" Value="0" Width="150px">
                            <EnabledStyle HorizontalAlign="Right" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="entry" align="center" style="width: 361px">
                        <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" ValidationGroup="BeginningBalanceItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="BeginningBalanceItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                        </asp:Button>
                    </td>
                    <td style="width: 20px">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
