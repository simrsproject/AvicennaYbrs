<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitScheduleDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitScheduleDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table>
    <tr>
        <td class="label">
            Day of Week
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboDayOfWeek" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvDayOfWeek" runat="server" ErrorMessage="Day of Week required."
                ControlToValidate="cboDayOfWeek" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            Start Time
        </td>
        <td class="entry">
            <telerik:RadMaskedTextBox ID="txtStartTime" runat="server" Mask="<00..23>:<00..59>"
                PromptChar="_" RoundNumericRanges="false" Width="100px">
            </telerik:RadMaskedTextBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ErrorMessage="Start Time required."
                ControlToValidate="txtStartTime" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            End Time
        </td>
        <td class="entry">
            <telerik:RadMaskedTextBox ID="txtEndTime" runat="server" Mask="<00..23>:<00..59>"
                PromptChar="_" RoundNumericRanges="false" Width="100px">
            </telerik:RadMaskedTextBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ErrorMessage="End Time required."
                ControlToValidate="txtEndTime" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
