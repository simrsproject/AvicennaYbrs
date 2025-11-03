<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorAutoBillItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorAutoBillItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="GuarantorAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="GuarantorAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true" Filter="Contains"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                            ControlToValidate="cboServiceUnitID" SetFocusOnError="True" ValidationGroup="GuarantorAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10" AutoPostBack="True"
                                        OnTextChanged="txtItemID_TextChanged" />
                                </td>
                                <td>&nbsp;
                                    <asp:Label ID="lblItemName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item ID required."
                            ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="GuarantorAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                            ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="GuarantorAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRItemUnit" runat="server" Text="Item Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="300px" Enabled="false" />
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsGenerateOnRegistration" runat="server" Text="Generate On Old Patient Registration" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsGenerateOnNewRegistration" runat="server" Text="Generate On New Patient Registration" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsGenerateOnReferral" runat="server" Text="Generate On Referral" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsGenerateOnFirstRegistration" runat="server" Text="Generate Only On First Registration" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="GuarantorAutoBillItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="GuarantorAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>