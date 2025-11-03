<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PpiInfectionDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PpiInfectionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemBalance" runat="server" ValidationGroup="PpiInfection" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PpiInfection"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td width="50%">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRInfectionType" runat="server" Text="Infection" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRInfectionType" runat="server" Width="304px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRInfectionType" runat="server" ErrorMessage="Infection required."
                            ControlToValidate="cboSRInfectionType" SetFocusOnError="True" ValidationGroup="PpiInfection"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDaysTo" runat="server" Text="Days To"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtDaysTo" runat="server" Width="50px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvDayTo" runat="server" ErrorMessage="Days To required."
                            ControlToValidate="txtDaysTo" SetFocusOnError="True" ValidationGroup="PpiInfection"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCultures" runat="server" Text="Cultures"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCultures" runat="server" Width="300px" MaxLength="500"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvCultures" runat="server" ErrorMessage="Cultures required."
                            ControlToValidate="txtCultures" SetFocusOnError="True" ValidationGroup="PpiInfection"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PpiInfection"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PpiInfection" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
