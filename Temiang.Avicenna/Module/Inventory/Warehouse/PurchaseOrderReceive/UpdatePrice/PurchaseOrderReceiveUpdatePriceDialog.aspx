<%@ Page Title="Purchase Order Receive (Update Price)" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="PurchaseOrderReceiveUpdatePriceDialog.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.PurchaseOrderReceiveUpdatePriceDialog" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtTaxAmount">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTaxPercentage" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Received No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Received Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" Enabled="False">
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplierName" runat="server" Text="Supplier"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSupplierName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferenceNo" runat="server" Text="PO Reference"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnit" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceNo" runat="server" Text="Supplier Invoice No / Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Width="200px" MaxLength="100" />
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtInvoiceSupplierDate" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" AutoGenerateColumns="False" GridLines="None"
        OnNeedDataSource="grdItem_NeedDataSource" AllowPaging="False" OnItemDataBound="grdItem_ItemDataBound">
        <PagerStyle Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo, ItemID, ConversionFactor">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Item Name" UniqueName="Description"
                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="ConversionFactor"
                    HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Quantity" HeaderText="Qty"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="90px" DbValue='<%#Eval("Price")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsDiscountInPercent"
                    HeaderText="Disc In %" UniqueName="IsDiscountInPercent" SortExpression="IsDiscountInPercent"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc In %" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsDiscInPercent" runat="server" Width="50px" Checked='<%#Eval("IsDiscountInPercent")%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount1Percentage"
                    HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                    Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc 1 (%)" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtDisc1" runat="server" Width="50px" DbValue='<%#Eval("Discount1Percentage")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount2Percentage"
                    HeaderText="Disc 2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                    Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc 2 (%)" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtDisc2" runat="server" Width="50px" DbValue='<%#Eval("Discount2Percentage")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc. Amount"
                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Disc Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtDiscAmt" runat="server" Width="90px" DbValue='<%#Eval("Discount")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BatchNumber" HeaderText="Batch Number"
                    UniqueName="BatchNumber" SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ExpiredDate" HeaderText="Expire Date"
                    UniqueName="ExpiredDate" SortExpression="ExpiredDate" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsTaxable"
                        HeaderText="PPn" UniqueName="IsTaxable" SortExpression="IsTaxable"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="False" />      
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="PPn" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsTaxable" runat="server" Width="50px" Checked='<%#Eval("IsTaxable")%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDiscountAmount" runat="server" Text="Global Discount Amount" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtDiscountAmount" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxPercentage" runat="server" Text="Tax Percentage"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTaxPercentage" runat="server" Type="Percent" Width="100px"
                                MaxLength="5" MaxValue="999.99" MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxAmount" runat="server" Text="Tax Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTaxAmount" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" AutoPostBack="true" OnTextChanged="txtTaxAmount_TextChanged" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStampAmount" runat="server" Text="Stamp Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtStampAmount" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
