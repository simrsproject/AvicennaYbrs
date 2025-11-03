<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MachineItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Cssd.Master.MachineItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="CssdMachineItem" runat="server" ValidationGroup="CssdMachineItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CssdMachineItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblSRCssdProcessType" runat="server" Text="Process Type"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboSRCssdProcessType" Width="300px" AllowCustomText="true"
                Filter="Contains">
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRCssdProcessType" runat="server" ErrorMessage="Process Type required."
                ControlToValidate="cboSRCssdProcessType" SetFocusOnError="True" ValidationGroup="CssdMachineItem"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr height="30">
        <td class="label">
            <asp:Label ID="lblDuration" runat="server" Text="Duration"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtDuration" runat="server" Width="100px" MaxLength="10"
                MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvDuration" runat="server" ErrorMessage="Duration required."
                ControlToValidate="txtDuration" SetFocusOnError="True" ValidationGroup="CssdMachineItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CssdMachineItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="CssdMachineItem" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
