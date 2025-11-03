<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="CpptVerification.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.CpptVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>DPJP Notes</legend>
        <telerik:RadTextBox ID="txtDpjpNotes" runat="server" Width="100%" Height="170px" MaxLength="4000"
            TextMode="MultiLine" Resize="Vertical" />
    </fieldset>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnVerif" runat="server" Text="Verified Current" Width="120" OnClick="btnVerif_Click" OnClientClick="if (!confirm('Verified current ?')) return false;" />&nbsp;
                    <asp:Button ID="btnVerifAll" runat="server" Text="Verified All" Width="120" OnClick="btnVerifAll_Click" OnClientClick="if (!confirm('Verified current with all previous ?')) return false;" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSaveNote" runat="server" Text="Add Note" Width="70" OnClick="btnSaveNote_Click" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70" OnClientClick="Close();return false;" />
            </td>
        </tr>
    </table>
</asp:Content>
