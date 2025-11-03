<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PpiRiskFactorsItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PpiRiskFactorsItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemBalance" runat="server" ValidationGroup="PpiRiskFactorsItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PpiRiskFactorsItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td width="50%">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDateOfInfection" runat="server" Text="Date Of Infection" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtDateOfInfection" runat="server" Width="100px"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDateOfInfection" runat="server" ErrorMessage="Date Of Infection required."
                            ControlToValidate="txtDateOfInfection" SetFocusOnError="True" ValidationGroup="PpiRiskFactorsItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRSignsOfInfection" runat="server" Text="Signs Of Infection" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRSignsOfInfection" runat="server" Width="304px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRSignsOfInfection" runat="server" ErrorMessage="Signs Of Infection required."
                            ControlToValidate="cboSRSignsOfInfection" SetFocusOnError="True" ValidationGroup="PpiRiskFactorsItem"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="250" TextMode="MultiLine" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PpiRiskFactorsItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PpiRiskFactorsItem" Visible='<%# DataItem is GridInsertionObject %>' />
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
