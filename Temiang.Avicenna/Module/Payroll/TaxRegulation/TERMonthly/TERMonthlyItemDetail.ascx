<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TERMonthlyItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.TaxRegulation.TERMonthlyItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumTERMonthlyItem" runat="server" ValidationGroup="TERMonthlyItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TERMonthlyItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblTERMonthlyItemID" runat="server" Text="TER Monthly Item ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtTERMonthlyItemID" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvTERMonthlyItemID" runat="server" ErrorMessage="ID required."
                ControlToValidate="txtTERMonthlyItemID" SetFocusOnError="True" ValidationGroup="TERMonthlyItem" Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblLowerLimit" runat="server" Text="Lower Limit"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtLowerLimit" runat="server" Width="150px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvLowerLimit" runat="server" ErrorMessage="Lower Limit required."
                ControlToValidate="txtLowerLimit" SetFocusOnError="True" ValidationGroup="TERMonthlyItem" Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblUpperLimit" runat="server" Text="Upper Limit"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtUpperLimit" runat="server" Width="150px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvUpperLimit" runat="server" ErrorMessage="Upper Limit required."
                ControlToValidate="txtUpperLimit" SetFocusOnError="True" ValidationGroup="TERMonthlyItem" Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblTaxRate" runat="server" Text="Tax Rate"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtTaxRate" runat="server" Width="150px" Type="Percent" MaxValue="100" MinValue="0"/>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvTaxRate" runat="server" ErrorMessage="Tax Rate required."
                ControlToValidate="txtTaxRate" SetFocusOnError="True" ValidationGroup="TERMonthlyItem" Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TERMonthlyItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="TERMonthlyItem"
                Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
