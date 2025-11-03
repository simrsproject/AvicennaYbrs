<%@ Control AutoEventWireup="true" CodeBehind="CashEntryItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2.CashEntryItemDetail"
    Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMemorialItem" runat="server" ValidationGroup="MemorialItem" />
<asp:CustomValidator ErrorMessage="" ID="customValidator" OnServerValidate="customValidator_ServerValidate"
    runat="server" ValidationGroup="MemorialItem">&nbsp;</asp:CustomValidator>
<asp:Label ID="lblDetailId" runat="server" Visible="false"></asp:Label>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr runat="server" id="trCashList">
                    <td class="label">
                        Cash List
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="txtCashList" TabIndex="0" runat="server" Height="190px"
                            Width="300px" AutoPostBack="true" HighlightTemplatedItems="true" DataTextField="Description"
                            DataValueField="ListId" NoWrap="true">
                            <ItemTemplate>
                                <div>
                                    <b>
                                        <%# Eval("ListId")%></b><%# Eval("Description", " - {0}") %>
                                </div>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                        <asp:Label ID="lblCashList" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
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
                        <asp:Label runat="server" ID="lblChartOfAccountCode" />
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
                        <asp:Label runat="server" ID="lblChartOfAccountName" />
                    </td>
                    <td style="width: 20px">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Account Name." ValidationGroup="MemorialItem"
                            ControlToValidate="txtChartOfAccountName" SetFocusOnError="True" Width="100%">
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
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Description
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDescription" TextMode="MultiLine" runat="server" Width="300px"
                            MaxLength="250" Height="60px" />
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        Amount
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtAmount" runat="server" Value="0" Width="150px">
                            <EnabledStyle HorizontalAlign="Right" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Parent Refference
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsParentRefference" runat="server" />
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" ValidationGroup="MemorialItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="MemorialItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                        </asp:Button>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
