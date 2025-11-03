<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorDocumentChecklistDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorDocumentChecklistDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumGuarantorDocumentChecklist" runat="server" ValidationGroup="GuarantorDocumentChecklist" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="GuarantorDocumentChecklist"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblSRRegistrationType" runat="server" Text="Registration Type"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRRegistrationType" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRRegistrationType" runat="server" ErrorMessage="Registration Type required."
                ControlToValidate="cboSRRegistrationType" SetFocusOnError="True" ValidationGroup="GuarantorDocumentChecklist"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSRDocumentChecklist" runat="server" Text="Document Checklist"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRDocumentChecklist" runat="server" Width="300px" AllowCustomText="true"
                Filter="Contains" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRDocumentChecklist" runat="server" ErrorMessage="Document Checklist required."
                ControlToValidate="cboSRDocumentChecklist" SetFocusOnError="True" ValidationGroup="GuarantorDocumentChecklist"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="GuarantorDocumentChecklist"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="GuarantorDocumentChecklist" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>