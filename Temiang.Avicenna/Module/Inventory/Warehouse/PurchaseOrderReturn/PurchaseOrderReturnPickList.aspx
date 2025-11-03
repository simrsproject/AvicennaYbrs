<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PurchaseOrderReturnPickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.PurchaseOrderReturnPickList"
    Title="Purchase Order Received List" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%=grdDetail.UniqueID%>", "rebind:" + args.getDataKeyValue("TransactionNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchReceivedNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchInvoiceNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBatchNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchExpiredDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemID_ItemDataBound"
                                OnItemsRequested="cboItemID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 30 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchItem" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblReceivedNo" Text="Received No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtReceivedNo" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchReceivedNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblInvoiceNo" Text="Invoice Supplier No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtInvoiceNo" Width="300px" MaxLength="30" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchInvoiceNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblBatchNo" Text="Batch No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtBatchNo" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchBatchNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblExpiredDate" Text="Expiry Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtExpiredDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchExpiredDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" ShowStatusBar="true" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="True" AutoGenerateColumns="False">
        <PagerStyle Mode="NextPrevAndNumeric" />
        <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo"
            PageSize="10">
            <Columns>
                <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Supplier" UniqueName="SupplierName"
                    SortExpression="SupplierName">
                    <HeaderStyle HorizontalAlign="Left" Width="250px"/>
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="PO No" UniqueName="ReferenceNo"
                    SortExpression="ReferenceNo">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Received No" UniqueName="TransactionNo"
                    SortExpression="TransactionNo">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InvoiceNo" HeaderText="Invoice Supp. No" UniqueName="InvoiceNo"
                    SortExpression="InvoiceNo">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="TransactionDate" HeaderText="Received Date"
                    UniqueName="TransactionDate" SortExpression="TransactionDate">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName">
                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" />
            <ClientEvents OnRowSelected="RowSelected" />
        </ClientSettings>
    </telerik:RadGrid>
    <br />
    <telerik:RadGrid ID="grdDetail" runat="server" AutoGenerateColumns="False" GridLines="None"
        OnNeedDataSource="grdDetail_NeedDataSource" OnPageIndexChanged="grdDetail_PageIndexChanged"
        AllowPaging="False">
        <PagerStyle Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo, ItemID, BatchNumber">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Item Name" UniqueName="Description"
                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Quantity" HeaderText="Received Qty"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="QuantityFinishInBaseUnit"
                    HeaderText="Returned" UniqueName="QuantityFinishInBaseUnit" SortExpression="QuantityFinishInBaseUnit"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Return Qty" HeaderStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQtyInput" runat="server" Width="80px" DbValue='<%#Eval("QtyInput")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="False" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="90px" DbValue='<%#Eval("Price")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsDiscountInPercent"
                    HeaderText="Disc In %" UniqueName="IsDiscountInPercent" SortExpression="IsDiscountInPercent"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc In %" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsDiscInPercent" runat="server" Width="50px" Checked='<%#Eval("IsDiscountInPercent")%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Discount1Percentage"
                    HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                    Visible="False" />
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc 1 (%)" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtDisc1" runat="server" Width="50px" DbValue='<%#Eval("Discount1Percentage")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Discount2Percentage"
                    HeaderText="Disc 2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                    Visible="False" />
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc 2 (%)" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtDisc2" runat="server" Width="50px" DbValue='<%#Eval("Discount2Percentage")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc Amount"
                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="False" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Disc Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtDiscAmt" runat="server" Width="90px" DbValue='<%#Eval("Discount")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BatchNumber" HeaderText="Batch No"
                    UniqueName="BatchNumber" SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn DataField="ExpiredDate" HeaderText="Expired Date" UniqueName="ExpiredDate"
                    SortExpression="ExpiredDate">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
