<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EducationLevelItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.HRBase.EducationLevelItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEducationLevel" runat="server" ValidationGroup="EducationLevel" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EducationLevel"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEducationLevelID" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtEducationLevelID" runat="server" Width="300px" MaxLength="20" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEducationLevelID" runat="server" ErrorMessage="Code required."
                            ControlToValidate="txtEducationLevelID" SetFocusOnError="True" ValidationGroup="EducationLevel"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEducationLevelName" runat="server" Text="Education Level"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtEducationLevelName" runat="server" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEducationLevelName" runat="server" ErrorMessage="Education Level required."
                            ControlToValidate="txtEducationLevelName" SetFocusOnError="True" ValidationGroup="EducationLevel"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREducationGroup" runat="server" Text="Education Group"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREducationGroup" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSREducationGroup" runat="server" ErrorMessage="Education Group required."
                            ControlToValidate="cboSREducationGroup" SetFocusOnError="True" ValidationGroup="EducationLevel"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRlReportID" runat="server" Text="RL Report"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboRlReportID" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRlReportID" runat="server" ErrorMessage="RL Report required."
                            ControlToValidate="cboRlReportID" SetFocusOnError="True" ValidationGroup="EducationLevel"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EducationLevel"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EducationLevel" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
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
