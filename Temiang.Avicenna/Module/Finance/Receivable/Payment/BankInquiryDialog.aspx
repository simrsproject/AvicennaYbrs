<%@ Page Title="Import" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="BankInquiryDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.BankInquiryDialog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdInquiryDetail" runat="server" AutoGenerateColumns="false"
                    GridLines="None" OnNeedDataSource="grdInquiryDetail_NeedDataSource"
                    AllowPaging="True" PageSize="18" AllowSorting="False" AllowCustomPaging="true" >
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    <MasterTableView ClientDataKeyNames="TransactionID" DataKeyNames="TransactionID" 
                    InsertItemPageIndexAction="ShowItemOnCurrentPage">
                        <Columns>
                            <telerik:GridBoundColumn DataField="AccountNo" HeaderText="AccountNo" SortExpression="AccountNo"
                                UniqueName="AccountNo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransactionDateTime" HeaderText="Date"
                                DataFormatString="{0:MM/dd/yyyy}" DataType="System.DateTime" 
                                SortExpression="TransactionDateTime" UniqueName="TransactionDateTime" >
                                <HeaderStyle HorizontalAlign="Left" Width="130" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AccountNoAlias" HeaderText="AccountNoAlias" SortExpression="AccountNoAlias"
                                UniqueName="AccountNoAlias">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                                UniqueName="Description">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="ReferenceNo" SortExpression="ReferenceNo"
                                UniqueName="ReferenceNo">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn DataField="Debit" HeaderText="Debit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                UniqueName="Debit" SortExpression="Debit" >
                                <HeaderStyle HorizontalAlign="Right" Width="150" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn DataField="Credit" HeaderText="Credit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                UniqueName="Credit" SortExpression="Credit" >
                                <HeaderStyle HorizontalAlign="Right" Width="150" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn DataField="SRCashTransactionCode" HeaderText="Trans Code" SortExpression="SRCashTransactionCode"
                                UniqueName="SRCashTransactionCode">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
