<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BalanceDetailExpiredDateItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Stock.BalanceDetailExpiredDateItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemBalanceDetailEd" runat="server" ValidationGroup="ItemBalanceDetailEd" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemBalanceDetailEd"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBatchNumber" runat="server" Text="Batch Number"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBatchNumber" runat="server" Width="300px" MaxLength="50" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvBatchNo" runat="server" ErrorMessage="Batch Number is required."
                            ControlToValidate="txtBatchNumber" SetFocusOnError="True" ValidationGroup="ItemBalanceDetailEd"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblExpiredDate" runat="server" Text="Expired Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtExpiredDate" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvExpiredDate" runat="server" ErrorMessage="Expired Date is required."
                            ControlToValidate="txtExpiredDate" SetFocusOnError="True" ValidationGroup="ItemBalanceDetailEd"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBalance" runat="server" Text="Balance"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtBalance" runat="server" Width="100px" Enabled="false" />
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
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemBalanceDetailEd"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemBalanceDetailEd" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%" valign="top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>