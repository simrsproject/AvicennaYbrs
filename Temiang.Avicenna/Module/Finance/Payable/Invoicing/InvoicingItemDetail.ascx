<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoicingItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Payable.InvoicingItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumInvoiceSupplierItem" runat="server" ValidationGroup="InvoiceSupplierItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="InvoiceSupplierItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="txtTransactionNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtTransactionNo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="150px" MaxLength="3"
                            Enabled="false" Text="d" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                            ControlToValidate="txtTransactionNo" SetFocusOnError="True" ValidationGroup="InvoiceSupplierItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="150px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="150px" MaxLength="10"
                            NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPPn" runat="server" Text="PPn Amount"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtPPnAmount" runat="server" Width="150px" MaxLength="10"
                                        NumberFormat-DecimalDigits="2" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsPpnExcluded" runat="server" Checked="false" Text="Ppn Excluded" />
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <asp:Panel runat="server" ID="pnlPphFixedValue" Visible="False">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPPh22" runat="server" Text="PPh 22"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPPh22Amount" runat="server" Width="150px" MaxLength="10"
                                NumberFormat-DecimalDigits="2" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPPh23" runat="server" Text="PPh 23"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPPh23Amount" runat="server" Width="150px" MaxLength="10"
                                NumberFormat-DecimalDigits="2" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlPphNonFixedValue" Visible="False">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Basic PPh Calculation"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBasicPphCalculation" runat="server" Width="150px" MaxLength="10"
                                NumberFormat-DecimalDigits="2" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPphType" runat="server" Text="PPh Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox runat="server" ID="cboSRPph" Width="154px" AllowCustomText="true"
                                            Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboSRPph_OnSelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPphPercentage" runat="server" Width="80px" MaxLength="10"
                                            MaxValue="99.99" MinValue="0" NumberFormat-DecimalDigits="2" Type="Percent" ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPphAmount" runat="server" Text="PPh Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPphAmount" runat="server" Width="150px" MaxLength="10"
                                NumberFormat-DecimalDigits="2" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblStampAmount" runat="server" Text="Stamp / Shipping"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtStampAmount" runat="server" Width="150px" MaxLength="10"
                            NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDownPaymentAmount" runat="server" Text="Down Payment"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtDownPaymentAmount" runat="server" Width="150px"
                            MaxLength="10" MinValue="0" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblOtherDeduction" runat="server" Text="Other Deduction"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtOtherDeduction" runat="server" Width="150px" MaxLength="10"
                            NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblInvoiceSN" runat="server" Text="Tax Serial No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtInvoiceSN" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTaxInvoiceDate" runat="server" Text="Tax Invoice Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtTaxInvoiceDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="trSRItemType" runat="server" visible="false">
                    <td class="label">
                        <asp:Label ID="Label3" runat="server" Text="Item Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRItemType" runat="server" Visible="false" 
                            OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged"
                            AutoPostBack="true" >
                            <Items>
                                <telerik:RadComboBoxItem Text="" Value="" />
                                <telerik:RadComboBoxItem Text="Medical" Value="11" />
                                <telerik:RadComboBoxItem Text="Non Medical" Value="21" />
                                <telerik:RadComboBoxItem Text="Item Kitchen" Value="81" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRItemType" runat="server" ErrorMessage="Item Type required."
                            ValidationGroup="InvoiceSupplierItem" ControlToValidate="cboSRItemType" SetFocusOnError="True"
                            Width="100%" Enabled="false">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 79px">
                        Debet Account
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboCOA" Height="190px"
                            Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                            OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                            OnItemDataBound="cboCOA_ItemDataBound"
                            OnItemsRequested="cboCOA_ItemsRequested"  >
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="Subledger"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSl" Height="190px" Width="100%"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                            OnItemDataBound="cboSl_ItemDataBound" 
                            OnItemsRequested="cboSl_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCurrencyType" runat="server" Text="Currency Type / Rate" />
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboCurrencyID" runat="server" Width="179px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCurrencyID_SelectedIndexChanged" />
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCurrencyRate" runat="server" Width="113px" MinValue="1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvCurrencyType" runat="server" ErrorMessage="Currency Type required."
                            ValidationGroup="InvoiceSupplierItem" ControlToValidate="cboCurrencyID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="InvoiceSupplierItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="InvoiceSupplierItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
