<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReasonForTreatmentItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ReasonForTreatmentItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumReasonForTreatmentItem" runat="server" ValidationGroup="ReasonForTreatmentItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ReasonForTreatmentItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReasonsForTreatmentID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReasonsForTreatmentID" runat="server" Width="300px" MaxLength="10" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvReasonsForTreatmentID" runat="server" ErrorMessage="ID required."
                            ControlToValidate="txtReasonsForTreatmentID" SetFocusOnError="True" ValidationGroup="ReasonForTreatmentItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReasonsForTreatmentName" runat="server" Text="Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReasonsForTreatmentName" runat="server" Width="300px"
                            MaxLength="250" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvReasonsForTreatmentName" runat="server" ErrorMessage="Name required."
                            ControlToValidate="txtReasonsForTreatmentName" SetFocusOnError="True" ValidationGroup="ReasonForTreatmentItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDiagnoseID" runat="server" Text="Diagnose ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDiagnoseID" runat="server" Width="100px" AutoPostBack="true"
                            OnTextChanged="txtDiagnoseID_TextChanged" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDiagnoseName" runat="server" Text="Diagnose Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDiagnoseName" runat="server" Width="300px" ReadOnly="True" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ReasonForTreatmentItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ReasonForTreatmentItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%">
</table>
