<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="MenuDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.MenuDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblMenuID" runat="server" Text="Menu ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMenuID" runat="server" Width="100px" MaxLength="10">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvMenuID" runat="server" ErrorMessage="Menu ID required."
                    ValidationGroup="entry" ControlToValidate="txtMenuID" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblMenuName" runat="server" Text="Menu Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMenuName" runat="server" Width="300px" MaxLength="200">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvMenuName" runat="server" ErrorMessage="Menu Name required"
                    ValidationGroup="entry" ControlToValidate="txtMenuName" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry" style="height: 22px">
                <asp:CheckBox ID="chkIsExtra" Text="Extra" runat="server" />
            </td>
            <td width="20" style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry" style="height: 22px">
                <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
            </td>
            <td width="20" style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
    </table>
</asp:Content>
