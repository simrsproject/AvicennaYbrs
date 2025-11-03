<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RL4ProfessionTypeItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.HRBase.RL4.RL4ProfessionTypeItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRL4ProfessionType" runat="server" ValidationGroup="RL4ProfessionType" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RL4ProfessionType"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRL4ProfessionTypeID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRL4ProfessionTypeID" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRL4ProfessionTypeID" runat="server" ErrorMessage="ID required."
                            ControlToValidate="txtRL4ProfessionTypeID" SetFocusOnError="True" ValidationGroup="RL4ProfessionType"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRL4ProfessionTypeName" runat="server" Text="Profession Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRL4ProfessionTypeName" runat="server" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRL4ProfessionTypeName" runat="server" ErrorMessage="Education Level required."
                            ControlToValidate="txtRL4ProfessionTypeName" SetFocusOnError="True" ValidationGroup="RL4ProfessionType"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RL4ProfessionType"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="RL4ProfessionType" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
            </table>
        </td>
    </tr>
</table>