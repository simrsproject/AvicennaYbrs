<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WashingMachineProgramDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Master.WashingMachineProgramDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="LaundryWashingMachineProgram" runat="server" ValidationGroup="LaundryWashingMachineProgram" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="LaundryWashingMachineProgram"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblSRLaundryProgram" runat="server" Text="Program"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboSRLaundryProgram" Width="300px" AllowCustomText="true"
                Filter="Contains">
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRLaundryProgram" runat="server" ErrorMessage="Program required."
                ControlToValidate="cboSRLaundryProgram" SetFocusOnError="True" ValidationGroup="LaundryWashingMachineProgram"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="LaundryWashingMachineProgram"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="LaundryWashingMachineProgram" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>