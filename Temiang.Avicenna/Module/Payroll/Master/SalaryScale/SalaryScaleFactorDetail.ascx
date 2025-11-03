<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalaryScaleFactorDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryScaleFactorDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumStandardSalaryFaktor" runat="server" ValidationGroup="SalaryScaleFactor" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="v"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblSalaryScaleFactorID" runat="server" Text="ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtSalaryScaleFactorID" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSalaryScaleFactorID" runat="server" ErrorMessage="ID required."
                ControlToValidate="txtSalaryScaleFactorID" SetFocusOnError="True" ValidationGroup="SalaryScaleFactor" Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900"
                MaxDate="12/31/2999" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="ValidFrom required."
                ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="SalaryScaleFactor" Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Amount required."
                ControlToValidate="txtAmount" SetFocusOnError="True" ValidationGroup="SalaryScaleFactor" Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>

    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SalaryScaleFactor"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="SalaryScaleFactor"
                Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
