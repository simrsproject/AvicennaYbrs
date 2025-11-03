<%@ Page Title="Fee Detail" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" 
CodeBehind="BkuJournalDetailDialog.aspx.cs" 
Inherits="Temiang.Avicenna.Module.Finance.BkuJournalDetailDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">
            function OpenBKU(coaid) {
                //alert(coaid);
                var oWnd = $find("<%= winDialog.ClientID %>");

                oWnd.setUrl("CoaEditBkuDialog.aspx?coaid=" + coaid);

                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    if (oWnd.argument == 'rebind') {
                        __doPostBack("<%= gridCoaSetting.UniqueID %>", 'rebind');
                        oWnd.argument = 'undefined';
                    }
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Maximize, Move, Close"
        Width="600px" Height="200px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Transaction Number
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNumber" runat="server" Width="287px" MaxLength="10" />
                            <asp:Label ID="lblTransactionNumber" runat="server"></asp:Label>
                        </td>
                        <td style="width: 20px;">

                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Transaction Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="105px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 20px;">

                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Reference Number
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><telerik:RadTextBox ID="txtRefferenceNumber" runat="server" Width="287px" MaxLength="10"
                                        Enabled="false" /></td>
                                    <td>
                                        <div id="divRefferenceNumber" runat="server">
                                            <asp:Label ID="lblRefferenceNumber" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td><asp:ImageButton ID="btnRecalculate" runat="server" ImageUrl="../../../Images/Toolbar/process16_h.png"
                                        CausesValidation="False" OnClick="btnRecalculate_Click" ToolTip="Reprocessing BKU Journal"
                                        Visible="true" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px;">
                            <asp:Label ID="lblJournalType" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Description
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" MaxLength="250"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px">

                        </td>
                        <td>
                        </td>        
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotalAmountDebit" runat="server" Text="Total Debit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotalAmountDebit" runat="server" ReadOnly="true"
                                Value="0" Width="150px">
                                <DisabledStyle HorizontalAlign="Right" Font-Bold="true" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotalAmountCredit" runat="server" Text="Total Credit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotalAmountCredit" runat="server" ReadOnly="true"
                                Value="0" Width="150px">
                                <DisabledStyle HorizontalAlign="Right" Font-Bold="true" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblSelisih" runat="server" Text="Difference"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSelisih" runat="server" ReadOnly="true" Value="0"
                                Width="150px">
                                <DisabledStyle HorizontalAlign="Right" Font-Bold="true" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td class="label">
                            <asp:Label ID="lblApprove" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" Enabled="false" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" Enabled="false" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
        <Tabs>
            <telerik:RadTab runat="server" Text="Detail" Selected="True" PageViewID="pgJournalDetail" />
            <telerik:RadTab runat="server" Text="Reference" PageViewID="pgInfoDetail" Visible="false"  />
            <telerik:RadTab runat="server" Text="Setting Chart Of Account And LOG" PageViewID="pgInfoSettingCoa" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgJournalDetail" runat="server">
            <telerik:RadGrid ID="grdVoucherEntryItem" runat="server" AllowPaging="false" AllowCustomPaging="false"
                PageSize="18" ShowFooter="True" OnNeedDataSource="grdVoucherEntryItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="Horizontal">
                <MasterTableView DataKeyNames="BkuDetailId" ShowGroupFooter="True">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="100px" DataTextField="ChartOfAccountCode" 
                            HeaderText="Account Code" UniqueName="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="COAJournal" 
                            DataNavigateUrlFields="ChartOfAccountID"
                            DataNavigateUrlFormatString="javascript:OpenBKU('{0}');"
                        />
                        <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="300px" DataField="ChartOfAccountName"
                            HeaderText="Account Name" UniqueName="ChartOfAccountName" />
                        <telerik:GridBoundColumn DataField="SubLedgerId" HeaderText="Subledger ID" UniqueName="SubLedgerId"
                            Visible="false" />
                        <telerik:GridBoundColumn ItemStyle-Wrap="True" HeaderStyle-Width="150px" DataField="SubLedgerName"
                            HeaderText="Subledger" UniqueName="SubLedgerName" />
                        <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="150px" DataField="DocumentNumber"
                            HeaderText="Reference#" UniqueName="DocumentNumber" FooterStyle-HorizontalAlign="Right"
                            Aggregate="Count" FooterAggregateFormatString="Total :" />
                        <telerik:GridBoundColumn DataField="Debit" HeaderText="Debit" UniqueName="Debit"
                            DataFormatString="{0:N2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Center" Width="115px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Credit" HeaderText="Credit" UniqueName="Credit"
                            DataFormatString="{0:N2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Center" Width="115px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                            ItemStyle-Wrap="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgInfoDetail" runat="server">

        </telerik:RadPageView>
        <telerik:RadPageView ID="pgInfoSettingCoa" runat="server">
            <telerik:RadGrid ID="gridCoaSetting" runat="server" AllowPaging="false" AllowCustomPaging="false"
                PageSize="18" ShowFooter="True" OnNeedDataSource="gridCoaSetting_NeedDataSource"
                AutoGenerateColumns="False" GridLines="Horizontal">
                <MasterTableView DataKeyNames="ChartOfAccountID" ShowGroupFooter="True">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Chart Of Account Journal" Name="COAJournal" />
                        <telerik:GridColumnGroup HeaderText="Chart Of Account BKU" Name="COABku" />
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="100px" DataTextField="ChartOfAccountCode" 
                            HeaderText="Account Code" UniqueName="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="COAJournal" 
                            DataNavigateUrlFields="ChartOfAccountID"
                            DataNavigateUrlFormatString="javascript:OpenBKU('{0}');"
                        />
                        <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="300px" DataField="ChartOfAccountName"
                            HeaderText="Account Name" UniqueName="ChartOfAccountName" ColumnGroupName="COAJournal" />
                        
                        <telerik:GridBoundColumn ItemStyle-Wrap="false" HeaderStyle-Width="100px" DataField="ChartOfAccountCodeBKU"
                            HeaderText="Account Code" UniqueName="ChartOfAccountCodeBKU" SortExpression="ChartOfAccountCodeBKU" ColumnGroupName="COABku" />
                        <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="300px" DataField="ChartOfAccountNameBKU"
                            HeaderText="Account Name" UniqueName="ChartOfAccountNameBKU" ColumnGroupName="COABku" />

                        <telerik:GridBoundColumn DataField="Message" HeaderText="Message" UniqueName="Message"
                            ItemStyle-Wrap="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br /><br /><br />
</asp:Content>