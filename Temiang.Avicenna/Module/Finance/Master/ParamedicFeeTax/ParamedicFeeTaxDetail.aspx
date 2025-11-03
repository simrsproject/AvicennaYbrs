<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ParamedicFeeTaxDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeTaxDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblMinAmount" runat="server" Text="Min Amount" />
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtMinAmount" runat="server" Width="100px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfMinAmount" runat="server" ErrorMessage="Min Amount required."
                    ValidationGroup="entry" ControlToValidate="txtMinAmount" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblMaxAmount" runat="server" Text="Max Amount" />
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtMaxAmount" runat="server" Width="100px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvMaxAmount" runat="server" ErrorMessage="Max Amount required."
                    ValidationGroup="entry" ControlToValidate="txtMaxAmount" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPercentage" runat="server" Text="Tax Value" />
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtPercentage" runat="server" Width="100px" Type="Percent"
                    MaxLength="5" MaxValue="999.99" MinValue="0" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPercentage" runat="server" ErrorMessage="Percentage required."
                    ValidationGroup="entry" ControlToValidate="txtPercentage" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Tax Value For Non NPWP" />
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtPercentageNonNpwp" runat="server" Width="100px" Type="Percent"
                    MaxLength="5" MaxValue="999.99" MinValue="0" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPercentageNonNpwp" runat="server" ErrorMessage="Percentage for non npwp required."
                    ValidationGroup="entry" ControlToValidate="txtPercentageNonNpwp" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
