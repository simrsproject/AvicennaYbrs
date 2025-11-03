<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SafetyCultureIncidentReportsConslusionItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.KEHRS.SafetyCultureIncidentReportsConslusionItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSafetyCultureIncidentReportsConslusion" runat="server" ValidationGroup="SafetyCultureIncidentReportsConslusion" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SafetyCultureIncidentReportsConslusion"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnSequenceNo" />
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="50%" valign="Top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblConclusion" runat="server" Text="Conclusion" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtConclusion" runat="server" Width="100%" TextMode="MultiLine" Height="250px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvConclusion" runat="server" ErrorMessage="Conclusion required."
                            ValidationGroup="SafetyCultureIncidentReportsConslusion" ControlToValidate="txtConclusion" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SafetyCultureIncidentReportsConslusion"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="SafetyCultureIncidentReportsConslusion" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="Top">
            
        </td>
    </tr>
</table>