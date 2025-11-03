<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemPackageItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ItemPackageItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemPackage" runat="server" ValidationGroup="ItemPackage" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemPackage"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDetailItemID" runat="server" Text="Detail Item" />
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtDetailItemID" runat="server" Width="100px" MaxLength="10"
                                        AutoPostBack="true" OnTextChanged="txtDetailItemID_TextChanged" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblDetailItemName" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDetailItemID" runat="server" ErrorMessage="Detail Item ID required."
                            ControlToValidate="txtDetailItemID" SetFocusOnError="True" ValidationGroup="ItemPackage"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="10"
                                        AutoPostBack="true" OnTextChanged="txtServiceUnitID_TextChanged" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblServiceUnitName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                            ControlToValidate="txtServiceUnitID" SetFocusOnError="True" ValidationGroup="ItemPackage"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity" />
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="100px" MaxLength="20"
                                        ReadOnly="true" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                            ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="ItemPackage"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr runat="server" id="trDiscount">
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Discount" />
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtDiscountValue" runat="server" Width="100px" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsDiscountInPercent" runat="server" Text="In Percent (%)" OnCheckedChanged="chkIsDiscountInPercent_CheckedChanged"
                                        AutoPostBack="true" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemPackage"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemPackage" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsStockControl" runat="server" Text="Stock Control" Enabled="False" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkExtraItem" runat="server" Text="Extra Item" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr id="trChkAutoApprove" runat="server">
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkAutoApprove" runat="server" Text="Auto Approve" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
