<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="OpeningBalanceList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Cashier.OpeningBalanceList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="TransactionNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="OpeningDate" HeaderText="Opening Date" UniqueName="OpeningDate"
                    SortExpression="OpeningDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OpenedBy" HeaderText="Opened By"
                    UniqueName="OpenedBy" SortExpression="OpenedBy" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ShiftName" HeaderText="Shift"
                    UniqueName="ShiftName" SortExpression="ShiftName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CashierCounterName"
                    HeaderText="Cashier Counter" UniqueName="CashierCounterName" SortExpression="CashierCounterName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="OpeningBalance" HeaderText="Opening Balance"
                    UniqueName="OpeningBalance" SortExpression="OpeningBalance" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="ClosingBalance" HeaderText="Closing Balance"
                    UniqueName="ClosingBalance" SortExpression="ClosingBalance" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn DataField="ClosingDate" HeaderText="Closing Date" UniqueName="ClosingDate"
                    SortExpression="ClosingDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ClosedBy" HeaderText="Closed By"
                    UniqueName="ClosedBy" SortExpression="ClosedBy" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="CashierUserID" AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CashierUserID" HeaderText="ID"
                            UniqueName="CashierUserID" SortExpression="CashierUserID" />
                        <telerik:GridBoundColumn DataField="CashierUserName" HeaderText="Cashier Name" UniqueName="CashierUserName"
                            SortExpression="CashierUserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsCashierCheckin"
                            HeaderText="Checkin" UniqueName="IsCashierCheckin" SortExpression="IsCashierCheckin"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="CashierCheckinDateTime" HeaderText="Checkin Date/Time"
                            UniqueName="CashierCheckinDateTime" SortExpression="CashierCheckinDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
            <ExpandCollapseColumn Visible="True" />
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
