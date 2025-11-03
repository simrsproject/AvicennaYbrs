<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderReturnItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.PurchaseOrderReturnItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemTransactionItem" runat="server" ValidationGroup="ItemTransactionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTransactionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSequenceNo" runat="server" Text="Sequence No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSequenceNo" runat="server" Width="100px" MaxLength="3"
                            Enabled="false" Text="" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSequenceNo" runat="server" ErrorMessage="Sequence No required."
                            ControlToValidate="txtSequenceNo" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" ReadOnly="True" />&nbsp;<asp:Label
                            ID="lblItemName" runat="server"></asp:Label>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr height="30">
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" MaxLength="10"
                                        MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="100px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboSRItemUnit_SelectedIndexChanged" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                            ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvSRItemUnit" runat="server" ErrorMessage="Unit required."
                            ControlToValidate="cboSRItemUnit" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblConversionFactor" runat="server" Text="Conversion Factor" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtConversionFactor" runat="server" Width="100px"
                            MinValue="1" ReadOnly="True" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvConversionFactor" runat="server" ErrorMessage="Conversion Factor required."
                            ControlToValidate="txtConversionFactor" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblQtyPending" runat="server" Text="Quantity Pending"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQtyPending" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemTransactionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <asp:Panel runat="server" ID="pnlPrice">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPrice" runat="server" Text="Price" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" ReadOnly="True" />
                            <telerik:RadNumericTextBox ID="txtPriceInCurrency" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" Visible="False" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsDiscountInPercent" runat="server" Text="Discount In Percent"
                                Enabled="False" OnCheckedChanged="chkIsDiscountInPercent_CheckedChanged" AutoPostBack="true"
                                Checked="True" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDiscount1Percentage" runat="server" Text="Discount 1 (%)" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtDiscount1Percentage" runat="server" Type="Percent"
                                Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" ReadOnly="True" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDiscount2Percentage" runat="server" Text="Discount 2 (%)" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtDiscount2Percentage" runat="server" Type="Percent"
                                Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" ReadOnly="True" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDiscountAmount" runat="server" Text="Discount Amount" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtDiscountAmount" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" ReadOnly="True" />
                            <telerik:RadNumericTextBox ID="txtDiscountAmountInCurrency" runat="server" Width="100px"
                                MaxLength="16" MinValue="0" Visible="False" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPurchasePrice" runat="server" NumberFormat-DecimalDigits="0" />
                            <telerik:RadNumericTextBox ID="txtPurchaseDiscountInCurrency" runat="server" NumberFormat-DecimalDigits="0" />
                            <telerik:RadNumericTextBox ID="txtPurchaseDiscount" runat="server" NumberFormat-DecimalDigits="0" />
                            <telerik:RadNumericTextBox ID="txtPurchasePriceInCurrency" runat="server" NumberFormat-DecimalDigits="0" />
                            <telerik:RadNumericTextBox ID="txtPurchaseConversionFactor" runat="server" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBatchNumber" runat="server" Text="Batch No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBatchNumber" runat="server" Width="300px" ReadOnly="True" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblExpiredDate" runat="server" Text="Expiry Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtExpiredDate" runat="server" Width="100px" Enabled="False" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
