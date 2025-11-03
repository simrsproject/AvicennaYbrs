<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="DepartmentDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.DepartmentDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblDepartmentID" runat="server" Text="Department ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDepartmentID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDepartmentID" runat="server" ErrorMessage="Department ID required."
                    ValidationGroup="entry" ControlToValidate="txtDepartmentID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDepartmentName" runat="server" Text="Department Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDepartmentName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDepartmentName" runat="server" ErrorMessage="Department Name required."
                    ValidationGroup="entry" ControlToValidate="txtDepartmentName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblShortName" runat="server" Text="Short Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtShortName" runat="server" Width="100px" MaxLength="35" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblInitial" runat="server" Text="Initial"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInitial" runat="server" Width="100px" MaxLength="3" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDepartmentManager" runat="server" Text="Department Manager"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDepartmentManager" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr style="display: none">
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsHasRegistration" runat="server" Text="Has Registration" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr style="display: none">
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsAllowBackDateRegistration" runat="server" Text="Allow Back Date Registration" />
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
                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
