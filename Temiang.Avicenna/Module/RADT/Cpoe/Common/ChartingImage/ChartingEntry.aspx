<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="ChartingEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ChartingEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblName" runat="server" Text="Chart Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtName" runat="server" Width="100%" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Chart Name."
                    ValidationGroup="entry" ControlToValidate="txtName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" />
            </td>
            <td width="20px"></td>
        </tr>
    </table>
</asp:Content>
