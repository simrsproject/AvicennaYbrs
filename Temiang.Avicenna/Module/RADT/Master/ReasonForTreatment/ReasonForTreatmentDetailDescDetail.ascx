<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReasonForTreatmentDetailDescDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ReasonForTreatmentDetailDescDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumReasonForTreatmentItem" runat="server" ValidationGroup="ReasonForTreatmentDescItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ReasonForTreatmentDescItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReasonsForTreatmentDescID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReasonsForTreatmentDescID" runat="server" Width="300px" MaxLength="10" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvReasonsForTreatmentDescID" runat="server" ErrorMessage="ID required."
                            ControlToValidate="txtReasonsForTreatmentDescID" SetFocusOnError="True" ValidationGroup="ReasonForTreatmentDescItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReasonsForTreatmentDescName" runat="server" Text="Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReasonsForTreatmentDescName" runat="server" Width="300px"
                            MaxLength="250" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvReasonsForTreatmentDescName" runat="server" ErrorMessage="Name required."
                            ControlToValidate="txtReasonsForTreatmentDescName" SetFocusOnError="True" ValidationGroup="ReasonForTreatmentDescItem"
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
                        
                    </td>
                    <td width="20px">
                        
                    </td>
                    <td>
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ReasonForTreatmentDescItem"
                                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                                ValidationGroup="ReasonForTreatmentDescItem" Visible='<%# DataItem is GridInsertionObject %>'>
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
