<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="SubLedgerBalanceDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.SubLedgerBalanceDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function gotoDetailUrl(journalID, journalType) {
                location.replace('VoucherEntryDetail.aspx?md=view&ivd=' + journalID + '&ivt=01&source=je&jt=' + journalType + '&pg=0');
            }
        </script>

    </telerik:RadCodeBlock>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Parameters">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="30%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                Periode
                            </td>
                            <td class="entry">
                                <asp:Literal ID="ltrDateRange" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: left;">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Account
                            </td>
                            <td class="entry">
                                <asp:Literal ID="ltrChartOfAccount" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Sub Ledger
                            </td>
                            <td class="entry">
                                <asp:Literal ID="ltrSubLedger" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="30%" style="v-align: top;">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                Beginning Balance
                            </td>
                            <td class="entry" style="text-align: right;">
                                <asp:Literal ID="ltrBeginningBalance" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: left;">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Ending Balance
                            </td>
                            <td class="entry" style="text-align: right;">
                                <asp:Literal ID="ltrEndingBalance" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: left;">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                            </td>
                            <td class="entry">
                            </td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="30%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                Total Debit
                            </td>
                            <td class="entry" style="text-align: right;">
                                <asp:Literal ID="ltrTotalDebit" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: left;">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Total Credit
                            </td>
                            <td class="entry" style="text-align: right;">
                                <asp:Literal ID="ltrTotalCredit" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: left;">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                            </td>
                            <td class="entry">
                            </td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="true" AllowCustomPaging="true" PageSize="50">
        <MasterTableView DataKeyNames="">
            <Columns>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <%#string.IsNullOrEmpty(Eval("TransactionNumber").ToString()) ? string.Empty : string.Format("<a style='cursor:pointer;' onclick='javascript:gotoDetailUrl(\"{0}\",\"{1}\")'><img src='../../../../Images/Toolbar/search16.png'/></a>", Eval("JournalID"), Eval("JournalType"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" AllowSorting="false" DataField="TransactionDate"
                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    DataFormatString="dd/MM/yyyy" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn ItemStyle-Wrap="false" AllowSorting="false" HeaderStyle-Width="120px"
                    DataField="TransactionNumber" HeaderText="No" UniqueName="TransactionNumber"
                    SortExpression="TransactionNumber" />
                <telerik:GridBoundColumn HeaderStyle-Width="355px" DataField="Description" AllowSorting="false"
                    HeaderText="Description" UniqueName="Description" />
                <telerik:GridBoundColumn DataField="SubLedgerName" ItemStyle-Wrap="false" AllowSorting="false"
                    HeaderText="Subledger" UniqueName="SubLedgerName" SortExpression="SubLedgerName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="Debit" AllowSorting="false"
                    HeaderText="Debit" UniqueName="Debit" DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="Credit" AllowSorting="false"
                    HeaderText="Credit" UniqueName="Credit" DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="Balance" AllowSorting="false"
                    HeaderText="Balance" UniqueName="Balance" DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
