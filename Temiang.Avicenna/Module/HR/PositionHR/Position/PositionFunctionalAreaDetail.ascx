<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PositionFunctionalAreaDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionFunctionalAreaDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPositionFunctionalArea" runat="server" ValidationGroup="PositionFunctionalArea" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PositionFunctionalArea"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblPositionFunctionalAreaID" runat="server" Text="Position Functional Area ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtPositionFunctionalAreaID" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvPositionFunctionalAreaID" runat="server" ErrorMessage="Position Functional Area ID required."
                ControlToValidate="txtPositionFunctionalAreaID" SetFocusOnError="True" ValidationGroup="PositionFunctionalArea" Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSRPositionFunctionalArea" runat="server" Text="Position Functional Area"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRPositionFunctionalArea" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRPositionFunctionalArea" runat="server" ErrorMessage="Position Functional Area required."
                ControlToValidate="cboSRPositionFunctionalArea" SetFocusOnError="True" ValidationGroup="PositionFunctionalArea" Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" Height="80px" MaxLength="4000" TextMode="MultiLine" />
        </td>
        <td width="20px"></td>
        <td></td>
    </tr>

    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PositionFunctionalArea"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PositionFunctionalArea"
                Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel"></asp:Button></td>
    </tr>
</table>

