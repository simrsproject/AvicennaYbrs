<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ItemDeductionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.ItemDeductionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblDeductionID" runat="server" Text="ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDeductionID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDeductionID" runat="server" ErrorMessage="ID required."
                    ValidationGroup="entry" ControlToValidate="txtDeductionID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblMinAmount" runat="server" Text="Min Amount"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtMinAmount" runat="server" Width="100px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvMinAmount" runat="server" ErrorMessage="Min Amount required."
                    ValidationGroup="entry" ControlToValidate="txtMinAmount" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblMaxAmount" runat="server" Text="Max Amount"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtMaxAmount" runat="server" Width="100px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvMaxAmount" runat="server" ErrorMessage="Max Amount required."
                    ControlToValidate="txtMaxAmount" SetFocusOnError="True" ValidationGroup="entry"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDeductionAmount" runat="server" Text="Deduction Amount"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtDeductionAmount" runat="server" Width="100px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDeductionAmount" runat="server" ErrorMessage="Deduction Amount required."
                    ControlToValidate="txtDeductionAmount" SetFocusOnError="True" ValidationGroup="entry"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
