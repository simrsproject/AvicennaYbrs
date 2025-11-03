<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThrScheduleItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.ThrScheduleItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumThrScheduleItem" runat="server" ValidationGroup="ThrScheduleItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ThrScheduleItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblThrScheduleItemID" runat="server" Text="ThrScheduleItemID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtCounterItemID" runat="server" Width="300px" />
        </td>
        <td width="20px"></td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSRReligion" runat="server" Text="Religion"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRReligion" runat="server" Width="304px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRReligion" runat="server" ErrorMessage="Religion required."
                ControlToValidate="cboSRReligion" SetFocusOnError="True" ValidationGroup="ThrScheduleItem" Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>

    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ThrScheduleItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="ThrScheduleItem"
                Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>

        </td>
    </tr>
</table>
