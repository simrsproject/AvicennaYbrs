<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitAutoBillItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitAutoBillItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
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
                            ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
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
                            ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
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
                        <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemUnit" runat="server" ErrorMessage="Item unit required."
                            ControlToValidate="cboSRItemUnit" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr runat="server" id="trIsUsingGenerateOnSchedule">
                    <td class="label"></td>
                    <td colspan="3">
                        <fieldset>
                            <legend>
                                <asp:CheckBox ID="chkIsGenerateOnSchedule" runat="server" Text="Generate Using Schedule (for Inpatient Only)" /></legend>
                            <table>
                                <tr>
                                    <td>Start From Day</td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtGenerateOnDayStart" NumberFormat-DecimalDigits="0" Width="40px" MinValue="0"></telerik:RadNumericTextBox>
                                    </td>
                                    <td>&nbsp;To&nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtGenerateOnDayEnd" NumberFormat-DecimalDigits="0" Width="40px" MinValue="0"></telerik:RadNumericTextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="5">For In Bed Class: (*if nothing selected will apply for all class)</td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="padding-left: 10px">
                                        <telerik:RadCheckBoxList runat="server" AutoPostBack="false" ID="chklGenerateOnClassIDs" Direction="Horizontal">
                                            <DataBindings DataTextField="ClassName" DataValueField="ClassID" DataSelectedField="IsSelected" />
                                        </telerik:RadCheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsAutoPayment" runat="server" Text="Auto Payment" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
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
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
