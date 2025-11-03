<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrescriptionReturnItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.PrescriptionReturnItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumTransPrescriptionItem" runat="server" ValidationGroup="TransPrescriptionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TransPrescriptionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr style="height: 24px">
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsCompound" runat="server" Text="Compound" Enabled="false">
                        </asp:CheckBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtItemID" Width="100px" ReadOnly="true">
                        </telerik:RadTextBox>
                        &nbsp;
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                            ValidationGroup="TransPrescriptionItem" ControlToValidate="txtItemID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblParentNo" runat="server" Text="Compound Header ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtCompoundHeaderID" Width="100px" ReadOnly="true">
                        </telerik:RadTextBox>
                        &nbsp;
                        <asp:Label ID="lblCompoundHeaderName" runat="server"></asp:Label>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReturnQty" runat="server" Text="Return Qty"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtReturnQty" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20">
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
                        <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPrice" Enabled="false" runat="server" Width="100px" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDiscountAmount" runat="server" Text="Discount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtDiscount" runat="server" Width="100px" Enabled="false" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDiscountReason" runat="server" Text="Discount Reason"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboDiscountReason" runat="server" Width="304px" Enabled="false">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TransPrescriptionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="TransPrescriptionItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
