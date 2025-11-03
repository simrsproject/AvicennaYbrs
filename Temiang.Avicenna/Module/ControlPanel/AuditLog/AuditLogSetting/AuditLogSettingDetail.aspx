<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="AuditLogSettingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.AuditLogSettingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTableName" runat="server" Text="Table Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTableName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvTableName" runat="server" ErrorMessage="Table Name required."
                    ValidationGroup="entry" ControlToValidate="txtTableName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTableDescription" runat="server" Text="Table Description"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTableDescription" runat="server" Width="300px" MaxLength="200" TextMode="MultiLine" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsAuditLog" runat="server" Text="Audit Log" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
