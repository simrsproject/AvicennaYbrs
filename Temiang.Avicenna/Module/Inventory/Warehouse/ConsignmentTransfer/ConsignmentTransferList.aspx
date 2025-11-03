<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="ConsignmentTransferList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.ConsignmentTransferList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="TransactionNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SupplierName" HeaderText="Supplier"
                    UniqueName="SupplierName" SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />    
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FromLocationName" HeaderText="From Location"
                    UniqueName="FromLocationName" SortExpression="FromLocationName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ToServiceUnitName" HeaderText="To Service Unit"
                    UniqueName="ToServiceUnitName" SortExpression="ToServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ToLocationName" HeaderText="To Location"
                    UniqueName="ToLocationName" SortExpression="ToLocationName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />    
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemType" HeaderText="Item Type"
                    UniqueName="ItemType" SortExpression="ItemType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="SequenceNo" Name="grdDetail" AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="10" ShowFooter="True">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Quantity" HeaderText="Qty"
                            UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Unit"
                            UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                            DataType="System.Double" DataFields="Price, Quantity" Expression="{0}*{1}" FooterText=" "
                            FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" Aggregate="Sum"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>