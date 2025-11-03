<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="LoginDataEncrypt.aspx.cs" Inherits="Temiang.Avicenna.LoginDataEncrypt" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="left" class="label">
                <asp:Label ID="lblPassword" runat="server" Text="Password Data"></asp:Label>
            </td>
            <td align="left" style="font: 12px arial,sans-serif" width="150px">
                <telerik:RadTextBox ID="txtPassword" runat="server" Width="150px" MaxLength="15"
                    TextMode="Password" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ValidationGroup="entry"
                    ErrorMessage="Password required." ControlToValidate="txtPassword" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
            </td>
        </tr>
    </table>
</asp:Content>
