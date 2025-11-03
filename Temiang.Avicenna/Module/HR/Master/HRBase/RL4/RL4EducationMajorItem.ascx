<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RL4EducationMajorItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.HRBase.RL4.RL4EducationMajorItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRL4EducationMajor" runat="server" ValidationGroup="RL4EducationMajor" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RL4EducationMajor"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRL4EducationMajorID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRL4EducationMajorID" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRL4EducationMajorID" runat="server" ErrorMessage="ID required."
                            ControlToValidate="txtRL4EducationMajorID" SetFocusOnError="True" ValidationGroup="RL4EducationMajor"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRL4EducationMajorName" runat="server" Text="Education Major"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRL4EducationMajorName" runat="server" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRL4EducationMajorName" runat="server" ErrorMessage="Education Major required."
                            ControlToValidate="txtRL4EducationMajorName" SetFocusOnError="True" ValidationGroup="RL4EducationMajor"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RL4EducationMajor"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="RL4EducationMajor" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
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