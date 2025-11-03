<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="EmbalaceDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.EmbalaceDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblEmbalaceID" runat="server" Text="Embalace ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmbalaceID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvEmbalaceID" runat="server" ErrorMessage="Embalace ID required."
                    ValidationGroup="entry" ControlToValidate="txtEmbalaceID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblEmbalaceName" runat="server" Text="Embalace Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmbalaceName" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvEmbalaceName" runat="server" ErrorMessage="Embalace Name required."
                    ValidationGroup="entry" ControlToValidate="txtEmbalaceName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblEmbalaceLabel" runat="server" Text="Initial"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmbalaceLabel" runat="server" Width="100px" MaxLength="15" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblEmbalaceFeeAmount" runat="server" Text="Fee Amount"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtEmbalaceFeeAmount" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="2" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvEmbalaceFeeAmount" runat="server" ErrorMessage="Embalace Fee Amount required."
                    ValidationGroup="entry" ControlToValidate="txtEmbalaceFeeAmount" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
