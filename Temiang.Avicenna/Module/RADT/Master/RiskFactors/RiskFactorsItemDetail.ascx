<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskFactorsItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.RiskFactorsItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRiskFactors" runat="server" ValidationGroup="RiskFactors" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RiskFactors"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblRiskFactorsID" runat="server" Text="Risk Factors ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtRiskFactorsID" runat="server" Width="300px" MaxLength="20">
            </telerik:RadTextBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvRiskFactorsID" runat="server" ErrorMessage="Risk Factors ID required."
                ControlToValidate="txtRiskFactorsID" SetFocusOnError="True" ValidationGroup="RiskFactors"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblRiskFactorsName" runat="server" Text="Risk Factors Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtRiskFactorsName" runat="server" Width="300px">
            </telerik:RadTextBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvRiskFactorsName" runat="server" ErrorMessage="Risk Factors Name required."
                ControlToValidate="txtRiskFactorsName" SetFocusOnError="True" ValidationGroup="RiskFactors"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RiskFactors"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="RiskFactors" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
<br />
<br />