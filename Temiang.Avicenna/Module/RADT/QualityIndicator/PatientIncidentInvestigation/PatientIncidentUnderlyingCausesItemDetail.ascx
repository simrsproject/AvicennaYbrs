<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientIncidentUnderlyingCausesItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientIncidentUnderlyingCausesItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumIncidentUnderlyingCauses" runat="server" ValidationGroup="IncidentUnderlyingCauses" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="IncidentUnderlyingCauses"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFactorID" runat="server" Text="Factor"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboFactorID" runat="server" Width="300px" OnSelectedIndexChanged="cboFactorID_SelectedIndexChanged"
                            AutoPostBack="True" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvFactorID" runat="server" ErrorMessage="Factor required."
                            ValidationGroup="IncidentUnderlyingCauses" ControlToValidate="cboFactorID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFactorItemID" runat="server" Text="Factor Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboFactorItemID" runat="server" Width="300px" OnSelectedIndexChanged="cboFactorItem_SelectedIndexChanged"
                            AutoPostBack="True" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvcboFactorItem" runat="server" ErrorMessage="Factor Item required."
                            ValidationGroup="IncidentUnderlyingCauses" ControlToValidate="cboFactorItemID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblComponentID" runat="server" Text="Component"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboComponentID" runat="server" Width="300px" OnSelectedIndexChanged="cboComponentID_SelectedIndexChanged"
                            AutoPostBack="True" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblComponentName" runat="server" Text="Component Description"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtComponentName" runat="server" Width="300px" MaxLength="250" TextMode="MultiLine"
                            Enabled="False" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="IncidentUnderlyingCauses"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="IncidentUnderlyingCauses" Visible='<%# DataItem is GridInsertionObject %>'>
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