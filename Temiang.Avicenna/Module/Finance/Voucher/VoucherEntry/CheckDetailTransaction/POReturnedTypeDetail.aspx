<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POReturnedTypeDetail.aspx.cs"
    MasterPageFile="~/MasterPage/MasterDialog.Master" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction.POReturnedTypeDetail" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
        PageSize="15">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
            GroupLoadMode="Client">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="SupplierName" HeaderStyle-Width="150px" ItemStyle-Wrap="true"
                    HeaderText="Supplier Name" UniqueName="SupplierName" SortExpression="SupplierName"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderStyle-Width="150px" ItemStyle-Wrap="true"
                    HeaderText="Service Unit" UniqueName="ServiceUnitName" SortExpression="ServiceUnitName"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn DataField="Quantity" HeaderText="Quantity" HeaderStyle-Width="80px"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="Unit" SortExpression="Unit" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Count" FooterAggregateFormatString="Total :" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                    FooterStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Discount"
                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                    FooterStyle-HorizontalAlign="Right" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Received Amount
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtReceivedAmount" runat="server" MaxLength="16" MinValue="0"
                                ReadOnly="true" Width="150px" />
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Tax Amount
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTaxAmount" runat="server" MaxLength="16" MinValue="0"
                                ReadOnly="true" Width="150px" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
