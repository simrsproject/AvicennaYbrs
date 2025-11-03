<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderReceiveItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.PurchaseOrderReceiveItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemTransactionItem" runat="server" ValidationGroup="ItemTransactionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTransactionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="txtItemID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtItemID" />
                <telerik:AjaxUpdatedControl ControlID="lblItemName" />
                <telerik:AjaxUpdatedControl ControlID="cboSRItemUnit" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table width="100%">
    <tr>
        <td style="width: 50%" valign="top">
            <table width="100%">
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblSequenceNo" runat="server" Text="Sequence No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSequenceNo" runat="server" Width="100px" MaxLength="3"
                            Enabled="false" Text="d" />
                        <telerik:RadTextBox ID="txtReferenceSequenceNo" runat="server" Width="100px" MaxLength="3"
                            Enabled="false" Text="r" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSequenceNo" runat="server" ErrorMessage="Sequence No required."
                            ControlToValidate="txtSequenceNo" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                            OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnItemsRequested="cboItemID_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                </b>&nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>)
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                
                <tr runat="server" id="trFabricID">
                    <td class="label">
                        <asp:Label ID="lblFabricID" runat="server" Text="Factory" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboFabricID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboFabricID_ItemDataBound"
                            OnItemsRequested="cboFabricID_ItemsRequested" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                        <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="100px" AutoPostBack="True"
                            OnSelectedIndexChanged="cboSRItemUnit_SelectedIndexChanged" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblConversionFactor" runat="server" Text="Conversion Factor" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtConversionFactor" runat="server" Width="100px"
                            MinValue="1" ReadOnly="True" AutoPostBack="True" OnTextChanged="txtConversionFactor_TextChanged" />
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
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPrice" runat="server" Text="Price" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" MaxLength="16"
                            MinValue="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
            </table>
        </td>
        <td style="width: 50%" valign="top">
            <table width="100%">
                <tr id="trIsDiscInPerc" runat="server">
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsDiscountInPercent" runat="server" Text="Discount In Percent"
                            Enabled="true" OnCheckedChanged="chkIsDiscountInPercent_CheckedChanged" AutoPostBack="true"
                            Checked="True" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr id="trDisc1" runat="server">
                    <td class="label">
                        <asp:Label ID="lblDiscount1Percentage" runat="server" Text="Discount 1 (%)" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtDiscount1Percentage" runat="server" Type="Percent"
                            Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr id="trDisc2" runat="server">
                    <td class="label">
                        <asp:Label ID="lblDiscount2Percentage" runat="server" Text="Discount 2 (%)" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtDiscount2Percentage" runat="server" Type="Percent"
                            Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr id="trDiscAmt" runat="server">
                    <td class="label">
                        <asp:Label ID="lblDiscountAmount" runat="server" Text="Discount Amount" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtDiscountAmount" runat="server" Width="100px" MaxLength="16"
                            MinValue="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <asp:Panel runat="server" ID="pnlEd">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBatchNumber" runat="server" Text="Batch No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBatchNumber" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblExpiredDate" runat="server" Text="Expiry Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtExpiredDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </asp:Panel>
                <tr id="trBonus" runat="server">
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsBonusItem" runat="server" Text="Bonus" Enabled="false" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr id="trTaxable" runat="server">
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsTaxable" runat="server" Text="Taxable" Enabled="false" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblQtyPending" runat="server" Text="Quantity Pending"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQtyPending" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Barcode"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBarcode" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemTransactionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemTransactionItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
