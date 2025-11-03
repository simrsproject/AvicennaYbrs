<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APPaymentTypeDetail.aspx.cs"
    MasterPageFile="~/MasterPage/MasterDialog.Master" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction.APPaymentTypeDetail" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
        OnUpdateCommand="grdDetail_UpdateCommand"
        AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
        PageSize="15">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, ReferenceNo"
            GroupLoadMode="Client">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="25px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ReferenceNo" HeaderText="Reference No"
                    UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SupplierName" HeaderText="Supplier"
                    UniqueName="SupplierName" SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Payment Date"
                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="PaymentMethod" HeaderText="Payment Method" HeaderStyle-Width="120px"
                    UniqueName="PaymentMethod" SortExpression="PaymentMethod" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="Bank" HeaderText="Bank"
                    UniqueName="Bank" SortExpression="Bank" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="Total :"
                    Aggregate="Count" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PaymentAmount" HeaderText="Amount"
                    UniqueName="PaymentAmount" SortExpression="PaymentAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" />
                <telerik:GridNumericColumn DataField="PPNAmount" HeaderText="PPn Amount" UniqueName="PPNAmount"
                    SortExpression="PPNAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" />
                <telerik:GridNumericColumn DataField="PPh22Amount" HeaderText="PPh22 Amount" UniqueName="PPh22Amount"
                    SortExpression="PPh22Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" />
                <telerik:GridNumericColumn DataField="PPh23Amount" HeaderText="PPh23 Amount" UniqueName="PPh23Amount"
                    SortExpression="PPh23Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" />
                <telerik:GridNumericColumn DataField="StampAmount" HeaderText="Stamp Amount" UniqueName="StampAmount"
                    SortExpression="StampAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" />
                <telerik:GridNumericColumn DataField="OtherDeduction" HeaderText="Other Deduction" UniqueName="OtherDeduction"
                    SortExpression="OtherDeduction" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" />
            </Columns>
            <EditFormSettings UserControlName="APPaymentTypeDetailEdit.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandColumn1">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
