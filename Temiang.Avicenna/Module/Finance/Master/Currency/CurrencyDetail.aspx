<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="CurrencyDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.CurrencyDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblCurrencyID" runat="server" Text="Currency ID"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtCurrencyID" runat="server" Width="300px" MaxLength="10" />
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvCurrencyID" runat="server" ErrorMessage="Currency ID required."
                        ValidationGroup="entry" ControlToValidate="txtCurrencyID" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
            </tr>
             <tr>
                <td class="label">
                    <asp:Label ID="lblCurrencyName" runat="server" Text="Currency Name"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtCurrencyName" runat="server" Width="300px" MaxLength="100" />
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvCurrencyName" runat="server" ErrorMessage="Currency Name required."
                        ValidationGroup="entry" ControlToValidate="txtCurrencyName" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
            </tr>
             <tr>
                <td class="label">
                    <asp:Label ID="lblCurrencyRate" runat="server" Text="Currency Rate"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadNumericTextBox ID="txtCurrencyRate" runat="server" Width="100px" />
                </td>
                <td width="20px">
                 <asp:RequiredFieldValidator ID="rfvCurrencyRate" runat="server" ErrorMessage="Currency Rate required."
                        ValidationGroup="entry" ControlToValidate="txtCurrencyRate" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
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