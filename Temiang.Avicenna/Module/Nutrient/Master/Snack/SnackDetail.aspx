<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="SnackDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.SnackDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblSnackID" runat="server" Text="Snack ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSnackID" runat="server" Width="100px" MaxLength="10">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvSnackID" runat="server" ErrorMessage="Snack ID required."
                    ValidationGroup="entry" ControlToValidate="txtSnackID" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSnackName" runat="server" Text="Snack Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSnackName" runat="server" Width="300px" MaxLength="200">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvSnackName" runat="server" ErrorMessage="Snack Name required"
                    ValidationGroup="entry" ControlToValidate="txtSnackName" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label" style="height: 22px">
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