<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentReceiveReturnItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Charges.PaymentReceiveReturnItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cboSRPaymentMethod">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="pnlDiscountReason" />
                <telerik:AjaxUpdatedControl ControlID="pnlBank" />
                <telerik:AjaxUpdatedControl ControlID="pnlCardProvider" />
                <telerik:AjaxUpdatedControl ControlID="pnlCardFeeAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboSRCardProvider">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cboEDCMachineID" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<asp:HiddenField ID="hdnSequenceNo" runat="server" />
<asp:ValidationSummary ID="vsumTransPaymentItem" runat="server" ValidationGroup="TransPaymentItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TransPaymentItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRPaymentMethod" runat="server" Text="Payment Method"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRPaymentMethod" runat="server" Width="304px" AutoPostBack="true"
                            OnSelectedIndexChanged="cboSRPaymentMethod_SelectedIndexChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRPaymentMethod" runat="server" ErrorMessage="Payment Method required."
                            ControlToValidate="cboSRPaymentMethod" SetFocusOnError="True" ValidationGroup="TransPaymentItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr runat="server" id="pnlBank">
                    <td class="label">
                        <asp:Label ID="lblBankID" runat="server" Text="Bank"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboBank" runat="server" Width="304px" />
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
                        <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Amount required."
                            ControlToValidate="txtAmount" SetFocusOnError="True" ValidationGroup="TransPaymentItem"
                            Width="100%">
                            <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TransPaymentItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="TransPaymentItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
            <table width="100%">
                <tr runat="server" id="pnlCardProvider">
                    <td class="label">
                        <asp:Label ID="lblSRCardProvider" runat="server" Text="Card Provider"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRCardProvider" runat="server" Width="304px" AutoPostBack="True"
                            OnSelectedIndexChanged="cboSRCardProvider_SelectedIndexChanged" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <asp:Panel ID="pnlCardDetail" runat="server">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRCardType" runat="server" Text="Card Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRCardType" runat="server" Width="304px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEDCMachineID" runat="server" Text="EDC Machine"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboEDCMachineID" runat="server" Width="304px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboEDCMachineID_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCardNo" runat="server" Text="Card No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCardNo" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCardHolderName" runat="server" Text="Card Holder Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCardHolderName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <tr runat="server" id="pnlCardFeeAmount">
                    <td class="label">
                        <asp:Label ID="lblCardFeeAmount" runat="server" Text="Card Fee Amount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtCardFeeAmount" runat="server" Width="100px" ReadOnly="true" />
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