<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorReceiptDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.GuarantorReceiptDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumGuarantorReceipt" runat="server" ValidationGroup="GuarantorReceipt" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="GuarantorReceipt"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGuarantorName" runat="server" Text="Receipt To"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="304px" HighlightTemplatedItems="True"
                            AutoPostBack="True" MarkFirstMatch="False" EnableLoadOnDemand="true" NoWrap="False"
                            OnItemDataBound="cboGuarantorID_ItemDataBound" OnItemsRequested="cboGuarantorID_ItemsRequested"
                            OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged">
                            <FooterTemplate>
                                Note : Show max 10 result
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td style="width: 20px">
                         <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Receipt To required."
                            ControlToValidate="cboGuarantorID" SetFocusOnError="True" ValidationGroup="GuarantorReceipt"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date / Time"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtPaymentDate" runat="server" Width="100px" Enabled="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <telerik:RadMaskedTextBox ID="txtPaymentTime" runat="server" Mask="<00..23>:<00..59>"
                                        PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="true">
                                    </telerik:RadMaskedTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 20px">
                        <asp:RequiredFieldValidator ID="rfvPaymentDate" runat="server" ErrorMessage="Payment Date required."
                            ValidationGroup="entry" ControlToValidate="txtPaymentDate" SetFocusOnError="True">
                            <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>&nbsp;
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTotalPaymentAmount" runat="server" Text="Receipt Amount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtTotalPaymentAmount" runat="server" Width="100px"
                            MaxLength="16" MinValue="0" />
                    </td>
                    <td style="width: 20px">
                        <asp:RequiredFieldValidator ID="rfvTotalPaymentAmount" runat="server" ErrorMessage="Receipt Amount required."
                            ControlToValidate="txtTotalPaymentAmount" SetFocusOnError="True" ValidationGroup="GuarantorReceipt"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="GuarantorReceipt"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="GuarantorReceipt" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%" id="tblInclude" runat="server">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300" MaxLength="500" TextMode="MultiLine" />
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
