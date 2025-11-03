<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTaxStatusDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.PayrollInfo.EmployeeTaxStatusDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeTaxStatus" runat="server" ValidationGroup="EmployeeTaxStatus" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeTaxStatus"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblSPTYear" runat="server" Text="SPT Year"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtSPTYear" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSPTYear" runat="server" ErrorMessage="SPT Year required."
                ControlToValidate="txtSPTYear" SetFocusOnError="True" ValidationGroup="EmployeeTaxStatus"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSRTaxStatus" runat="server" Text="Tax Status"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRTaxStatus" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRTaxStatus" runat="server" ErrorMessage="TaxStatus required."
                ControlToValidate="cboSRTaxStatus" SetFocusOnError="True" ValidationGroup="EmployeeTaxStatus"
                Width="100%">
                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeTaxStatus"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="EmployeeTaxStatus" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
