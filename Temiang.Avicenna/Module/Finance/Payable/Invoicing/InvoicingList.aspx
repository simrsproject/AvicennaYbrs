<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="InvoicingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Payable.InvoicingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type, pg) {
                var url = "InvoicingDetail.aspx?md=new&type=" + type + "&pg=" + pg;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind" AllowPaging="true" AllowCustomPaging="true" PageSize="15">
        <MasterTableView DataKeyNames="InvoiceNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="InvoiceNo" HeaderText="Invoice No"
                    UniqueName="InvoiceNo" SortExpression="InvoiceNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="InvoiceDate" HeaderText="Date"
                    UniqueName="InvoiceDate" SortExpression="InvoiceDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="InvoiceDueDate" HeaderText="Due Date"
                    UniqueName="InvoiceDueDate" SortExpression="InvoiceDueDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Supplier" UniqueName="SupplierName"
                    SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PayableStatus"
                    HeaderText="Status" UniqueName="PayableStatus" SortExpression="PayableStatus"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Total" HeaderText="Total"
                    UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="InvoiceNo, TransactionNo" Name="grdDetail" Width="100%"
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="TransactionNo" HeaderText="Trans/Seq No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="InvoiceSuppNo" HeaderText="Invoice Supp. No"
                            UniqueName="InvoiceSuppNo" SortExpression="InvoiceSuppNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="InvoiceSupplierDate"
                            HeaderText="Invoice Supp. Date" UniqueName="InvoiceSupplierDate" SortExpression="InvoiceSupplierDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="110px" DataField="Amount" HeaderText="Amount"
                            UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PPnAmount" HeaderText="PPn"
                            UniqueName="PPnAmount" SortExpression="PPnAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PphTypeName" HeaderText="Pph Type"
                            UniqueName="PphTypeName" SortExpression="PphTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <%--                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PPh22Amount" HeaderText="PPh 22"
                            UniqueName="PPh22Amount" SortExpression="PPh22Amount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PPh23Amount" HeaderText="PPh 23"
                            UniqueName="PPh23Amount" SortExpression="PPh23Amount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />--%>
                        <telerik:GridNumericColumn HeaderStyle-Width="110px" DataField="PphAmount" HeaderText="Pph Amount"
                            UniqueName="PphAmount" SortExpression="PphAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="StampAmount" HeaderText="Stamp / Shipping"
                            UniqueName="StampAmount" SortExpression="StampAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DownPaymentAmount"
                            HeaderText="Down Payment" UniqueName="DownPaymentAmount" SortExpression="DownPaymentAmount"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="OtherDeduction" HeaderText="Other Deduction"
                            UniqueName="OtherDeduction" SortExpression="OtherDeduction" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="VerifyDate" HeaderText="Verify Date"
                            UniqueName="VerifyDate" SortExpression="VerifyDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="110px" DataField="VerifyAmount" HeaderText="Verify"
                            UniqueName="VerifyAmount" SortExpression="VerifyAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="110px" DataField="PaymentAmount" HeaderText="Payment"
                            UniqueName="PaymentAmount" SortExpression="PaymentAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Center" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
