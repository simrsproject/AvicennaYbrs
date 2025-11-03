<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="APPaymentTypeDetailEdit.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction.APPaymentTypeDetailEdit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMemorialItem" runat="server" ValidationGroup="MemorialItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="MemorialItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:Label ID="lblDetailId" runat="server" Visible="false"></asp:Label>

<table width="100%">
    <tr>
        <td class="label" style="width: 16px">PPh22 Amount
        </td>
        <td class="entry" style="width: 361px">
            <telerik:RadNumericTextBox ID="txtPPh22Amount" runat="server" Value="0" MinValue="0" Width="150px">
                <EnabledStyle HorizontalAlign="Right" />
            </telerik:RadNumericTextBox>
        </td>
        <td style="width: 20px"></td>
    </tr>
    <tr>
        <td class="label" style="width: 16px">PPh23 Amount
        </td>
        <td class="entry" style="width: 361px">
            <telerik:RadNumericTextBox ID="txtPPh23Amount" runat="server" Value="0" MinValue="0" Width="150px">
                <EnabledStyle HorizontalAlign="Right" />
            </telerik:RadNumericTextBox>
        </td>
        <td style="width: 20px"></td>
    </tr>
    <tr>
        <td class="label" style="width: 16px">Stamp Amount
        </td>
        <td class="entry" style="width: 361px">
            <telerik:RadNumericTextBox ID="txtStampAmount" runat="server" Value="0" MinValue="0" Width="150px">
                <EnabledStyle HorizontalAlign="Right" />
            </telerik:RadNumericTextBox>
        </td>
        <td style="width: 20px"></td>
    </tr>
    <tr>
        <td></td>
        <td class="entry" align="center" style="width: 361px">
            <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" ValidationGroup="BeginningBalanceItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="BeginningBalanceItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
                        <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"></asp:Button>
        </td>
        <td style="width: 20px"></td>
    </tr>
</table>

