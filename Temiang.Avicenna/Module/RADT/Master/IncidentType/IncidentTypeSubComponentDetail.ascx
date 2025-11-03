<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncidentTypeSubComponentDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.IncidentTypeSubComponentDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumIncidentTypeSubComp" runat="server" ValidationGroup="IncidentTypeSubComp" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="IncidentTypeSubComp"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSubComponentID" runat="server" Text="Sub Component ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSubComponentID" runat="server" Width="100px" MaxLength="10" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSubComponentID" runat="server" ErrorMessage="Sub Component ID required."
                            ControlToValidate="txtSubComponentID" SetFocusOnError="True" ValidationGroup="IncidentTypeSubComp"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSubComponentName" runat="server" Text="Sub Component Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSubComponentName" runat="server" Width="300px" MaxLength="250" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSubComponentName" runat="server" ErrorMessage="Sub Component Name required."
                            ControlToValidate="txtSubComponentName" SetFocusOnError="True" ValidationGroup="IncidentTypeSubComp"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsAllowEdit" Text="Allow Edit" runat="server" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="IncidentTypeSubComp"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="IncidentTypeSubComp" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
