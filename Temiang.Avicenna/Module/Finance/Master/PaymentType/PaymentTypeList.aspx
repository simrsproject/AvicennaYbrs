<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="PaymentTypeList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.PaymentTypeList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="SRPaymentTypeID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SRPaymentTypeID" HeaderText="Payment Type ID"
                    UniqueName="SRPaymentTypeID" SortExpression="SRPaymentTypeID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PaymentTypeName" HeaderText="Payment Type Name"
                    UniqueName="PaymentTypeName" SortExpression="PaymentTypeName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="Chart Of Account"
                    UniqueName="ChartOfAccountName" SortExpression="ChartOfAccountName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SubLedgerName" HeaderText="Subledger" UniqueName="SubLedgerName"
                    SortExpression="SubLedgerName">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="isCashierFrontOffice"
                    HeaderText="Cashier Front Office" UniqueName="isCashierFrontOffice" SortExpression="isCashierFrontOffice"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsArPayment" HeaderText="A/R Payment"
                    UniqueName="IsArPayment" SortExpression="IsArPayment" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApPayment" HeaderText="A/P Payment"
                    UniqueName="IsApPayment" SortExpression="IsApPayment" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsFeePayment" HeaderText="Fee Payment"
                    UniqueName="IsFeePayment" SortExpression="IsFeePayment" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAssetAuctionPayment" HeaderText="Asset Payment"
                    UniqueName="IsAssetAuctionPayment" SortExpression="IsAssetAuctionPayment" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
