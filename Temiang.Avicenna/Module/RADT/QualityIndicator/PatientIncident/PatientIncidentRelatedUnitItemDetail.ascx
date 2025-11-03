<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientIncidentRelatedUnitItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientIncidentRelatedUnitItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumIncidentRelatedUnit" runat="server" ValidationGroup="IncidentRelatedUnit" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="IncidentRelatedUnit"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                            ControlToValidate="cboServiceUnitID" SetFocusOnError="True" ValidationGroup="IncidentRelatedUnit"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="IncidentRelatedUnit"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="IncidentRelatedUnit" Visible='<%# DataItem is GridInsertionObject %>'>
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
