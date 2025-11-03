<%@ Control Language="C#" AutoEventWireup="true" Codebehind="DiagnoseDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.DiagnoseDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumDiagnose" runat="server" ValidationGroup="Diagnose" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="Diagnose"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblDiagnoseID" runat="server" Text="Diagnose ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtDiagnoseID" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtDiagnoseID_TextChanged" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvDiagnoseID" runat="server" ErrorMessage="Diagnose ID required."
                ControlToValidate="txtDiagnoseID" SetFocusOnError="True" ValidationGroup="Diagnose"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblDiagnoseName" runat="server" Text="Diagnose Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtDiagnoseName" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvDiagnoseName" runat="server" ErrorMessage="Diagnose Name required."
                ControlToValidate="txtDiagnoseName" SetFocusOnError="True" ValidationGroup="Diagnose"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsChronicDisease" runat="server" Text="Chronic Disease" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsDisease" runat="server" Text="Disease" />
        </td>
        <td width="20px">
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
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="Diagnose"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="Diagnose" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
