<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="MenuVersionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.MenuVersionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblVersionID" runat="server" Text="Version ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtVersionID" runat="server" Width="100px" MaxLength="10">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvVersionID" runat="server" ErrorMessage="Version ID required."
                    ValidationGroup="entry" ControlToValidate="txtVersionID" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblVersionName" runat="server" Text="Version Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtVersionName" runat="server" Width="300px" MaxLength="200">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvVersionName" runat="server" ErrorMessage="Version Name required"
                    ValidationGroup="entry" ControlToValidate="txtVersionName" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCycle" runat="server" Text="Cycle"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtCycle" runat="server" Width="100px" MaxLength="10"
                    MinValue="0" NumberFormat-DecimalDigits="0" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsPlusOneRule" Text="Using +1 Rule" runat="server" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
