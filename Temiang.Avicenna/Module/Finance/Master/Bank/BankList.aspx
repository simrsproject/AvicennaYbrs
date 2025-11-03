<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="BankList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.BankList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="BankID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BankID" HeaderText="ID"
                    UniqueName="BankID" SortExpression="BankID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BankName" HeaderText="Name" UniqueName="BankName"
                    SortExpression="BankName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="Chart Of Account" UniqueName="ChartOfAccountName"
                    SortExpression="ChartOfAccountName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="JournalCode" HeaderText="Journal Code" UniqueName="JournalCode"
                    SortExpression="JournalCode">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsToBeCleared" HeaderText="Reconcile"
                    UniqueName="IsToBeCleared" SortExpression="IsToBeCleared" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsCrossRefference" HeaderText="Cross Refference"
                    UniqueName="IsCrossRefference" SortExpression="IsCrossRefference" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />

                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsCashierFrontOffice" HeaderText="Front Office"
                    UniqueName="IsCashierFrontOffice" SortExpression="IsCashierFrontOffice" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsCashierFrontOfficeDpReturn" HeaderText="Front Office (DP Return)"
                    UniqueName="IsCashierFrontOfficeDpReturn" SortExpression="IsCashierFrontOfficeDpReturn" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsArPayment" HeaderText="AR Payment"
                    UniqueName="IsArPayment" SortExpression="IsArPayment" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApPayment" HeaderText="AP Payment"
                    UniqueName="IsApPayment" SortExpression="IsApPayment" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsFeePayment" HeaderText="Fee Payment"
                    UniqueName="IsFeePayment" SortExpression="IsFeePayment" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsAssetAuctionPayment" HeaderText="Asset Payment"
                    UniqueName="IsAssetAuctionPayment" SortExpression="IsAssetAuctionPayment" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsBKU" HeaderText="BKU"
                    UniqueName="IsBKU" SortExpression="IsBKU" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />

                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
