<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="CashEntryCrossRefference.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2.CashEntryCrossRefference" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtCashList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td class="label">
                Date
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtFilterDateFrom" runat="server" Width="105px">
                                <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtFilterDateTo" runat="server" Width="105px">
                                <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 20px" />
                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                OnClick="btnFilter_Click" ToolTip="Search" />
            <td />
        </tr>
        <tr>
            <td class="label">
                Description
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFilterDesc" runat="server" Width="340px" MaxLength="150"></telerik:RadTextBox>
            </td>
            <td style="width: 20px" />
                <asp:ImageButton ID="btnFilterDesc" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnFilter_Click" ToolTip="Search" />
            <td />
        </tr>
    </table>
    <telerik:RadGrid ID="grdListItem" runat="server" AllowPaging="true" AllowCustomPaging="true"
        PageSize="18" ShowFooter="True" OnNeedDataSource="grdListItem_NeedDataSource"
        OnDetailTableDataBind="grdListItem_DetailTableDataBind"
        AutoGenerateColumns="False" GridLines="Horizontal">
        <MasterTableView DataKeyNames="DetailId" GroupLoadMode="Client" CommandItemDisplay="None">
            <Columns>
                <telerik:GridBoundColumn DataField="DetailId" UniqueName="DetailId" SortExpression="DetailId"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="JournalNumber" UniqueName="JournalNumber"
                    SortExpression="JournalNumber" HeaderText="Journal Number" HeaderStyle-Width="120px" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="TransactionDate"
                    HeaderText="Transaction Date" UniqueName="TransactionDate" SortExpression="TransactionDate" />
                <telerik:GridBoundColumn DataField="BankName" HeaderText="Bank Name"
                    UniqueName="BankName" SortExpression="BankName" />
                <telerik:GridBoundColumn DataField="Description_Detail" UniqueName="Description_Detail" SortExpression="Description_Detail"
                    HeaderText="Description" />
                <telerik:GridBoundColumn DataField="SubLedgerName" UniqueName="SubLedgerName" SortExpression="SubLedgerName"
                    HeaderText="Sub Ledger" />
                <telerik:GridBoundColumn DataField="DayLen" UniqueName="DayLen" SortExpression="DayLen"
                    HeaderText="Days" HeaderStyle-Width="60px"/>
                <telerik:GridNumericColumn DataField="Debit" HeaderText="Debit" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="Debit" SortExpression="Debit" >
                    <HeaderStyle HorizontalAlign="Right" Width="100" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="CreditAmountRef" HeaderText="Credit" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="CreditAmountRef" SortExpression="CreditAmountRef" >
                    <HeaderStyle HorizontalAlign="Right" Width="100" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="Balance" HeaderText="Balance" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="Balance" SortExpression="Balance" >
                    <HeaderStyle HorizontalAlign="Right" Width="100" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="DetailId" Name="grdDetail" Width="100%"
                    AutoGenerateColumns="false" AllowPaging="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description"
                            UniqueName="Description" SortExpression="Description" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DebitRealization" HeaderText="Realization"
                            UniqueName="DebitRealization" SortExpression="DebitRealization" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <br /><br />
</asp:Content>
