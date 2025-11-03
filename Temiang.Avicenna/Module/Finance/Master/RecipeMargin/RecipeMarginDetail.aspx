<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="RecipeMarginDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.RecipeMarginDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblStartingValue" runat="server" Text="Starting Value" />
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtStartingValue" runat="server" Width="100px" MaxValue="9999" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvStartingValue" runat="server" ErrorMessage="Starting Value required."
                    ValidationGroup="entry" ControlToValidate="txtStartingValue" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblEndingValue" runat="server" Text="Ending Value" />
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtEndingValue" runat="server" Width="100px" MaxValue="9999" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvEndingValue" runat="server" ErrorMessage="Ending Value required."
                    ValidationGroup="entry" ControlToValidate="txtEndingValue" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblRecipeAmount" runat="server" Text="Amount" />
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtRecipeAmount" runat="server" Width="100px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvRecipeAmount" runat="server" ErrorMessage="Recipe Amount required."
                    ValidationGroup="entry" ControlToValidate="txtRecipeAmount" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
