<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ARPaymentTypeDetail.aspx.cs" MasterPageFile="~/MasterPage/MasterDialog.Master"
    Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction.ARPaymentTypeDetail" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
        PageSize="15">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, ReferenceNo" GroupLoadMode="Client">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                    UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ReferenceNo" HeaderText="Reference No"
                    UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor"
                    UniqueName="GuarantorName" SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Payment Date"
                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="PaymentType" HeaderText="Payment Type" HeaderStyle-Width="120px"
                    UniqueName="PaymentType" SortExpression="PaymentType" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="PaymentMethod" HeaderText="Payment Method" HeaderStyle-Width="120px"
                    UniqueName="PaymentMethod" SortExpression="PaymentMethod" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="170px" DataField="Bank" HeaderText="Bank"
                    UniqueName="Bank" SortExpression="Bank" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right"
                    FooterAggregateFormatString="Total :" Aggregate="Count" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PaymentAmount" HeaderText="Amount"
                    UniqueName="PaymentAmount" SortExpression="PaymentAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" />

            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>

